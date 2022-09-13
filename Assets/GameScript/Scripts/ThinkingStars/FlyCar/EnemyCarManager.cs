//Create by DongLei

using System.Collections.Generic;
using UnityEngine;

namespace ThinkingStars.FlyCar
{
    public class EnemyCarManager : MonoBehaviour
    {
        private static EnemyCarManager instance;
        private List<EnemyCar> _enemyCars = new List<EnemyCar>();

        private void Awake()
        {
            if (instance != null)
            {
                Destroy(this);
            }

            instance = this;
        }

        public static void RegisterEnemyCar(EnemyCar enemyCar)
        {
            instance._enemyCars.Add(enemyCar);
            enemyCar.onDead = UnRegisterEnemyCar;
        }

        private static void UnRegisterEnemyCar(EnemyCar enemyCar)
        {
            instance._enemyCars.Remove(enemyCar);
        }

        public static List<EnemyCar> GetAllEnemyCars()
        {
            return instance._enemyCars;
        }
    }
}