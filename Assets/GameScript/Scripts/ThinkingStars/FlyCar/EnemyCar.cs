using System;
using ThinkingStars.Artillery;
using UnityEngine;

namespace ThinkingStars.FlyCar
{
    public class EnemyCar : MonoBehaviour
    {
        public enum EnemyCarState
        {
            Ready,
            Idle,
            Move,
            Muzzle,
            Fail,
            Destroy
        }

        [SerializeField] private EnemyCarMovement enemyCarMovement;
        [SerializeField] private EnemyCarState state;

        public Action<EnemyCar> onDead;


        public void SetState(EnemyCarState enemyCarState)
        {
            state = enemyCarState;
            enemyCarMovement.isMuzzle = state == EnemyCarState.Muzzle;
        }

        public EnemyCarState GetState()
        {
            return state;
        }

        public void MoveToPoint(Transform point)
        {
            SetState(EnemyCarState.Move);
            enemyCarMovement.isMuzzle = false;
            enemyCarMovement.toPoint = point;
        }


        private void Start()
        {
            EnemyCarManager.RegisterEnemyCar(this);
            enemyCarMovement.onMoveToPointArrived = OnMoveToPointArrived;
            SetState(EnemyCarState.Ready);
        }

        private bool OnMoveToPointArrived()
        {
            if (GetState() == EnemyCarState.Move)
            {
                enemyCarMovement.toPoint = null;
                SetState(EnemyCarState.Idle);
                return true;
            }

            return false;
        }

        public void Fire()
        {
            var guns = GetComponentsInChildren<Gun>();
            foreach (var gun in guns)
            {
                gun.Fire();
            }
        }

        private void OnDisable()
        {
            onDead?.Invoke(this);
        }
    }
}