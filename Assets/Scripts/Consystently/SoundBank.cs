using UnityEngine;
using System;
using System.Collections.Generic;

namespace Consystent
{
  namespace Sounds
  {
    [RequireComponent(typeof(AudioSource))]
    public abstract class SoundBank : MonoBehaviour
    {
      /// <summary>
      /// This GameObject's local audio source.
      /// </summary>
      public AudioSource Source { get; protected set; }

      /// <summary>
      /// The sound currently applied to this GameObject's audio source.
      /// </summary>
      public Sound CurrentSound { get; protected set; }

      protected virtual void Awake() => InitializeAudioSource();

      protected virtual void OnEnable()
      {
        SoundManager.Instance.SoundBanks.Add(this);
      }

      protected virtual void OnDisable()
      {
        SoundManager.Instance.SoundBanks.Remove(this);
      }

      /// <summary>
      /// Gets and sets the required audio source component attached to this GameObject.
      /// </summary>
      private void InitializeAudioSource()
      {
        Source ??= GetComponent<AudioSource>();
        Source.playOnAwake = false;
      }

      /// <summary>
      /// Mutes the audio source's volume.
      /// </summary>
      public virtual void MuteSource() => SoundManager.Instance.MuteSource(Source, CurrentSound);

      /// <summary>
      /// Un-mutes the audio source's volume.
      /// </summary>
      public virtual void UnMuteSource() => SoundManager.Instance.UnMuteSource(Source, CurrentSound);

      /// <summary>
      /// Pauses the audio source's playback.
      /// </summary>
      public virtual void PauseSource() => SoundManager.Instance.PauseSource(Source, CurrentSound);

      /// <summary>
      /// Un-pauses the audio source's playback.
      /// </summary>
      public virtual void UnPauseSource() => SoundManager.Instance.UnPauseSource(Source, CurrentSound);

      /// <summary>
      /// Stops the audio source's playback.
      /// </summary>
      public virtual void StopSource() => SoundManager.Instance.StopSource(Source, CurrentSound);
    }

    #region sound track bank
    
    [DisallowMultipleComponent]
    public class SoundTrackBank : SoundBank
    {
      [Header("Sound Track")]

      [Tooltip("List of all sound tracks associated with this GameObject.")]
      [SerializeField] private List<SoundTrack> soundTracks;

      /// <summary>
      /// List of all sound tracks associated with this GameObject.
      /// </summary>
      public List<SoundTrack> SoundTracks => soundTracks;

      /// <summary>
      /// Searches this sound bank for a sound track whose name matches the provided string.
      /// </summary>
      /// <param name="soundTrackName">The requested sound track's name.</param>
      /// <param name="restartTrack">If the same sound track is passed, checks whether to start it from the beginning.</param>
      public void PlaySoundTrack (string soundTrackName, bool restartTrack = false)
      {
        SoundTrack newSoundTrack = soundTracks.Find(Track => Track.DisplayName == soundTrackName);

        if(newSoundTrack != null) 
          PlaySoundTrack(newSoundTrack, restartTrack);
      }

      /// <summary>
      /// Plays a new sound track.
      /// </summary>
      /// <param name="newSoundTrack">The new sound track to be applied to the audio source.</param>
      /// <param name="restartTrack">If the same sound track is passed, checks whether to start it from the beginning.</param>
      public void PlaySoundTrack (SoundTrack newSoundTrack, bool restartTrack = false)
      {
        SoundManager.Instance.PlaySoundTrack(Source, newSoundTrack, restartTrack);
        CurrentSound = newSoundTrack;
      } 
    }

    #endregion

    #region sound effect bank

    [DisallowMultipleComponent]
    public class SoundEffectBank : SoundBank
    {
      [Header("Sound Effect")]

      [Tooltip("List of all sound effects associated with this GameObject.")]
      [SerializeField] private List<SoundEffect> soundEffects;

      /// <summary>
      /// List of all sound effects associated with this GameObject.
      /// </summary>
      public List<SoundEffect> SoundEffects => soundEffects;

      /// <summary>
      /// Searches this sound bank for a sound effect whose name matches the provided string.
      /// </summary>
      /// <param name="soundEffectName">The requested sound effect's name.</param>
      public void PlaySoundEffect (string soundEffectName)
      {
        SoundEffect newSoundEffect = soundEffects.Find(effect => effect.DisplayName == soundEffectName);

        if(newSoundEffect != null) 
          PlaySoundEffect(newSoundEffect);
      }

      /// <summary>
      /// Plays a new sound effect.
      /// </summary>
      /// <param name="newSoundEffect">The new sound effect to be applied to the audio source.</param>
      public void PlaySoundEffect (SoundEffect newSoundEffect)
      {
        SoundManager.Instance.PlaySoundEffect(Source, newSoundEffect);
        CurrentSound = newSoundEffect;
      }
    }

    #endregion
  }
}