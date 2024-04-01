using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public GameObject playerPrefab;

    public GameObject biestro13;

    public GameObject adminOffice;


    // Start is called before the first frame update
    void Start()
    {
        GameObject player = Instantiate(playerPrefab, biestro13.transform.GetChild(0).transform.position, Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
