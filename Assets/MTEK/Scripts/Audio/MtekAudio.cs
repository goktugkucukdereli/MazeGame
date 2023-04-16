using System;
using System.Collections.Generic;
using System.Diagnostics;
using RotaryHeart.Lib.SerializableDictionary;
using UnityEngine;
using UnityEngine.Audio;

namespace MTEK.Audio
{
    //[RequireComponent(typeof(AudioListener))]
    public class MtekAudio : MonoBehaviour
    {
        


        public SerializableDictionaryBase<Clips, AudioClip> clips;
        public SerializableDictionaryBase<BackgroundMusics, AudioClip> backgroundMusics;

        private AudioSource sfxSource;
        private AudioSource backgroundMusicSource;

        private AudioMixerGroup masterMixer;
        private AudioMixerGroup sfxMixer;
        private AudioMixerGroup backgroundAudioMixer;

        public static Action<AudioClip> PopOne;
        public static Action<AudioClip> PlayOnce;
        public static Action<AudioClip> SetBackgroundMusic;
        
        public static MtekAudio instance;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                Reset();
                PopOne = PlayOneShot;
                PlayOnce = PlayOneShot;
                SetBackgroundMusic = PlayBackground;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                DestroyImmediate(gameObject);
            }
            

        }

        private void OnDestroy()
        {
            PopOne = null;
        }

        private void _Mute(bool mute = true, Sources source = Sources.AllSources)
        {
            switch (source)
            {
                case Sources.AllSources:
                    masterMixer.audioMixer.SetFloat("masterVolume", mute ? -80f : 0f);
                    break;
                case Sources.Sfx:
                    sfxMixer.audioMixer.SetFloat("sfxVolume", mute ? -80f : 0f);
                    break;
                case Sources.Background:
                    backgroundAudioMixer.audioMixer.SetFloat("backgroundVolume", mute ? -80f : 0f);
                    break;
            }
        }

        public void Mute(Sources source)
        {
            _Mute(true, source);
        }
        
        public void UnMute(Sources source)
        {
            _Mute(false, source);
        }
        
        public void Mute()
        {
            _Mute(true, Sources.AllSources);
        }
        
        public void UnMute()
        {
            _Mute(false, Sources.AllSources);
        }
        
        public void MuteSFX()
        {
            _Mute(true, Sources.Sfx);
        }
        
        public void UnMuteSFX()
        {
            _Mute(false, Sources.Sfx);
        }
        
        public void MuteBackground()
        {
            _Mute(true, Sources.Background);
        }
        
        public void UnMuteBackground()
        {
            _Mute(false, Sources.Background);
        }

        

        public virtual void PlayOneShot(AudioClip audioClip)
        {
            sfxSource.PlayOneShot(audioClip);
        }

        public virtual void PlayOneShot(Clips clip)
        {
            sfxSource.PlayOneShot(clips[clip]);
        }


        public virtual void PlayBackground(AudioClip audioClip)
        {
            if (backgroundMusicSource.isPlaying)
                backgroundMusicSource.Stop();

            backgroundMusicSource.clip = audioClip;
            backgroundMusicSource.Play();
        }

        public virtual void PlayBackground(BackgroundMusics clip)
        {
            if (backgroundMusicSource.isPlaying)
                backgroundMusicSource.Stop();

            backgroundMusicSource.clip = backgroundMusics[clip];
            backgroundMusicSource.Play();
        }

        private void Reset()
        {
            int numAudioSources = GetComponents<AudioSource>().Length;
            for (int i = 0; i < 2 - numAudioSources; i++)
                gameObject.AddComponent<AudioSource>();

            sfxSource = gameObject.GetComponents<AudioSource>()[0];
            backgroundMusicSource = gameObject.GetComponents<AudioSource>()[1];
            sfxSource.loop = false;
            sfxSource.playOnAwake = false;
            backgroundMusicSource.loop = true;
            backgroundMusicSource.playOnAwake = true;
            masterMixer = masterMixer
                ? masterMixer
                : Resources.Load<AudioMixerGroup>("Audio/Mixers/Mtek Master Audio Mixer");
            sfxMixer = sfxMixer
                ? sfxMixer
                : Resources.Load<AudioMixerGroup>("Audio/Mixers/Mtek SFX Audio Mixer");
            backgroundAudioMixer = backgroundAudioMixer
                ? backgroundAudioMixer
                : Resources.Load<AudioMixerGroup>("Audio/Mixers/Mtek Background Audio Mixer");

            sfxSource.outputAudioMixerGroup = sfxMixer;
            backgroundMusicSource.outputAudioMixerGroup = backgroundAudioMixer;
        }
    }

    public enum Sources
    {
        AllSources,
        Sfx,
        Background
    }

    public enum Clips
    {
        Example
    }
    
    public enum BackgroundMusics
    {
        Example
    }
}