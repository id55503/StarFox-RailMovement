//Create by DongLei

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine;

namespace ThinkingStars.FlyCar.AI.NodeCanvas.Condition
{
    [Category("GroupAI")]
    public class CheckEnemyCarState : ConditionTask
    {
        public BBParameter<EnemyCarAgent> enemyCarAgent;
        public EnemyCar.EnemyCarState enemyCarState;

        protected override bool OnCheck()
        {
            var enemyCar = enemyCarAgent.value.GetEnemyCar();
            return enemyCar.GetState() == enemyCarState;
        }
    }
}