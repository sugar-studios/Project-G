using ProjectG;
using UnityEngine;

namespace ProjectG.Manger
{
    public class TriggerHandler : MonoBehaviour
    {
        public GameManager GameManager;
        private RoadBlocker RoadBlocker;

        private void Start()
        {
            RoadBlocker = GameObject.FindWithTag("Main Road").GetComponent<RoadBlocker>();
            Debug.Log(GameObject.FindWithTag("Main Road").name);
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
                Debug.Log("Hit Road");
                RoadBlocker.MainRoadTriggerEnter();
            }
            if (other.CompareTag("Player") && this.gameObject.tag == "MainRoadTruck")
            {
                Debug.Log("Hit Car");
                RoadBlocker.MainRoadTruckTriggerEnter();
            }
        }
    }
}
