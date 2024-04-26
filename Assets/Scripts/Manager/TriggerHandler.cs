using ProjectG;
using ProjectG.Enemies.Enemy;
using ProjectG.Items;
using UnityEngine;

namespace ProjectG.Manger
{
    public class TriggerHandler : MonoBehaviour
    {
        public GameManager GameManager;
        private RoadBlocker RoadBlocker;

        private void Start()
        {
            GameManager = GameObject.FindWithTag("GameManagerTag").GetComponent<GameManager>();
            RoadBlocker = GameObject.FindWithTag("Main Road").GetComponent<RoadBlocker>();
        }

        [SerializeField] bool enableStay = false;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                GameManager.HandleTriggerEnter(gameObject.name);
            }
            if (other.CompareTag("Player") && enableStay && this.gameObject.name == "Biestro Trigger")
            {
                GameManager.isInBiestro = true;
            }
            if (other.CompareTag("Player") && this.gameObject.tag == "Main Road")
            {
                RoadBlocker.MainRoadTriggerEnter();
            }
            if (other.CompareTag("Player") && this.gameObject.tag == "MainRoadTruck")
            {
                RoadBlocker.MainRoadTruckTriggerEnter();
            }
            if (other.CompareTag("Player") && this.gameObject.tag == "EnemyVision")
            {
                Debug.Log("Seen Player");
                transform.GetComponentInParent<PlayerDetection>().inView = true;
            }
            if (other.CompareTag("PlayerNoise") && this.gameObject.tag == "Enemy")
            {
                Debug.Log("Heard Player");
                transform.GetComponentInParent<PlayerDetection>().inHearing = true;
            }
            if (this.gameObject.tag == "Enemy")
            {
                Debug.Log(other.gameObject.name);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && this.gameObject.tag == "EnemyVision")
            {
                transform.GetComponentInParent<PlayerDetection>().inView = false;
            }
        }
    }
}
