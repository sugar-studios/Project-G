using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectG.Player;

namespace ProjectG.Manger
{
    public class RoadBlocker : MonoBehaviour
    {
        public GameObject truck;
        public GameObject player;
        public GameObject explo;
        public GameObject explo2;
        public GameManager gManager;
        public float explosionForce = 7500f;
        public float truckSpeed = 50f;
        public GameObject CrashSound;
        [SerializeField]
        private bool moveTruck;

        private bool roadFire = false;
        private bool truckFire = false;

        // Start is called before the first frame update
        void Start()
        {
            truck.SetActive(false);
            moveTruck = false;
            player.GetComponent<PlayerMovement>().enabled = true;
            player.GetComponent<BoxCollider>().enabled = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (moveTruck)
            {
                truck.transform.position = new Vector3((truck.transform.position.x - truckSpeed * Time.deltaTime),
                                                        truck.transform.position.y,
                                                        truck.transform.position.z);
            }
        }

        public void MainRoadTriggerEnter()
        {
            if (!roadFire)
            {
                roadFire = true;
                player.GetComponent<PlayerMovement>().enabled = false;
                player.transform.GetChild(0).GetChild(1).GetComponent<Animator>().enabled = false;
                player.GetComponent<BoxCollider>().enabled = true;
                player.AddComponent<Rigidbody>();
                moveTruck = true;
                truck.SetActive(true);
                roadFire = true;
            }
        }

        public void MainRoadTruckTriggerEnter()
        {
            if (!truckFire)
            {
                truckFire = true;
                Vector3 exploPos = truck.transform.position;
                exploPos = new Vector3(exploPos.x, exploPos.y, exploPos.z - 10);
                explo.SetActive(true);
                explo2.SetActive(true);
                CrashSound.SetActive(true);
                player.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, exploPos, 100);

                // Draw explosion for debugging
                Debug.DrawLine(exploPos, exploPos + Vector3.up * 5, Color.red, 2f);
                Debug.DrawLine(exploPos, exploPos + Vector3.right * 5, Color.red, 2f);
                Debug.DrawLine(exploPos, exploPos + Vector3.forward * 5, Color.red, 2f);
                truckFire = true;
                explo.SetActive(false);
                explo2.SetActive(false);

                gManager.GameOver(true);

            }
        }
    }
}
