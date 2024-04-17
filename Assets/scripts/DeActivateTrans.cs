using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DeActivateTrans : MonoBehaviour
{
    GameObject thisObject;

    public bool sceneLoad;

    private void Start()
    {
        thisObject = gameObject;
    }

    private void Awake()
    {
        sceneLoad = false;
    }
    public void Deactivate()
    { 
        sceneLoad=true;
        thisObject.SetActive(false);
    }
    public void True()
    {
        sceneLoad = true;
    }
}
