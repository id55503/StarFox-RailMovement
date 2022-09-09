//Create by DongLei

using System;
using UnityEngine;

namespace ThinkingStars.Artillery.Interface
{
    public class GameLaserBeam : MonoBehaviour
    {
        public float lifeTime;

        public GameObject beamStartPrefab;
        public GameObject beamEndPrefab;
        public GameObject beamPrefab;

        [Header("Adjustable Variables")]
        public float beamEndOffset = 1f; //How far from the raycast hit point the end effect is positioned

        public float textureScrollSpeed = 8f; //How fast the texture scrolls along the beam
        public float textureLengthScale = 3; //Length of the beam texture

        private GameObject beamStart;
        private GameObject beamEnd;
        private GameObject beam;
        private LineRenderer line;
        private bool isRunning;

        public Action<GameLaserBeam> onDestroy;

        public void Fire(Vector3 start, Vector3 end)
        {
            if (isRunning) return;
            beamStart = Instantiate(beamStartPrefab, new Vector3(0, 0, 0), Quaternion.identity,transform) as GameObject;
            beamEnd = Instantiate(beamEndPrefab, new Vector3(0, 0, 0), Quaternion.identity,transform) as GameObject;
            beam = Instantiate(beamPrefab, new Vector3(0, 0, 0), Quaternion.identity,transform) as GameObject;
            line = beam.GetComponent<LineRenderer>();
            ShootBeamInDir(start, end);
            isRunning = true;
        }


        public void UnFire()
        {
            Destroy(gameObject);
        }

        public void ShootBeamInDir(Vector3 start, Vector3 end)
        {
            var dir = end - start;
            line.positionCount = 2;
            line.SetPosition(0, start);
            beamStart.transform.position = start;


            RaycastHit hit;
            if (Physics.Raycast(start, dir, out hit))
            {
                end = hit.point - (dir.normalized * beamEndOffset);
            }

            beamEnd.transform.position = end;
            line.SetPosition(1, end);

            beamStart.transform.LookAt(beamEnd.transform.position);
            beamEnd.transform.LookAt(beamStart.transform.position);

            float distance = Vector3.Distance(start, end);
            line.sharedMaterial.mainTextureScale = new Vector2(distance / textureLengthScale, 1);
            line.sharedMaterial.mainTextureOffset -= new Vector2(Time.deltaTime * textureScrollSpeed, 0);
        }

        private void Update()
        {
            lifeTime -= Time.deltaTime;
            if (lifeTime < 0)
            {
                UnFire();
            }
        }

        private void OnDestroy()
        {
            onDestroy?.Invoke(this);
            Destroy(beamStart);
            Destroy(beamEnd);
            Destroy(beam);
            Destroy(line);
        }
    }
}