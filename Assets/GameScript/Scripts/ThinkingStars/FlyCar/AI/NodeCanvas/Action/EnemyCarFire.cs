//Create by DongLei

using NodeCanvas.Framework;
using ParadoxNotion.Design;
using ThinkingStars.Artillery;
using UnityEngine;

namespace ThinkingStars.FlyCar.AI.NodeCanvas
{
    [Category("GroupAI")]
    public class EnemyCarFire: ActionTask
    {
        public BBParameter<GameObject> enemyCarAgent;

        protected override void OnExecute()
        {
            var enemyCar = enemyCarAgent.value.GetComponent<EnemyCarAgent>().GetEnemyCar();
            var guns = enemyCar.gameObject.GetComponentsInChildren<Gun>();
            foreach (var gun in guns)
            {
                gun.Fire();
            }
            EndAction(true);
        }
    }
}