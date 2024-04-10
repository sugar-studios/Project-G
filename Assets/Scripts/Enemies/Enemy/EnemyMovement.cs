using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectG.Enemies.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private PlayerDetection PlayerDetection;
        private NavMeshAgent navMeshAgent;
        public float roamingDistance;
        public float roamingRange;

        private void Start()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            PlayerDetection = GetComponent<PlayerDetection>();
        }

        private void Update()
        {

            if (PlayerDetection.inHearing)
            {
                HeardPlayer();
            }
            else
            {
                roaming();
            }
        }

        private void roaming()
        {
            if (!PlayerDetection.seePlayer)
            {
                if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance < 0.5f)
                {
                    Vector3 randomPosition = Random.insideUnitSphere * roamingRange;
                    NavMeshHit hit;
                    NavMesh.SamplePosition(transform.position + randomPosition, out hit, roamingRange, NavMesh.AllAreas);
                    navMeshAgent.SetDestination(hit.position);
                }
            }
        }
        private void HeardPlayer()
        {
            if (PlayerDetection.inHearing && !PlayerDetection.seePlayer)
            {
                navMeshAgent.SetDestination(PlayerDetection.player.position);
                PlayerDetection.overrideHeardingOff = true;
            }
        }
        private void attackPlayer()
        {

        }
    }
}
