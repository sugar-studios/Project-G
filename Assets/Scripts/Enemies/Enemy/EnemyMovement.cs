using ProjectG.Audio;
using ProjectG.Player;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace ProjectG.Enemies.Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        private PlayerDetection PlayerDetection;
        public NavMeshAgent navMeshAgent;
        public float roamingRange;
        public taserShoot taser;
        public Animator guardAnimator;
        public string chase = "none";

        private AudioManager aM;
        private float timeBetweenShoots;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            PlayerDetection = GetComponent<PlayerDetection>();

            try
            {
                aM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            }
            catch
            {
                aM = null;
            }
        }

        private void setAnimator()
        {
            guardAnimator.SetFloat("velocityY", navMeshAgent.velocity.y);
            guardAnimator.SetFloat("velocityX", navMeshAgent.velocity.x);
        }

        private void Start()
        {
            setAnimator();
        }

        private void Update()
        {
            setAnimator();

            if (PlayerDetection.playerInSightTrigger && PlayerDetection.seePlayer)
            {
                FaceTarget(PlayerDetection.player.position);
                ChasePlayer();
            }
            else if (PlayerDetection.inHearing)
            {
                HeardPlayer();
            }
            else if (!PlayerDetection.seePlayer && chase != "none")
            {
                StartCoroutine(LostSightOfPlayer());
            }
            else
            {
                Roam();
            }

            taser.particle1.gameObject.SetActive(guardAnimator.GetFloat("shooting") == 1);
            taser.particle2.gameObject.SetActive(guardAnimator.GetFloat("shooting") == 1);
        }

        private IEnumerator LostSightOfPlayer()
        {
            yield return new WaitForSeconds(3);
            chase = "none";
            Roam();
        }


        private void Roam()
        {
            if (!navMeshAgent.hasPath || navMeshAgent.remainingDistance < 0.5f)
            {
                Vector3 randomPosition = Random.insideUnitSphere * roamingRange;
                NavMeshHit hit;
                if (NavMesh.SamplePosition(transform.position + randomPosition, out hit, roamingRange, NavMesh.AllAreas))
                {
                    navMeshAgent.SetDestination(hit.position);
                }
            }
        }

        private void HeardPlayer()
        {
            navMeshAgent.SetDestination(PlayerDetection.player.position);
            PlayerDetection.OverrideHearingOff();
        }

        private void ChasePlayer()
        {
            // Face the player
            FaceTarget(PlayerDetection.player.position);

            // Set the destination to the player's position
            navMeshAgent.SetDestination(PlayerDetection.player.position);

            // If close enough and ready to shoot
            if (Vector3.Distance(transform.position, PlayerDetection.player.position) < 7)
            {
                if (timeBetweenShoots > 1 && navMeshAgent.remainingDistance < 0.4f)
                {
                    guardAnimator.SetFloat("shooting", 1);
                    AttackPlayer();
                }
                else
                {
                    guardAnimator.SetFloat("shooting", 0);
                }
            }
            else
            {
                guardAnimator.SetFloat("shooting", 0);
            }

            timeBetweenShoots += Time.deltaTime;
        }

        private void AttackPlayer()
        {
            PlayerDetection.player.GetComponent<PlayerMovement>().playerHealth -= 5;
            PlayerDetection.player.GetComponent<PlayerMovement>().updateHealth();
            if (aM != null)
            {
                aM.Play("Player Hit");
                aM.Play("Taser");
            }
            timeBetweenShoots = 0;
        }

        private void FaceTarget(Vector3 destination)
        {
            Vector3 lookPos = destination - transform.position;
            lookPos.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5 * Time.deltaTime);
        }
    }
}
