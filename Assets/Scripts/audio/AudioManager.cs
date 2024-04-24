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

            // Create AudioSource components for songs
            foreach (Sound s in songs)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;
                s.source.volume = s.volume;
                s.source.outputAudioMixerGroup = mixerGroup;
            }

            // Create AudioSource components for sounds
            foreach (Sound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.clip;
                s.source.loop = s.loop;
                s.source.volume = s.volume;
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
            // Check if the sound is in the songs array
            Sound song = Array.Find(songs, item => item.name == sound);
            if (song != null)
            {
                PlaySong(song);
                return;
            }

            // Check if the sound is in the sounds array
            Sound sfx = Array.Find(sounds, item => item.name == sound);
            if (sfx != null)
            {
                PlaySound(sfx);
                return;
            }

            // Log that the sound doesn't exist
            Debug.LogWarning("Sound: " + sound + " doesn't exist!");
        }

        private void PlaySong(Sound song)
        {
            // Your existing code for playing a song
            if (settings == null)
            {
                song.source.volume = song.volume * (1f + UnityEngine.Random.Range(-song.volumeVariance / 2f, song.volumeVariance / 2f));
            }
            else
            {
                song.source.volume = song.volume * settings.volume;
            }

            song.source.pitch = song.pitch * (1f + UnityEngine.Random.Range(-song.pitchVariance / 2f, song.pitchVariance / 2f));

            song.source.Play();

            // Add the audio source to the list of active audio sources
            activeAudioSources.Add(song.source);
        }

        private void PlaySound(Sound sound)
        {
            // Your existing code for playing a sound
            if (settings == null)
            {
                sound.source.volume = sound.volume * (1f + UnityEngine.Random.Range(-sound.volumeVariance / 2f, sound.volumeVariance / 2f));
            }
            else
            {
                sound.source.volume = sound.volume * settings.volume;
            }

            sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));

            sound.source.Play();

            // Add the audio source to the list of active audio sources
            activeAudioSources.Add(sound.source);
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
                    Sound[] normalSongs = new Sound[4];
                    int normIndex = 0;
                    for (int i = 0; i < songs.Length; i++)
                    {
                        if (songs[i].name == "Normal Chase1" || songs[i].name == "Normal Chase2" || songs[i].name == "Normal Chase3" || songs[i].name == "Normal Chase4")
                        {
                            normalSongs[normIndex] = songs[i];
                            normIndex++;
                        }
                    }
                    Play(normalSongs[UnityEngine.Random.Range(0, normalSongs.Length)].name);
                    
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