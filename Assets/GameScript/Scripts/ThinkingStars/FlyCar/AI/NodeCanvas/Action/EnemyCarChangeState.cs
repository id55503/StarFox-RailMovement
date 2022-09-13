//Create by DongLei

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace ThinkingStars.FlyCar.AI.NodeCanvas
{
    [Category("GroupAI")]
    public class EnemyCarChangeState: ActionTask
    {
        public BBParameter<GameObject> enemyCarAgent;
        public EnemyCar.EnemyCarState enemyCarState;

        protected override void OnExecute()
        {
            var enemyCar = enemyCarAgent.value.GetComponent<EnemyCarAgent>().GetEnemyCar();
            enemyCar.SetState(enemyCarState);
            EndAction(true);
        }
    }
}