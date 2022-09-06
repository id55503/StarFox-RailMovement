//Create by DongLei

using System;
using UnityEngine;

namespace GameScript.Scripts.ThinkingStars.Artillery
{
    public class GameSmartMissile : SmartMissile<Rigidbody, Vector3>
    {
        public LayerMask layerMask;
        public GameObject impactParticle;
        public GameObject projectileParticle;
        public GameObject muzzleParticle;
        
        
        void Awake()
        {
            m_rigidbody = GetComponent<Rigidbody>();
            projectileParticle = Instantiate(projectileParticle, transform.position, transform.rotation) as GameObject;
            projectileParticle.transform.parent = transform;
            if (muzzleParticle)
            {
                muzzleParticle = Instantiate(muzzleParticle, transform.position, transform.rotation) as GameObject;
                Destroy(muzzleParticle, 1.5f); // Lifetime of muzzle effect.
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            impactParticle = Instantiate(impactParticle, transform.position, Quaternion.FromToRotation(Vector3.up, Vector3.up)) as GameObject;
            projectileParticle.SetActive(false);
        }

        protected override Transform findNewTarget()
        {
            foreach (Collider newTarget in Physics.OverlapSphere(transform.position, m_searchRange, layerMask))
            {
                if (newTarget.gameObject.CompareTag(m_targetTag) && isWithinRange(newTarget.transform.position))
                {
                    m_targetDistance = Vector3.Distance(newTarget.transform.position, transform.position);
                    return newTarget.transform;
                }
            }

            return null;
        }

        protected override bool isWithinRange(Vector3 coordinates)
        {
            if (Vector3.Distance(coordinates, transform.position) < m_targetDistance
                && Vector3.Angle(transform.forward, coordinates - transform.position) < m_searchAngle / 2)
            {
                return true;
            }

            return false;
        }

        protected override void goToTarget()
        {
            m_direction = (m_target.position + m_targetOffset - transform.position).normalized *
                          m_distanceInfluence.Evaluate(1 -
                                                       (m_target.position + m_targetOffset - transform.position)
                                                       .magnitude /
                                                       m_searchRange);
            m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity + m_direction * m_guidanceIntensity,
                m_rigidbody.velocity.magnitude);

            if (m_rigidbody.velocity != Vector3.zero)
                transform.LookAt(m_rigidbody.velocity);
        }
    }
}