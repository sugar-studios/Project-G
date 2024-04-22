using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace ProjectG.Enemies.Enemy
{
    public class taserShoot : MonoBehaviour
    {
        public ParticleSystem particle1;
        public ParticleSystem particle2;

       
        public void shoot()
        {
            particle1.gameObject.SetActive(false);
            particle2.gameObject.SetActive(false);

            StartCoroutine(turnOffParticles());
        }

        IEnumerator turnOffParticles()
        {

            //yield on a new YieldInstruction that waits for 5 seconds.
            yield return new WaitForSeconds(1);

            //After we have waited 5 seconds print the time again.
            particle1.gameObject.SetActive(true);
            particle2.gameObject.SetActive(true);

        }
    }
}
