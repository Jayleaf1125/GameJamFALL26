using UnityEngine;
using System;
using System.Collections.Generic;
using Consystently.Essentials;

namespace Consystent
{
    namespace Sounds
    {
        [RequireComponent(typeof(AudioSource))]
        public class SoundManager : Singleton<SoundManager>
        {
            public static Action<AudioSource, Sound> OnSoundPlayed;
            public static Action<AudioSource, Sound> OnSoundMuted;
            public static Action<AudioSource, Sound> OnSoundUnMuted;
            public static Action<AudioSource, Sound> OnSoundPaused;
            public static Action<AudioSource, Sound> OnSoundUnPaused;
            public static Action<AudioSource, Sound> OnSoundStopped;

            [Tooltip("Master list of all active Sound Banks. Sound Banks will automatically add and remove themselves.")]
            [SerializeField] private HashSet<SoundBank> soundBanks;

            [Tooltip("When checked the Sound Manager will log a message every time a sound-related function is called.")]
            [SerializeField] private bool logFunctions;

            /// <summary>
            /// Master list of all active Sound Banks. Sound Banks will automatically add and remove themselves.
            /// </summary>
            public HashSet<SoundBank> SoundBanks => soundBanks;

            /// <summary>
            /// This GameObject's local audio source.
            /// </summary>
            public AudioSource Source { get; protected set; }

            /// <summary>
            /// The current sound track playing in runtime.
            /// </summary>
            public SoundTrack GlobalSoundTrack { get; protected set; }

            protected override void Awake()
            {
                base.Awake();
                InitializeAudioSource();
            }

            private void InitializeAudioSource()
            {
                Source ??= GetComponent<AudioSource>();
                Source.playOnAwake = false;
            }

            public void PauseAudioListener() => AudioListener.pause = true;

            public void UnPauseAudioListener() => AudioListener.pause = false;

            /// <summary>
            /// Plays a new sound track.
            /// </summary>
            /// <param name="audioSource">The target AudioSource component.</param>
            /// <param name="newSoundTrack">The new sound track to be applied to the audio source.</param>
            /// <param name="restartTrack">If the same sound track is passed, checks whether to start it from the beginning.</param>
            public void PlaySoundTrack(AudioSource audioSource, SoundTrack newSoundTrack, bool restartTrack = false)
            {
                if (newSoundTrack == GlobalSoundTrack && restartTrack == false)
                    return;

                if (audioSource.isPlaying)
                    audioSource.Stop();

                ApplySoundSettingsToSource(audioSource, newSoundTrack);

                //Play the sound Track
                audioSource.Play();
                OnSoundPlayed?.Invoke(audioSource, newSoundTrack);
                LogSoundFunction(audioSource, newSoundTrack, "PLAYED");
            }

            /// <summary>
            /// Plays a new sound effect.
            /// </summary>
            /// <param name="audioSource">The target AudioSource component.</param>
            /// <param name="newSoundEffect">The new sound effect to be applied to the audio source.</param>
            public void PlaySoundEffect(AudioSource audioSource, SoundEffect newSoundEffect)
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();

                int i = newSoundEffect.variant;
                audioSource.clip = newSoundEffect.Clips[i];
                ApplySoundSettingsToSource(audioSource, newSoundEffect);

                //Play the sound effect
                audioSource.Play();
                OnSoundPlayed?.Invoke(audioSource, newSoundEffect);
                LogSoundFunction(audioSource, newSoundEffect, "PLAYED");
            }

            /// <summary>
            /// Mutes the audio source's volume.
            /// </summary>
            /// <param name="audioSource">The target AudioSource component.</param>
            /// <param name="sound">The current sound applied to the audio source upon calling the function.</param>
            public void MuteSource(AudioSource audioSource, Sound sound)
            {
                if (audioSource.mute)
                    return;

                audioSource.mute = true;
                OnSoundMuted?.Invoke(audioSource, sound);
                LogSoundFunction(audioSource, sound, "MUTED");
            }

            /// <summary>
            /// Un-mutes the audio source's volume.
            /// </summary>
            /// <param name="audioSource">The target AudioSource component.</param>
            /// <param name="sound">The current sound applied to the audio source upon calling the function.</param>
            public void UnMuteSource(AudioSource audioSource, Sound sound)
            {
                if (!audioSource.mute)
                    return;

                audioSource.mute = false;
                OnSoundUnMuted?.Invoke(audioSource, sound);
                LogSoundFunction(audioSource, sound, "UN-MUTED");
            }

            /// <summary>
            /// Pauses the audio source's playback.
            /// </summary>
            /// <param name="audioSource">The target AudioSource component.</param>
            /// <param name="sound">The current sound applied to the audio source upon calling the function.</param>
            public void PauseSource(AudioSource audioSource, Sound sound)
            {
                if (!audioSource.isPlaying)
                    return;

                audioSource.Pause();
                OnSoundPaused?.Invoke(audioSource, sound);
                LogSoundFunction(audioSource, sound, "PAUSED");
            }

            /// <summary>
            /// Un-pauses the audio source's playback.
            /// </summary>
            /// <param name="audioSource">The target AudioSource component.</param>
            /// <param name="sound">The current sound applied to the audio source upon calling the function.</param>
            public void UnPauseSource(AudioSource audioSource, Sound sound)
            {
                if (audioSource.isPlaying)
                    return;

                audioSource.UnPause();
                OnSoundUnPaused?.Invoke(audioSource, sound);
                LogSoundFunction(audioSource, sound, "UN-PAUSED");
            }

            /// <summary>
            /// Stops the audio source's playback.
            /// </summary>
            /// <param name="audioSource">The target AudioSource component.</param>
            /// <param name="sound">The current sound applied to the audio source upon calling the function.</param>
            public void StopSource(AudioSource audioSource, Sound sound)
            {
                if (audioSource.isPlaying)
                {
                    audioSource.Stop();
                    OnSoundStopped?.Invoke(audioSource, sound);
                    LogSoundFunction(audioSource, sound, "STOPPED");
                }
            }

            /// <summary>
            /// Applies all corresponding settings from the sound object to the local audio source.
            /// </summary>
            /// <param name="audioSource">The target AudioSource component.</param>
            /// <param name="sound">The target Sound object.</param>
            public void ApplySoundSettingsToSource(AudioSource audioSource, Sound sound)
            {
                audioSource.loop = sound.Loop;
                audioSource.ignoreListenerPause = sound.IgnoreListenerPause;

                audioSource.volume = sound.Volume;
                audioSource.pitch = sound.Pitch;

                //3D Settings
                audioSource.rolloffMode = sound.RolloffMode;
                audioSource.minDistance = sound.MinDistance;
                audioSource.maxDistance = sound.MaxDistance;
                audioSource.spatialBlend = sound.spatialBlend;
            }

            private void LogSoundFunction(AudioSource audioSource, Sound sound, string action) => Debug.Log("Sound " + action + ": " + sound.DisplayName, audioSource.gameObject);
        }
    }
}
