using System;
using System.Collections.Generic;
using GameScript.Scripts.ThinkingStars.Artillery.Interface;
using UnityEngine;

namespace GameScript.Scripts.ThinkingStars.Artillery
{
    public class Gun : MonoBehaviour
    {
        public GameBullet bulletPrefab;
        public float initSpeed;
        public float coolTime;
        public GameObject owner;
        public float lifeTime;

        private float fireTimeCount;

        public void Fire()
        {
            if (fireTimeCount < coolTime) return;
            var projectile = Instantiate(bulletPrefab, transform.position,transform.rotation);
            // projectile.transform.localPosition = Vector3.zero;
            projectile.initSpeed = initSpeed;
            projectile.lifeTime = lifeTime;
            projectile.Fire(owner);
            fireTimeCount = 0;
        }

        private void Update()
        {
            fireTimeCount += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Fire();
            }
            
        }
    }
}