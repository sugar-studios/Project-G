using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ProjectG.Player; // Ensure this namespace is correct and accessible

namespace ProjectG
{
    public class SinwaveUI : MonoBehaviour
    {
        public RectTransform parentPanel; // Parent panel to hold the line segments
        public GameObject lineSegmentPrefab; // Prefab for line segments
        public PlayerMovement pM; // Reference to the player movement script
        public int points = 100;
        public float baseAmplitude = 50; // Base value of amplitude
        public float baseFrequency = 1; // Base value of frequency
        public float baseMovementSpeed = 1; // Base value of movement speed
        public float width = 500; // Width of the wave display area
        public float xOffset = 0; // Horizontal offset for the wave
        public float intensity = 1; // Current intensity
        public float targetIntensity = 1; // Target intensity to smoothly transition to
        public float intensityTransitionSpeed = .05f; // Speed at which intensity transitions
        public bool usePlayerNoise;

        private float phase = 0;  // Variable to track the accumulated phase
        private List<GameObject> lineSegments = new List<GameObject>();

        void Start()
        {
            PopulatePool();
        }

        void Update()
        {
            if (usePlayerNoise)
            {
                targetIntensity = pM.noiseIntesnity; // Get the noise intensity from the player movement
            }

            // Smoothly interpolate intensity towards the target intensity
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
