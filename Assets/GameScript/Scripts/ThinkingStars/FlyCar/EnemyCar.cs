using UnityEngine;

namespace ThinkingStars.FlyCar
{
    public class EnemyCar : MonoBehaviour
    {
        private enum EnemyCarState
        {
            Ready,
            Idle,
            Muzzle,
            Fire,
            Fail,
            Destroy
        }

        [SerializeField] private EnemyCarMovement enemyCarMovement;
        [SerializeField] private EnemyCarState state;
    }
}