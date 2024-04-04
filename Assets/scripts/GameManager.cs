using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject biestro13;
    public GameObject adminOffice;
    public GameUIManager UIManager;

    public int score = 0;

    public GameObject GameUI;

    private GameObject mealReceiveTrigger;
    private GameObject mealDeliverTrigger;

    private bool isReceiveTriggerActive = true;

    void Start()
    {
        try
        {
            GameObject player = Instantiate(playerPrefab, biestro13.transform.GetChild(0).transform.position, Quaternion.identity);
        }
        catch { }

        mealReceiveTrigger = biestro13.transform.GetChild(1).gameObject;
        mealDeliverTrigger = adminOffice.transform.GetChild(0).gameObject;

        mealDeliverTrigger.GetComponent<Collider>().enabled = false;

        UIManager.TypeText(GameUI.transform.GetChild(0).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>(), "Administrion ordered their lunch! Go!", 2.5f);

        UIManager.UpdateScore(score);
    }

    public void HandleTriggerEnter(string triggerName)
    {
        if (triggerName == mealReceiveTrigger.name && isReceiveTriggerActive)
        {
            UIManager.TypeText(GameUI.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>(), "Recieved Meal", 2.5f);
            isReceiveTriggerActive = false;

            mealReceiveTrigger.GetComponent<Collider>().enabled = false;
            mealDeliverTrigger.GetComponent<Collider>().enabled = true;
        }
        else if (triggerName == mealDeliverTrigger.name && !isReceiveTriggerActive)
        {
            UIManager.TypeText(GameUI.transform.GetChild(2).transform.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>(), "Delivered Meal", 2.5f);

            score ++;

            UIManager.UpdateScore(score);

            isReceiveTriggerActive = true;

            mealReceiveTrigger.GetComponent<Collider>().enabled = true;
            mealDeliverTrigger.GetComponent<Collider>().enabled = false;
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
