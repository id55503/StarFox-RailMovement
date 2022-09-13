//Create by DongLei

using NodeCanvas.Framework;
using ParadoxNotion.Design;

namespace ThinkingStars.FlyCar.AI.NodeCanvas
{
    [Category("GroupAI")]
    public class AssignEnemyCarToAgent : ActionTask
    {
        private GroupAI _groupAI;

        protected override string OnInit()
        {
            _groupAI = agent.gameObject.GetComponent<GroupAI>();
            return null;
        }

        protected override void OnExecute()
        {
            _groupAI.AssignEnemyCar();
            EndAction(true);
        }
    }
}