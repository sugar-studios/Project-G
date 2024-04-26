using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProjectG.Player; 

namespace ProjectG
{
    public class SinwaveUI : MonoBehaviour
    {
        public RectTransform parentPanel; 
        public GameObject lineSegmentPrefab; 
        public PlayerMovement pM; 
        public int points = 100;
        public float baseAmplitude = 50; 
        public float baseFrequency = 1; 
        public float baseMovementSpeed = 1;
        public float width = 500; 
        public float xOffset = 0; 
        public float intensity = 1;
        public float targetIntensity = 1;
        public float intensityTransitionSpeed = .05f; 
        public bool usePlayerNoise;

        private float phase = 0;  
        private List<GameObject> lineSegments = new List<GameObject>();

        void Start()
        {
            PopulatePool();
        }

        void Update()
        {
            if (usePlayerNoise)
            {
                targetIntensity = pM.noiseIntesnity; 
            }

            
            intensity = Mathf.Lerp(intensity, targetIntensity, intensityTransitionSpeed * Time.deltaTime);

            UpdateWave();
        }

        private void PopulatePool()
        {
            for (int i = lineSegments.Count; i < points; i++)
            {
                GameObject segment = Instantiate(lineSegmentPrefab, parentPanel);
                segment.transform.localScale = Vector3.one;
                segment.SetActive(false);
                lineSegments.Add(segment);
            }
        }

        private void UpdateWave()
        {
            float xSpacing = width / (points - 1);
            float Tau = 2 * Mathf.PI;

            float amplitude = baseAmplitude * intensity;
            float frequency = baseFrequency * intensity;
            float movementSpeed = baseMovementSpeed * intensity;

            for (int i = 0; i < points; i++)
            {
                if (i >= lineSegments.Count)
                {
                    GameObject segment = Instantiate(lineSegmentPrefab, parentPanel);
                    segment.transform.localScale = Vector3.one;
                    lineSegments.Add(segment);
                }

                GameObject currentSegment = lineSegments[i];
                currentSegment.SetActive(true);
                float x = i * xSpacing + xOffset;
                float y = amplitude * Mathf.Sin(Tau * frequency * (x / width) + phase);
                currentSegment.GetComponent<RectTransform>().anchoredPosition = new Vector2(x, y);
            }

            // Disable unused segments if points were reduced
            for (int i = points; i < lineSegments.Count; i++)
            {
                lineSegments[i].SetActive(false);
            }

            phase += Time.deltaTime * movementSpeed;
        }
    }
}
