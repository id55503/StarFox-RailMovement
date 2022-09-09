using System;
using Cinemachine;
using DG.Tweening;
using UnityEditor;
using UnityEngine;

namespace GameScript.Scripts.ThinkingStars.Launcher
{
    public class EnemyCarMovement : MonoBehaviour
    {
        public Transform carModel;
        public float speedHV;
        public float horizontalRotationLimit;
        public Transform playerCar;
        public float searchAngle = 90;
        public float searchRange = 10;
        [TagField] public string targetTag;
        public bool isMuzzle = true;
        public Transform toPoint;
        public Transform cube;

        private MovementArea _movementArea;
        private float _targetDistance;

        public Action<EnemyCarMovement> onDead;


        private void Start()
        {
            EnemyCarManager.RegisterEnemyCar(this);
        }

        private void Update()
        {
            if (isMuzzle)
            {
                // var forward = transform.forward;
                // var playerProjectOn = Vector3.ProjectOnPlane(playerCar.transform.position, forward);
                // var selfProjectOn = Vector3.ProjectOnPlane(transform.position, forward);
                var playerLocal = transform.InverseTransformPoint(playerCar.transform.position);
                if (playerLocal.magnitude < 0.05)
                {
                    return;
                }
                Move(playerLocal.x, playerLocal.y, 0);
                cube.transform.localPosition = playerLocal;
                var target = FindNewTarget();
                // Debug.Log(target);
            }
            else
            {
                var playerLocal = toPoint.localPosition - transform.localPosition;
                if (playerLocal.magnitude < 0.05)
                {
                    HorizontalRotation(carModel, 0, horizontalRotationLimit, 0.1f);
                    return;
                }
                playerLocal = playerLocal.normalized;
                Move(playerLocal.x, playerLocal.y, playerLocal.z);

                // _tweener = transform.DOLocalMove(originPoint.localPosition, toPointSpeed);

                // transform.localPosition = originPoint.localPosition;
            }
        }

        private void OnDisable()
        {
            onDead?.Invoke(this);
        }


        public void Move(float horizontal, float vertical, float forward)
        {
            LocalMove(horizontal, vertical, forward);
            HorizontalRotation(carModel, horizontal, horizontalRotationLimit, 0.1f);
        }

        private void LocalMove(float x, float y, float z)
        {
            transform.localPosition += new Vector3(x, y, z) * (speedHV * Time.deltaTime);
        }

        private void HorizontalRotation(Transform model, float axis, float limit, float lerpTime)
        {
            Vector3 targetEulerAngels = model.localEulerAngles;
            model.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y,
                Mathf.LerpAngle(targetEulerAngels.z, -axis * limit, lerpTime));
        }

        private bool IsWithinRange(Vector3 Coordinates)
        {
            if (Vector3.Distance(Coordinates, transform.position) < _targetDistance
                && Vector3.Angle(transform.forward, Coordinates - transform.position) < searchAngle / 2)
                return true;

            return false;
        }

        protected Transform FindNewTarget()
        {
            _targetDistance = float.MaxValue;
            foreach (Collider newTarget in Physics.OverlapSphere(transform.position, searchRange))
            {
                if (newTarget.gameObject.CompareTag(targetTag) && IsWithinRange(newTarget.transform.position))
                {
                    _targetDistance = Vector3.Distance(newTarget.transform.position, transform.position);
                    return newTarget.transform;
                }
            }

            return null;
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Handles.color = Color.magenta;
            Handles.DrawSolidArc(transform.position, Quaternion.AngleAxis(90, -transform.right) * transform.forward,
                Quaternion.AngleAxis(-searchAngle / 2, transform.up) * transform.forward, searchAngle,
                searchRange);
            Handles.DrawSolidArc(transform.position, transform.forward,
                Quaternion.AngleAxis(-searchAngle / 2, transform.up) * transform.forward, 360, searchRange);
        }
#endif
    }
}