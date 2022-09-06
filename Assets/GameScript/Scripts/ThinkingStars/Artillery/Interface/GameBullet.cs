using UnityEngine;

namespace GameScript.Scripts.ThinkingStars.Artillery.Interface
{
    public class GameBullet : MonoBehaviour
    {
        public SmartMissile smartMissile;
        public float initSpeed;
        public GameObject owner;
        public float lifeTime = 20;

        public void Fire()
        {
            // var missile = Instantiate(smartMissile, transform);
            smartMissile.GetComponent<Rigidbody>().AddForce(transform.forward * initSpeed, ForceMode.Impulse);
            Destroy(gameObject, lifeTime);
        }
    }
}