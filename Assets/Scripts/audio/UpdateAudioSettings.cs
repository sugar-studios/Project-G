using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ProjectG.Audio
{
    public class UpdateAudioSettings : MonoBehaviour
    {
        PlaylistSetting settings;
        public TMP_Text text;
        string[] songArray = {"Normal Chase1", "Normal Chase2", "Normal Chase3", "Normal Chase4", ""};


        public void UpdateSong()
        {
            settings.perferredSong = songArray[this.GetComponent<TMP_Dropdown>().value];
        }
        public void UpdateVolume()
        {
            text.text = ((int)(this.GetComponent<Slider>().value * 100)).ToString();
            settings.volume = this.GetComponent<Slider>().value;
        }

        private void Start()
        {
            try
            {
                settings = GameObject.FindWithTag("AudioSettings").GetComponent<PlaylistSetting>();
                text.text = ((int)(settings.volume*100)).ToString();
            }
            catch
            {
                settings = null;
            }

            if (this.GetComponent<TMP_Dropdown>() != null)
            {
                int index = 0;
                for (int i = 0; i < songArray.Length; i++)
                {
                    if (songArray[i] == settings.perferredSong)
                    {
                        index = i;
                    }
                }
                this.GetComponent<TMP_Dropdown>().value = index;
            }
            if (this.GetComponent<Slider>() != null)
            {
                text.text = ((int)(settings.volume * 100)).ToString();
                this.GetComponent<Slider>().value = settings.volume;
            }
        }
    }

    public class TMPP_Text
    {
    }
}
