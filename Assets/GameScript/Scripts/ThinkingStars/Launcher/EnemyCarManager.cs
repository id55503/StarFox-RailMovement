//Create by DongLei

using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace GameScript.Scripts.ThinkingStars.Launcher
{
    public class EnemyCarManager : MonoBehaviour
    {
        private static EnemyCarManager instance;
        private List<EnemyCarMovement> _enemyCarMovements = new List<EnemyCarMovement>();

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }

            instance = this;
        }

        public static void RegisterEnemyCar(EnemyCarMovement enemyCarMovement)
        {
            instance._enemyCarMovements.Add(enemyCarMovement);
            enemyCarMovement.onDead = UnRegisterEnemyCar;
        }

        private static void UnRegisterEnemyCar(EnemyCarMovement enemyCarMovement)
        {
            instance._enemyCarMovements.Remove(enemyCarMovement);
        }
        
    }
    
}