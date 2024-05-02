using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProjectG.Items;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.Rendering;
using System;

namespace ProjectG.UI
{
    public class TutorialItemDisplay : MonoBehaviour
    {
        public Item[] items;
        public List<Image> icons;
        public GameObject grid;
        public Image gridIconPrefab;
        public TextMeshProUGUI desc;
        public Transform obj;
        public byte rotationSpeed;

        void Start()
        {
            icons.Clear();
            for (int i = 0; i < items.Length; i++)
            {
                Image newIcon = Instantiate(gridIconPrefab, grid.transform);
                newIcon.name = items[i].name;
                icons.Add(newIcon);

                Sprite sprite = Sprite.Create((Texture2D)items[i].icon, new Rect(0, 0, items[i].icon.width, items[i].icon.height), Vector2.zero);
                icons[i].sprite = sprite;
                icons[i].SetNativeSize();

                if (icons[i].GetComponent<TutorialItemDesc>() == null)
                {
                    icons[i].AddComponent<TutorialItemDesc>();
                }
                icons[i].GetComponent<TutorialItemDesc>().tTD = this.GetComponent<TutorialItemDisplay>();
                icons[i].GetComponent<TutorialItemDesc>().item = items[i];
            }
        }

        public void UpdateDesc(string txt)
        {
            ClearModel();
            desc.text = txt;
        }

        public void UpdateModel(GameObject itemPrefab)
        {
            ClearModel();
            Instantiate(itemPrefab, obj.position, obj.rotation, obj);
        }

        public void ClearModel()
        {
            for (int i = 0; i < obj.transform.childCount; i++)
            {
                try
                {
                    Destroy(obj.transform.GetChild(i).gameObject);
                }
                catch
                {
                    Debug.LogWarning("Missing Index on Clear Model");
                }
            }
        }


        void Update()
        {
            obj.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
        }
    }
}
