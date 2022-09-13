using System.Collections.Generic;
using NodeCanvas.BehaviourTrees;
using UnityEngine;

namespace ThinkingStars.FlyCar.AI
{
    public class GroupAI : MonoBehaviour
    {
        [SerializeField] private BehaviourTreeOwner behaviourTreeOwner;
        private List<EnemyCarAgent> _enemyCarAgents = new List<EnemyCarAgent>();

        public EnemyCarAgent GetEnemyCarAgent(GameObject gameObject)
        {
            foreach (var enemyCarAgent in _enemyCarAgents)
            {
                if (enemyCarAgent.gameObject == gameObject)
                {
                    return enemyCarAgent;
                }
            }

            return null;
        }
        
        private void Awake()
        {
            var enemyCarAgents = GetComponentsInChildren<EnemyCarAgent>();
            foreach (var enemyCarAgent in enemyCarAgents)
            {
                _enemyCarAgents.Add(enemyCarAgent);
            }
        }

        public void AssignEnemyCar()
        {
            var enemyCars = EnemyCarManager.GetAllEnemyCars();
            var unAssignEnemyCars = new Queue<EnemyCar>();
            foreach (var enemyCar in enemyCars)
            {
                foreach (var enemyCarAgent in _enemyCarAgents)
                {
                    if (enemyCarAgent.GetEnemyCar() != null && enemyCar == enemyCarAgent.GetEnemyCar())
                    {
                        continue;
                    }

                    unAssignEnemyCars.Enqueue(enemyCar);
                }
            }

            foreach (var enemyCarAgent in _enemyCarAgents)
            {
                if (unAssignEnemyCars.Count == 0) break;
                if (enemyCarAgent.GetEnemyCar() == null)
                {
                    var enemyCar = unAssignEnemyCars.Dequeue();
                    enemyCarAgent.SetEnemyCar(enemyCar);
                }
            }
        }
    }
}