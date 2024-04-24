using ProjectG.Enemies.Handler;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

namespace ProjectG.Items
{
    public class InventoryHandler : MonoBehaviour
    {
        public RawImage[] slots = new RawImage[4];
        public Item[] playersItems;
        public KeyCode[] slotCodes = new KeyCode[4];
        public int currentItemIndex;
        public GameObject itemRoot;

        public int numberOfItems;
        public NavMeshSurface surface;
        public Item[] allItems;
        public Transform allItemRoot;

        private void Start()
        {
            currentItemIndex = 0;
            spawnItems();
            updateIcons();
        }

        private void Update()
        {
            for (int i = 0; i < slotCodes.Length; i++)
            {
                if (Input.GetKeyDown(slotCodes[i]))
                {
                    currentItemIndex = i;
                    updateObject();
                    updateCurrentItem(slots[i].transform);
                }
            }

            if (Input.GetAxis("Fire1") == 1 && playersItems[currentItemIndex].usingItem)
            {
                Debug.Log("fire");
                GetComponent<itemUsageMethods>().SendMessage(playersItems[currentItemIndex].itemUseMethod);
                playersItems[currentItemIndex] = null;
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
                    slots[i].texture = null;
                }
            }
        }

        public void recievedItem(Item item)
        {
            playersItems[currentItemIndex] = item;
            updateIcons();
        }

        public void updateObject()
        {
            if (itemRoot.transform.childCount > 0)
            {
                Destroy(itemRoot.transform.GetChild(0).gameObject);
            }

            if (playersItems[currentItemIndex] != null)
            {
                Instantiate(playersItems[currentItemIndex].model, itemRoot.transform);
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
                Instantiate(allItems[Random.Range(0, allItems.Length)].model, spawner.RandomPointOnNavMesh(), Quaternion.identity ,allItemRoot);
            }
        }
    }
}
