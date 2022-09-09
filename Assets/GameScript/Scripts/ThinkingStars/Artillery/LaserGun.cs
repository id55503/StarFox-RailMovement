//Create by DongLei

using System.Collections.Generic;
using DG.Tweening;
using ThinkingStars.Artillery.Interface;
using UnityEngine;

namespace ThinkingStars.Artillery
{
    public class LaserGun : MonoBehaviour
    {
        public GameLaserBeam gameLaserBeam;
        public float laserLength;
        public float flowDuration;
        public Transform target;

        private List<GameLaserBeam> _gameLaserBeams = new List<GameLaserBeam>();


        private void FixedUpdate()
        {
            if (target != null)
            {
                transform.DOLookAt(target.transform.position, flowDuration);
            }
            foreach (var laserBeam in _gameLaserBeams)
            {
                laserBeam.ShootBeamInDir(transform.position,
                    transform.position + transform.forward * laserLength);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                var laserBeam = Instantiate(gameLaserBeam, transform);
                laserBeam.onDestroy = (laser) => { _gameLaserBeams.Remove(laser); };
                laserBeam.transform.localPosition = Vector3.zero;
                laserBeam.transform.rotation = Quaternion.identity;
                laserBeam.Fire(transform.position, transform.position + transform.forward * laserLength);
                _gameLaserBeams.Add(laserBeam);
            }
        }
    }
}