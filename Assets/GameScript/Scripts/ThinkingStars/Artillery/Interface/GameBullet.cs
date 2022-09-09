using UnityEngine;

namespace ThinkingStars.Artillery.Interface
{
    public class GameBullet : MonoBehaviour
    {
        public SmartMissile smartMissile;
        public float initSpeed;
        public GameObject owner;
        public float lifeTime = 20;

        public void Fire(GameObject owner)
        {
            // var missile = Instantiate(smartMissile, transform);
            var gameSmartMissile = smartMissile as GameSmartMissile;
            if (gameSmartMissile != null)
            {
                gameSmartMissile.SetTargetTag(owner.tag);
            }
            smartMissile.GetComponent<Rigidbody>().AddForce(transform.forward * initSpeed, ForceMode.Impulse);
            Destroy(gameObject, lifeTime);
        }
    }
}