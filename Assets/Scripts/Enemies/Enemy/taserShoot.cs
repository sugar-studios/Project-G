using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Enemies.Enemy
{
    public class taserShoot : MonoBehaviour
    {
        public ParticleSystem particle1;
        public ParticleSystem particle2;

       
        public void shoot()
        {
            particle1.Play();
            particle2.Play();
        }
    }
}
