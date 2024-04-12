using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    //public GameObject playerPrefab;
    public GameObject player;
    public GameObject biestro13;
    public GameObject adminOffice;
    public GameObject GameUI;
    public Transform[] mainSpawnPoints;

    public MeterGauge birdMeter;

    public bool inBiestro = false;

    public int score = 0;

    public GameUIManager UIManager;



    private Transform BiestroSpawn;

    private GameObject exitBiestroTrigger;
    private GameObject mealReceiveTrigger;
    private GameObject mealDeliverTrigger;

    private TextMeshProUGUI console;

    private bool isReceiveTriggerActive = true;

    void Start()
    {
        /*
        try
        {
            GameObject player = Instantiate(playerPrefab, biestro13.transform.GetChild(0).transform.position, Quaternion.identity);
        }
        catch { }
        */

        //get triggers
        exitBiestroTrigger = biestro13.transform.GetChild(1).gameObject;
        mealReceiveTrigger = biestro13.transform.GetChild(0).gameObject;
        mealDeliverTrigger = adminOffice.transform.GetChild(0).gameObject;

        //get spawn point
        BiestroSpawn = biestro13.transform.GetChild(2).transform;
        player.transform.position = BiestroSpawn.transform.position;

        //deactivate the deleiver trigger
        mealDeliverTrigger.GetComponent<Collider>().enabled = false;
        exitBiestroTrigger.SetActive(false);

        //Enital message
        console = GameUI.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>();
        UIManager.TypeText(console, "Administrion ordered their lunch! Go!", 2.5f);

        UIManager.UpdateScore(score);
    }

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

    public void HandleTriggerEnter(string triggerName)
    {
        if (triggerName == mealReceiveTrigger.name && isReceiveTriggerActive)
        {
            UIManager.ClearAndStopTyping(console);
            UIManager.TypeText(console, "Recieved Meal", 2.5f);
            isReceiveTriggerActive = false;

            mealReceiveTrigger.GetComponent<Collider>().enabled = false;
            mealDeliverTrigger.GetComponent<Collider>().enabled = true;
            mealReceiveTrigger.GetComponent<MeshRenderer>().enabled = false;
            mealReceiveTrigger.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
            exitBiestroTrigger.SetActive(true);
        }
        else if (triggerName == mealDeliverTrigger.name && !isReceiveTriggerActive)
        {
            UIManager.ClearAndStopTyping(console);
            UIManager.TypeText(console, "Delivered Meal", 2.5f);

            score ++;

            UIManager.UpdateScore(score);

            isReceiveTriggerActive = true;

            mealReceiveTrigger.GetComponent<Collider>().enabled = true;
            mealReceiveTrigger.GetComponent<MeshRenderer>().enabled = true;
            mealReceiveTrigger.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
            mealDeliverTrigger.GetComponent<Collider>().enabled = false;
        }
        else if (triggerName == exitBiestroTrigger.name && !isReceiveTriggerActive)
        {
            Debug.Log("Exit Biestro");
            Debug.Log("Player 1: " + player.transform.position);

            Transform selectedPoint = mainSpawnPoints[Random.Range(0, mainSpawnPoints.Length)];
            Vector3 spawnPosition = selectedPoint.position;

            Debug.Log("New spawn position: " + spawnPosition);

            player.SetActive(false);
            player.transform.position = spawnPosition;
            player.SetActive(true);

            Debug.Log("Player 2: " + player.transform.position);

            inBiestro = false;
            exitBiestroTrigger.SetActive(false);
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
