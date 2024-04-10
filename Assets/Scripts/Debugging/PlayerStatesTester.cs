using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ProjectG.Debugging
{
    public class PlayerStatesTester : MonoBehaviour
    {
        public bool makingNoise;
        public static float PlayerNoiseRadius;

        public bool alertedSecurity;

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, PlayerNoiseRadius);
        }
    }
   
}
