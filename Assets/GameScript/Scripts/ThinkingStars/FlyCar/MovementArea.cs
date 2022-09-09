using UnityEngine;

namespace ThinkingStars.FlyCar
{
    public class MovementArea : MonoBehaviour
    {
        public Transform aimTarget;
        public float speedLookAt = 1;
        private SphereCollider _sphereCollider;

        private void Awake()
        {
            _sphereCollider = GetComponent<SphereCollider>();
        }

        private void Update()
        {
            RotationLook();
        }

        private void RotationLook()
        {
            aimTarget.parent.position = Vector3.zero;
            aimTarget.localPosition = new Vector3(0, 0, 1);
            transform.rotation = Quaternion.RotateTowards(transform.rotation,
                Quaternion.LookRotation(aimTarget.position), Mathf.Deg2Rad * speedLookAt * Time.deltaTime);
        }
    }
}