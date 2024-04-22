using UnityEngine.Audio;
using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

namespace ProjectG.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public AudioMixerGroup mixerGroup;
        public PlaylistSetting settings;
        public Sound[] songs;
        public Sound[] sounds;

        public static AudioManager instance;

        private List<AudioSource> activeAudioSources = new List<AudioSource>();

        void Awake()
        {
            if (instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }

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

            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        // Method to handle scene loaded event
        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StopAllSounds();
            if (scene.name == "Game Scene")
            {
                GameSceneMusic();
            }
            else if (scene.name == "Title")
            {
                TitleMusic();
            }
            if (scene.name == "Results")
            {
                ResultMusic();
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }


        public void Play(string sound)
        {
            Sound s = Array.Find(songs, item => item.name == sound);
            if (s == null)
            {
                Debug.LogWarning("Sound: " + sound + " not found!");
                return;
            }

            if (settings == null)
            {
                s.source.volume = s.volume * (1f + UnityEngine.Random.Range(-s.volumeVariance / 2f, s.volumeVariance / 2f));
            }
            else
            {
                s.source.volume = s.volume * settings.volume;
            }

            s.source.pitch = s.pitch * (1f + UnityEngine.Random.Range(-s.pitchVariance / 2f, s.pitchVariance / 2f));

            s.source.Play();

            // Add the audio source to the list of active audio sources
            activeAudioSources.Add(s.source);
        }


        public void StopAllSounds()
        {
            foreach (AudioSource audioSource in activeAudioSources)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
            }

            // Clear the list of active audio sources
            activeAudioSources.Clear();
        }

        // Method to log every playing sound
        public void LogActiveSounds()
        {
            Debug.Log("Active Sounds:");
            foreach (AudioSource audioSource in activeAudioSources)
            {
                Debug.Log("Clip: " + audioSource.clip.name + ", Volume: " + audioSource.volume + ", Pitch: " + audioSource.pitch);
            }
        }

        // Method to update volume of active audio sources dynamically
        public void UpdateActiveAudioVolume()
        {
            foreach (AudioSource audioSource in activeAudioSources)
            {
                if (settings != null)
                {
                    audioSource.volume = settings.volume;
                }
            }
        }

        private void Update()
        {
            UpdateActiveAudioVolume();
        }



        void GameSceneMusic()
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
                    Play(songs[normIndex].name);
                }
                else
                {
                    int index = 0;
                    for (int i = 0; i < songs.Length; i++)
                    {
                        if (songs[i].name == settings.perferredSong)
                        {
                            Debug.Log(i);
                            Debug.Log(index);
                            index = i;
                        }
                    }
                    Play(songs[index].name);
                }
            }
            else
            {
                Play(songs[0].name);

            }
        }

        private void TitleMusic()
        {
            Play("Title");
        }


        private void ResultMusic()
        {
            Play("Results");
        }
    }
}
