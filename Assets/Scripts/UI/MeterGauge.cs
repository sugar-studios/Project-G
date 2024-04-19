using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using ProjectG.Enemies;

namespace ProjectG.UI
{
    public class MeterGauge : MonoBehaviour
    {
        [SerializeField]
        [Range(-100, 0)]
        public int value = 0;
        public GameObject fillArea;
        public RawImage fillImage; // Ensure this is a RawImage component in the Unity Editor
        public BirdAttack birdAttackScript; // Assign the BirdAttack component in the Unity Editor

        public float fillDuration = 18f; // Customizable fill-up duration

        public bool paused = false; // Variable to control pausing

        private void Start()
        {
            if (fillImage == null)
            {
                Debug.LogError("Fill RawImage is not assigned in MeterGauge script.");
                return; // Exit if fillImage is not assigned
            }

            if (birdAttackScript == null)
            {
                Debug.LogError("BirdAttack script reference is not assigned in MeterGauge script.");
                return; // Exit if birdAttackScript is not assigned
            }

            StartCoroutine(AnimateMeterAndPulse());
        }

        public void PauseTimer(bool pauseState)
        {
            paused = pauseState;
        }

        public IEnumerator AnimateMeterAndPulse()
        {
            float startTime = Time.time;
            while (Time.time < startTime + fillDuration)
            {
                // Check if paused, if paused, wait until unpaused
                while (paused)
                {
                    yield return null;
                }

                // Dynamically calculate the value based on elapsed time
                value = (int)Mathf.Lerp(0, -100, (Time.time - startTime) / fillDuration);
                SetYPosition(value);
                yield return null;
            }

            // Pulse effect
            for (int i = 0; i < 3; i++)
            {
                yield return StartCoroutine(FadeTo(fillImage, new Color(1, 1, 1, 0), 0.25f));
                SetYPosition(0);
                yield return StartCoroutine(FadeTo(fillImage, new Color(1, 1, 1, 49 / 255f), 0.25f));
            }
            fillImage.color = new Color(0, 0, 0, 179 / 255f);
            SetYPosition(-100);

            // Activate BirdAttack
            birdAttackScript.gameObject.SetActive(true);
        }

        private IEnumerator FadeTo(RawImage image, Color targetColor, float duration)
        {
            Color startColor = image.color;
            for (float t = 0; t < 1; t += Time.deltaTime / duration)
            {
                image.color = Color.Lerp(startColor, targetColor, t);
                yield return null;
            }
            image.color = targetColor;
        }

        public void SetYPosition(float newY)
        {
            float clampedY = Mathf.Clamp(newY, -100, 0);
            RectTransform rt = fillArea.GetComponent<RectTransform>();
            rt.anchoredPosition = new Vector2(rt.anchoredPosition.x, clampedY);
        }

        public void RestartSequence()
        {
            StopAllCoroutines();
            StartCoroutine(AnimateMeterAndPulse());
        }
    }
}
