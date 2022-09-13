//Create by DongLei

using UnityEngine;

namespace ThinkingStars.FlyCar.AI
{
    public class EnemyCarAgent : MonoBehaviour
    {
        [SerializeField] private EnemyCar enemyCar;

        public EnemyCar GetEnemyCar()
        {
            return enemyCar;
        }

        public void SetEnemyCar(EnemyCar enemyCar)
        {
            this.enemyCar = enemyCar;
        }
    }
}