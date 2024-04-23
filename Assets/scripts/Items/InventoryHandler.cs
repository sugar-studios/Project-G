using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

        private void Update()
        {
            for (int i = 0; i < slotCodes.Length; i++)
            {
                if (Input.GetKeyDown(slotCodes[i]))
                {
                    currentItemIndex = i;

                    updateObject();
                }
            }
        }

        public void updateIcons()
        {
            for (int i = 0; i < slots.Length; i++)
            {
                slots[i].texture = playersItems[i].icon;
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
    }
}
