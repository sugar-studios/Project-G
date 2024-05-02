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

        private Dictionary<string, float> songPositions = new Dictionary<string, float>();
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
                s.source.volume = s.volume;
                s.source.outputAudioMixerGroup = mixerGroup;
                songPositions[s.name] = 0f;  // Initialize song position tracking
            }

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

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            StopAllSounds();
            switch (scene.name)
            {
                case "Game Scene":
                    GameSceneMusic();
                    break;
                case "Title":
                    TitleMusic();
                    break;
                case "Results":
                    ResultMusic();
                    break;
                case "Leaderboard":
                    LeaderMusic();
                    break;
            }
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public void Play(string sound)
        {
            Sound song = Array.Find(songs, item => item.name == sound);
            if (song != null)
            {
                PlaySong(song);
                return;
            }

            Sound sfx = Array.Find(sounds, item => item.name == sound);
            if (sfx != null)
            {
                PlaySound(sfx);
                return;
            }

            Debug.LogWarning("Sound: " + sound + " doesn't exist!");
        }

        private void PlaySong(Sound song)
        {
            song.source.volume = settings == null ? song.volume : song.volume * settings.volume;
            song.source.pitch = 1;

            // Check if there's a saved position for this song
            if (songPositions.ContainsKey(song.name) && songPositions[song.name] > 0f)
            {
                song.source.time = songPositions[song.name];
            }

            song.source.Play();
            activeAudioSources.Add(song.source);
        }

        private void PlaySound(Sound sound)
        {
            sound.source.volume = settings == null ? sound.volume : sound.volume * settings.volume;
            sound.source.pitch = sound.pitch * (1f + UnityEngine.Random.Range(-sound.pitchVariance / 2f, sound.pitchVariance / 2f));
            sound.source.Play();
            activeAudioSources.Add(sound.source);
        }

        public void StopAllSounds()
        {
            foreach (AudioSource audioSource in activeAudioSources)
            {
                if (audioSource.isPlaying)
                {
                    // Save the current position of the song before stopping
                    Sound song = Array.Find(songs, item => item.source == audioSource);
                    if (song != null)
                    {
                        songPositions[song.name] = audioSource.time;
                    }
                    audioSource.Stop();
                }
            }
            activeAudioSources.Clear();
        }

        public void LogActiveSounds()
        {
            Debug.Log("Active Sounds:");
            foreach (AudioSource audioSource in activeAudioSources)
            {
                Debug.Log("Clip: " + audioSource.clip.name + ", Volume: " + audioSource.volume + ", Pitch: " + audioSource.pitch);
            }
        }

        public void UpdateActiveAudioVolume()
        {
            foreach (AudioSource audioSource in activeAudioSources)
            {
                if (settings != null)
                {
                    Sound sound = Array.Find(sounds, item => item.source == audioSource);
                    if (sound != null)
                    {
                        audioSource.volume = sound.volume * settings.volume;
                    }
                    else
                    {
                        Sound song = Array.Find(songs, item => item.source == audioSource);
                        if (song != null)
                        {
                            audioSource.volume = song.volume * settings.volume;
                        }
                    }
                }
            }
        }

        void Update()
        {
            UpdateActiveAudioVolume();
        }

        void GameSceneMusic()
        {
            if (settings != null && !string.IsNullOrEmpty(settings.perferredSong))
            {
                int index = Array.FindIndex(songs, item => item.name == settings.perferredSong);
                if (index != -1)
                {
                    Play(songs[index].name);
                }
            }
            else
            {
                Play(songs[UnityEngine.Random.Range(0, songs.Length)].name);
            }
        }

        private void TitleMusic()
        {
            Play("Title");
        }

        private void LeaderMusic()
        {
            Play("Leaderboard");
        }

        private void ResultMusic()
        {
            Play("Results");
        }
    }
}