using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject map;
    public GameObject biestro13;
    public GameObject adminOffice;
    public GameObject GameUI;
    public Transform[] mainSpawnPoints;
    public GameObject[] exitPoints;
    public GameObject Transition;

    public MeterGauge birdMeter;

    public int score = 0;

    public bool isInBiestro = true;

    public GameUIManager UIManager;

    private Transform biestroSpawn;

    private GameObject exitBiestroTrigger;
    private GameObject mealReceiveTrigger;
    private GameObject mealDeliverTrigger;

    private TextMeshProUGUI console;

    private bool isReceiveTriggerActive = true;

    void Start()
    {
        exitBiestroTrigger = biestro13.transform.GetChild(1).gameObject;
        mealReceiveTrigger = biestro13.transform.GetChild(0).gameObject;
        mealDeliverTrigger = adminOffice.transform.GetChild(0).gameObject;

        biestroSpawn = biestro13.transform.GetChild(2);

        player.transform.position = biestroSpawn.position;

        mealDeliverTrigger.GetComponent<Collider>().enabled = false;
        exitBiestroTrigger.SetActive(false);

        console = GameUI.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        UIManager.TypeText(console, "Administrion ordered their lunch! Go!", 2.5f);

        UIManager.UpdateScore(score);

        isInBiestro = true;
        unpauseBirds();
    }

    void unpauseBirds()
    {
        birdMeter.paused = isInBiestro;
        birdMeter.value = isInBiestro ? -100 : 0;
    }

    public void HandleTriggerEnter(string triggerName)
    {
        if (triggerName == mealReceiveTrigger.name && isReceiveTriggerActive)
        {
            HandleMealReceived();
        }
        else if (triggerName == mealDeliverTrigger.name && !isReceiveTriggerActive)
        {
            HandleMealDelivered();
        }
        else if (triggerName == exitBiestroTrigger.name && !isReceiveTriggerActive)
        {
            HandleExitBiestro();
        }
        else if (triggerName.StartsWith("returnPoint"))
        {
            SendPlayerToBiestro();
            turnOffAllExits();
        }
        else if (triggerName == "biestroEntrance") // Assuming this is the trigger for entering Biestro
        {
            ResetBirdTimer();
        }
    }

    void HandleMealReceived()
    {
        UIManager.ClearAndStopTyping(console);
        UIManager.TypeText(console, "Recieved Meal!", 2.5f);
        isReceiveTriggerActive = false;

        mealReceiveTrigger.GetComponent<Collider>().enabled = false;
        mealDeliverTrigger.GetComponent<Collider>().enabled = true;
        mealReceiveTrigger.GetComponent<MeshRenderer>().enabled = false;
        mealReceiveTrigger.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        exitBiestroTrigger.SetActive(true);
    }

    void HandleMealDelivered()
    {
        UIManager.ClearAndStopTyping(console);
        UIManager.TypeText(console, "Delivered Meal! Go to exit point!", 2.5f);

        turnOffAllExits();
        exitPoints[Random.Range(0, exitPoints.Length)].SetActive(true);

        score++;
        UIManager.UpdateScore(score);

        isReceiveTriggerActive = true;

        mealReceiveTrigger.GetComponent<Collider>().enabled = true;
        mealReceiveTrigger.GetComponent<MeshRenderer>().enabled = true;
        mealReceiveTrigger.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        mealDeliverTrigger.GetComponent<Collider>().enabled = false;
    }

    void HandleExitBiestro()
    {
        map.SetActive(true);

        Transform selectedPoint = mainSpawnPoints[Random.Range(0, mainSpawnPoints.Length)];
        Vector3 spawnPosition = selectedPoint.position;

        player.SetActive(false);
        player.transform.position = spawnPosition;
        player.SetActive(true);

        exitBiestroTrigger.SetActive(false);
        isInBiestro = false;
        unpauseBirds();
        Transition.GetComponent<Animation>().Play();
    }

    public void SendPlayerToBiestro()
    {
        player.SetActive(false);
        player.transform.position = biestroSpawn.position;
        player.transform.rotation = Quaternion.identity;
        player.SetActive(true);
        isInBiestro = true;
        unpauseBirds();
        Transition.GetComponent<Animation>().Play();
    }

    void turnOffAllExits()
    {
        foreach (var exitPoint in exitPoints)
        {
            exitPoint.SetActive(false);
        }
    }

    public void UpdateStamina(float num)
    {
        Slider slider = GameUI.transform.GetChild(3).GetChild(0).GetComponent<Slider>();
        slider.value = num;

        Image sliderFill = slider.transform.GetChild(1).GetChild(0).GetComponent<Image>();

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
        Slider slider = GameUI.transform.GetChild(3).GetChild(0).GetComponent<Slider>();
        slider.maxValue = num;
        slider.minValue = num2;
    }

    void ResetBirdTimer()
    {
        birdMeter.value = -100;
    }
}
