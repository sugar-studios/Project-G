using ProjectG.Debugging;
using ProjectG.Enemies;
using ProjectG.Enemies.Handler;
using ProjectG.UI;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.UI;

namespace ProjectG.Items
{
    public class InventoryHandler : MonoBehaviour
    {
        public RawImage[] slots = new RawImage[4];
        public Item[] playersItems;
        public Item blankItem;
        public KeyCode[] slotCodes = new KeyCode[4];
        public int currentItemIndex;
        public GameObject itemRoot;
        public PostProcessVolume ppv;
        public ColorGrading ppvcg;

        public int numberOfItems;
        public NavMeshSurface surface;
        public Item[] allItems;
        public Transform allItemRoot;
        public Texture2D alphaTexture;
        public Transform miniItemRoot;
        public GameObject miniItemPrefab;
        public GameObject cross;

        public MeterGauge BirdAttack;

        private void Start()
        {
            currentItemIndex = 0;
            spawnItems();
            //ppvcg.colorFilter = new Vector3(96f, 96f, 96f)
            updateIcons();
        }
        private void Awake()
        {
            updateIcons();
            SetupHolder();

        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Item")
            {
                playersItems[currentItemIndex] = other.gameObject.GetComponent<itemPickupContainer>().item;
            }
            //Debug.Log(other.gameObject.name);

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Item")
            {
                Debug.Log(other.transform.parent.name);
                Debug.Log(itemRoot.name);

                if (other.transform.parent.name != itemRoot.name)
                {

                    recievedItem(other.GetComponent<itemPickupContainer>().item);
                    Destroy(other.gameObject);
                }
            }
            //Debug.Log(other.name + other.tag);
        }

        private void Update()
        {
            //dont touch this
            PlayerStatesTester.PlayerNoiseRadius = Mathf.Lerp(PlayerStatesTester.PlayerNoiseRadius, 30, Time.deltaTime);
            //Debug.Log(PlayerStatesTester.PlayerNoiseRadius);

            for (int i = 0; i < slotCodes.Length; i++)
            {
                if (Input.GetKeyDown(slotCodes[i]))
                {
                    currentItemIndex = i;
                    updateObject();

                    //check if gun
                    if (playersItems[currentItemIndex] == allItems[2])
                    {
                        cross.SetActive(true);
                    }
                    else
                    {
                        cross.SetActive(false);
                    }

                    updateCurrentItem(slots[i].transform);
                }
            }

            if (Input.GetAxis("Fire1") == 1 && playersItems[currentItemIndex] != null)
            {
                if (playersItems[currentItemIndex].usingItem)
                {
                    GetComponent<itemUsageMethods>().SendMessage(playersItems[currentItemIndex].itemUseMethod);
                    Debug.Log(playersItems[currentItemIndex].itemUseMethod);


                    if (!playersItems[currentItemIndex].reuseable)
                    {
                        playersItems[currentItemIndex] = null;
                        playersItems[currentItemIndex] = blankItem;

                        Debug.Log($"destroyed 2: {itemRoot.transform.GetChild(0).name}");
                        //Destroy(itemRoot.transform.GetChild(0).gameObject);
                        turnOffAll();
                        playersItems[currentItemIndex] = blankItem;
                    }
                    updateIcons();
                }
                updateIcons();
            }
        }

        public void updateIcons()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                if (playersItems[i] != null)
                {
                    slots[i].texture = playersItems[i].icon;
                }
                else{
                    slots[i].texture = alphaTexture;
                }
            }
        }

        public void recievedItem(Item item)
        {
            playersItems[currentItemIndex] = item;
            updateIcons();
            updateObject();
        }

        public void updateObject()
        {
            /*
            if (itemRoot.transform.childCount == 1)
            {
                Debug.Log($"destroyed: {itemRoot.transform.GetChild(0).name}");

                Destroy(itemRoot.transform.GetChild(0).gameObject);
            }
            //Debug.Log(playersItems[currentItemIndex]);
            if (playersItems[currentItemIndex] != null)
            {
                if (playersItems[currentItemIndex].model != null && itemRoot.transform.childCount != 1)
                {
                    Debug.Log("BOOOOM   NEW MODEL");
                    Instantiate(playersItems[currentItemIndex].model, itemRoot.transform);
                }
            }*/

            for (int i = 0; i < itemRoot.transform.childCount; i++)
            {

                if (itemRoot.transform.GetChild(i).GetComponent<itemPickupContainer>().item.name == playersItems[currentItemIndex].name)
                {
                    itemRoot.transform.GetChild(i).gameObject.SetActive(true);

                }
                else
                {
                    itemRoot.transform.GetChild(i).gameObject.SetActive(false);

                }
            }


        }

        public void SetupHolder()
        {
            for(int i = 0; allItems.Length > i; i++)
            {
                if (allItems[i] != null)
                {
                    Instantiate(allItems[i].model, itemRoot.transform);
                }
                else
                {
                    Instantiate(blankItem.model, itemRoot.transform);
                }
            }

            turnOffAll();
        }
        public void turnOffAll()
        {
            for (int i = 0; i < itemRoot.transform.childCount; i++)
            {
                itemRoot.transform.GetChild(i).gameObject.SetActive(false);
            }
        }

        private void updateCurrentItem(Transform image)
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].transform.parent.parent.GetComponent<Image>().color = new Color(1, 1, 1, 0.4f);
            }

            Image currentImage = image.parent.parent.GetComponent<Image>();
            currentImage.color = new Color(1, 1, 1, 0.75f);
        }

        private void spawnItems()
        {
            SpawnHandler spawner = new SpawnHandler();
            spawner.surface = surface;

            for (int i = 0; i < numberOfItems; i++)
            {
                Vector3 point;
                spawner.spawnBounds.x = 200;
                spawner.spawnBounds.y = 100;

                if (spawner.RandomPoint(spawner.randomPoint(), spawner.range, out point))
                {
                    Vector3 itemPoint = point / 100;
                    Instantiate(allItems[Random.Range(0, allItems.Length)].model, point, Quaternion.identity ,allItemRoot);
                    Instantiate(miniItemPrefab, itemPoint, Quaternion.identity, miniItemRoot);

                }
                else
                {
                    Debug.Log("not valid point, item not spawning");
                }
            }
        }
    }
}
