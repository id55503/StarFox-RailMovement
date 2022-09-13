//Create by DongLei

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace ThinkingStars.FlyCar.AI.NodeCanvas
{
    [Category("GroupAI")]
    public class EnemyCarMoveToPoint : ActionTask
    {
        public BBParameter<GameObject> enemyCarAgent;
        public BBParameter<Transform> targetPoint;
        private EnemyCar _enemyCar;

        protected override void OnExecute()
        {
            var gameObject = enemyCarAgent.GetValue();
            if (gameObject == null) return;
            _enemyCar = gameObject.GetComponent<EnemyCarAgent>().GetEnemyCar();
            _enemyCar.MoveToPoint(targetPoint.value);
        }

        protected override void OnUpdate()
        {
            if (_enemyCar.GetState() != EnemyCar.EnemyCarState.Move)
            {
                EndAction(true);
            }
        }
    }
}