using Cinemachine;
using UnityEngine;

namespace ThinkingStars.FlyCar
{
    public class PlayerCarMovement : MonoBehaviour
    {
        public Transform carModel;
        public float speedHV;
        public float speedForward;
        public float speedLookAt;
        public float horizontalRotationLimit;
        public Transform aimTarget;
        public CinemachineDollyCart cinemachineDollyCart;

        private void SetSpeed(float dollyCartSpeed)
        {
            speedForward = dollyCartSpeed;
            cinemachineDollyCart.m_Speed = dollyCartSpeed;
        }
        
        private void Start()
        {
            SetSpeed(speedForward);
        }

        private void Update()
        {
            float h = Input.GetAxis("Horizontal");
            float v = Input.GetAxis("Vertical");
            ProcessCollision(ref h, ref v);
            LocalMove(h, v);
            // RotationLook(h, v);
            HorizontalRotation(carModel, h, horizontalRotationLimit, 0.1f);
        }

        private void LocalMove(float x, float y)
        {
            transform.localPosition += new Vector3(x, y, 0) * (speedHV * Time.deltaTime);
        }

        private void RotationLook(float h, float v)
        {
            aimTarget.parent.position = Vector3.zero;
            aimTarget.localPosition = new Vector3(h, v, 1);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speedLookAt * Time.deltaTime);
        }

        private void HorizontalRotation(Transform model, float axis, float limit, float lerpTime)
        {
            Vector3 targetEulerAngels = model.localEulerAngles;
            model.localEulerAngles = new Vector3(targetEulerAngels.x, targetEulerAngels.y,
                Mathf.LerpAngle(targetEulerAngels.z, -axis * limit, lerpTime));
        }

        #region collision

        public float maxReboundDuration = 0.1f;
        public float reboundVelocityFacter = 2;
        public Collider collider;
        private Vector3 _relativeVelocity;
        private float _duration;


        private void OnTriggerEnter(Collider other)
        {
            // if (!(_duration < 0)) return;
            Physics.ComputePenetration(collider, collider.transform.position, Quaternion.identity, other,
                other.transform.position, Quaternion.identity, out var resolutionDirection, out var resolutionDistance);
            //
            // var position = transform.position;
            // var closestPoint = other.ClosestPointOnBounds(position);
            _relativeVelocity = resolutionDirection * resolutionDistance * reboundVelocityFacter;
            _duration = maxReboundDuration;
        }

        private void ProcessCollision(ref float h, ref float v)
        {
            _duration -= Time.deltaTime;
            if (!(_duration > 0)) return;
            if (Camera.main != null)
            {
                var vector3 = Vector3.ProjectOnPlane(_relativeVelocity, Camera.main.transform.forward);
                // var vector3 = Camera.main.ViewportToScreenPoint(_relativeVelocity);
                h = vector3.x;
                v = vector3.y;
            }
        }

        #endregion
    }
}