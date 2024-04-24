using UnityEngine;
using System;

namespace ProjectG.Audio
{
    public class PlaySound : MonoBehaviour
    {
        AudioManager aM;

        void Start()
        {
            try
            {
                aM = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
            }
            catch { aM = null; }
        }

        public void sfx(string name)
        {
            if (aM != null)
            {
                aM.Play(name);
            }
        }

        public void STOP()
        {
            if (aM != null)
            {
                aM.StopAllSounds();
            }
        }

        public void FootStep()
        {
            if (aM == null)
            {
                Debug.LogWarning("AudioManager not found!");
                return;
            }

            string[] steps = new string[aM.sounds.Length];
            for (int i = 0; i < aM.sounds.Length; i++)
            {
                steps[i] = aM.sounds[i].name;
            }

            // Create a list to hold valid steps
            var validSteps = new System.Collections.Generic.List<string>();

            // Iterate through the array
            foreach (string step in steps)
            {
                // Check if the step starts with "step"
                if (step.StartsWith("step", StringComparison.OrdinalIgnoreCase))
                {
                    // If it does, add it to the list of valid steps
                    validSteps.Add(step);
                }
            }

            // If there are valid steps, pick one randomly
            if (validSteps.Count > 0)
            {
                int randomIndex = UnityEngine.Random.Range(0, validSteps.Count);
                sfx(validSteps[randomIndex]);
            }
            else
            {
                Debug.LogWarning("No valid steps found.");
            }
        }
    }
}
