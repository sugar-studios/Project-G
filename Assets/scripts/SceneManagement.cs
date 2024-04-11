using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class SceneManagement : MonoBehaviour
{
    GameManager GameManager;
    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        if (SceneManager.GetActiveScene().name == "Biestro")
        {
            GameManager.inGameScene = false;
        }
        else if (SceneManager.GetActiveScene().name == "Game Scene")
        {
            GameManager.inGameScene = true;
        }
    }

}
