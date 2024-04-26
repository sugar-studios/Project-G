using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace ProjectG.UI
{

    public class SineWave : MonoBehaviour
    {
        public LineRenderer myLineRenderer;
        public int points;
        public float amplitude = 1;
        public float frequency = 1;
        public Vector2 xLimits = new Vector2(0, 1);
        public float movementSpeed = 1;
        private float phase = 0;  // Variable to track the accumulated phase

        void Start()
        {
            myLineRenderer = GetComponent<LineRenderer>();
        }

        void Draw()
        {
            float xStart = xLimits.x;
            float xFinish = xLimits.y;
            float Tau = 2 * Mathf.PI;

            myLineRenderer.positionCount = points;
            for (int currentPoint = 0; currentPoint < points; currentPoint++)
            {
                float progress = (float)currentPoint / (points - 1);
                float x = Mathf.Lerp(xStart, xFinish, progress);
                float y = amplitude * Mathf.Sin(Tau * frequency * x + phase);
                myLineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
            }
        }

        void Update()
        {
            // Update the phase based on the current movement speed
            phase += Time.deltaTime * movementSpeed;
            Draw();
        }
    }
}
