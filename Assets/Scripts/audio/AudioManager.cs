using UnityEngine.Audio;
using System;
using UnityEngine;

namespace ProjectG.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioMixerGroup mixerGroup;
        public PlaylistSetting settings;
        public Sound[] songs;
        public Sound[] sounds;


        void Awake()
        {
            foreach (Sound s in songs)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;
                s.source.outputAudioMixerGroup = mixerGroup;
            }

            try
            { 
                settings = GameObject.FindWithTag("AudioSettings").GetComponent<PlaylistSetting>();
            }
            catch
            {
                settings = null;
            }
            
        }

        private void Start()
        {
            if (settings != null)
            {
                if (settings.perferredSong == "")
                {
                    Sound[] normalSongs = new Sound[songs.Length];
                    int normIndex = 0;
                    for (int i = 0; i < songs.Length; i++)
                    {
                        if (songs[i].name == "Normal Chase1" || songs[i].name == "Normal Chase2" || songs[i].name == "Normal Chase3" || songs[i].name == "Normal Chase4")
                        {
                            normalSongs[normIndex] = songs[i];
                            normIndex++;
                        }
                    }
                    normIndex = UnityEngine.Random.Range(0, songs.Length);
                    Debug.Log(songs[normIndex].name);
                    Play(songs[normIndex].name);
                }
                else
                {
                    Play(settings.perferredSong); 
                }
            }
            else
            {
                Play(songs[0].name);

            }
        }


        public void Play(string sound)
        {
            Sound s = Array.Find(songs, item => item.name == sound);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + name + " not found!");
                return;
            }

            if (settings == null)
            {
                s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            }
            else
            {
                s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f)) * settings.volume;
            }
            
            s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            s.source.Play();
        }

        public void StopAllSounds()
        {
            foreach (Sound s in songs)
            {
                if (s.source.isPlaying)
                {
                    s.source.Stop();
                }
            }
        }
    }
}
