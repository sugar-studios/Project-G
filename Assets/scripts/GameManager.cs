using TMPro;
using Unity.VisualScripting;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public GameObject playerPrefab;
    public bool inGameScene;

    public GameObject player;
    public GameObject biestro13;
    public GameObject adminOffice;
    public GameObject GameUI;
    public GameObject map;
    public Transform[] spawnPoints;
    public Transform[] returnPoints;

    public MeterGauge birdMeter;

    public bool inBiestro = false;

    public int score = 0;

    public GameUIManager UIManager;



    private Transform BiestroSpawn;

    public MealTriggers exitBiestroTrigger;
    public MealTriggers mealReceiveTrigger;
    public MealTriggers mealDeliverTrigger;

    private TextMeshProUGUI console;

    private bool isReceiveTriggerActive = true;
    private bool gotBiestroObjects = false;
    private bool gotGameObjects = false;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {

        /*
        //get triggers
        exitBiestroTrigger = biestro13.transform.GetChild(1).gameObject;
        mealDeliverTrigger = adminOffice.transform.GetChild(0).gameObject;

        //spawn player
        player.transform.position = BiestroSpawn.transform.position;

        //deactivate the deleiver trigger
        mealDeliverTrigger.GetComponent<Collider>().enabled = false;
        exitBiestroTrigger.SetActive(false);

        //Enital message
        console = GameUI.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        UIManager.TypeText(console, "Administrion ordered their lunch! Go!", 2.5f);

        UIManager.UpdateScore(score);
        */

        GetObjects();
    }

    private void FindObjectsByTag(string tag, ref Transform[] array, string errorMessage)
    {
        GameObject tagObject = GameObject.FindGameObjectWithTag(tag);
        if (tagObject != null)
        {
            int count = tagObject.transform.childCount;
            if (count > 0)
            {
                array = new Transform[count];
                for (int i = 0; i < count; i++)
                {
                    array[i] = tagObject.transform.GetChild(i);
                }
            }
            else
            {
                Debug.LogError("No objects found under the '" + tag + "' GameObject.");
            }
        }
        else
        {
            Debug.LogError("No GameObject found with the tag '" + tag + "'.");
        }
    }

    private void InstantiateMealTrigger(GameObject biestro, int childIndex, ref MealTriggers mealTrigger, bool active)
    {
        mealTrigger = new MealTriggers();
        mealTrigger.thisGameObject = biestro.transform.GetChild(childIndex).gameObject;
        mealTrigger.setTriggerCollider();
        mealTrigger.active = active;
    }

    public void GetObjects()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        GameUI = GameObject.FindGameObjectWithTag("UI canvas");
        UIManager = GameUI.GetComponent<GameUIManager>();

        if (!inGameScene)
        {
            GameObject biestroObject = GameObject.FindGameObjectWithTag("Biestro");

            if (biestroObject != null)
            {
                InstantiateMealTrigger(biestroObject, 0, ref mealReceiveTrigger, true);
                InstantiateMealTrigger(biestroObject, 1, ref exitBiestroTrigger, false);
                gotBiestroObjects = true;
            }
            else
            {
                Debug.LogError("No GameObject found with the tag 'Biestro'.");
            }
        }
        else
        {
            GameObject mapObject = GameObject.FindGameObjectWithTag("Map").transform.GetChild(0).gameObject;

           
            if (mapObject != null)
            {
                InstantiateMealTrigger(mapObject, 0, ref mealReceiveTrigger, true);
            }
            else
            {
                Debug.LogError("No GameObject found with the tag 'Map'.");
            }

            FindObjectsByTag("Spawns", ref spawnPoints, "No spawn points found under the 'Spawns' GameObject.");
            FindObjectsByTag("Exits", ref returnPoints, "No return points found under the 'Exits' GameObject.");
            GameObject.FindGameObjectWithTag("Exits").SetActive(false);
        }
    }




    /*
    private void Update()
    {
        if (inBiestro)
        {
            birdMeter.paused = true;
        }
        else
        {
            birdMeter.paused = false;
        }
    }
    */

    public void HandleTriggerEnter(string triggerName)
    {
        if (triggerName == mealReceiveTrigger.thisGameObject.name)
        {
            UIManager.ClearAndStopTyping(console);
            UIManager.TypeText(console, "Recieved Meal", 2.5f);
            
            mealReceiveTrigger.active = false;
            mealReceiveTrigger.activateTrigger(mealReceiveTrigger.active);

            mealDeliverTrigger.active = true;

            exitBiestroTrigger.active = true;
            exitBiestroTrigger.activateTrigger(exitBiestroTrigger.active);
        }
        else if (triggerName == mealDeliverTrigger.thisGameObject.name)
        {
            UIManager.ClearAndStopTyping(console);
            UIManager.TypeText(console, "Delivered Meal", 2.5f);

            mealDeliverTrigger.active = false;
            mealDeliverTrigger.activateTrigger(mealDeliverTrigger.active);

            mealReceiveTrigger.active = true;

            score ++;

            UIManager.UpdateScore(score);

            mealReceiveTrigger.active = true;
        }
        else if (triggerName == exitBiestroTrigger.thisGameObject.name && !mealReceiveTrigger.active)
        {
            Debug.Log("Exit Biestro");
            Debug.Log("Player 1: " + player.transform.position);

            Transform selectedPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            Vector3 spawnPosition = selectedPoint.position;

            Debug.Log("New spawn position: " + spawnPosition);

            player.SetActive(false);
            player.transform.position = spawnPosition;
            player.SetActive(true);

            Debug.Log("Player 2: " + player.transform.position);

            inBiestro = false;
            exitBiestroTrigger.active = false;
            exitBiestroTrigger.activateTrigger(exitBiestroTrigger.active);
        }
    }

    public void UpdateStamina(float num)
    {
        Slider slider = GameUI.transform.GetChild(3).transform.GetChild(0).GetComponent<Slider>();
        Image sliderFill = slider.transform.GetChild(1).transform.GetChild(0).GetComponent<Image>();
        slider.value = num;

        Color green = new Color(15f / 255, 108f / 255, 2f / 255);
        Color yellow = Color.yellow; 
        Color red = new Color(193f / 255, 22f / 255, 0f / 255);

        float minValue = slider.minValue;
        float maxValue = slider.maxValue;
        float midValue = (maxValue + minValue) / 2;

        if (num >= midValue)
        {
            float t = (num - midValue) / (maxValue - midValue);
            sliderFill.color = Color.Lerp(yellow, green, t);
        }
        else
        {
            float t = (num - minValue) / (midValue - minValue);
            sliderFill.color = Color.Lerp(red, yellow, t);
        }
    }

    public void SetMaxStamina(float num, float num2)
    {
        GameUI.transform.GetChild(3).transform.GetChild(0).GetComponent<Slider>().maxValue = num;
        GameUI.transform.GetChild(3).transform.GetChild(0).GetComponent<Slider>().minValue = num2;
    }
}

    public class MealTriggers
    {
        public GameObject thisGameObject;
        public Collider collider;
        public bool active = false;

        public void activateTrigger(bool Bool)
        {
            active = Bool;
            thisGameObject.SetActive(active);
        }

        public void setTriggerCollider()
        { 
            collider = thisGameObject.GetComponent<Collider>();
        }

        public void checkStatus()
        {
            Debug.Log(active);
        }
    }
