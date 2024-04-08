using UnityEngine;
using UnityEngine.UI;

public class MeterGauge : MonoBehaviour
{
    [SerializeField]
    [Range(-100, 0)]
    int value = -100;
    public GameObject fillArea;


    public void SetYPosition(float newY)
    {

        float clampedY = Mathf.Clamp(newY, -100, 0);


        RectTransform rt = fillArea.GetComponent<RectTransform>();

        rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, clampedY);
    }

    private void Update()
    {
        SetYPosition(value);
    }
}
