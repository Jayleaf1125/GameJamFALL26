using UnityEditor.EditorTools;
using UnityEngine;

namespace Consystent
{
  namespace Sounds 
  {
    public class Sound : ScriptableObject
    {
      [Tooltip("The sound's display name.")]
      [SerializeField] private string displayName;

      /// <summary>
      /// The sound's display name.      
      /// </summary>
      public string DisplayName => displayName;

      /// <summary>
      /// Checks whether the audio source will loop the sound.
      /// </summary>
      public bool Loop;

      /// <summary>
      /// Allows the audio source to play the sound even though AudioListener.pause is set to true. This is useful for the menu element sounds or background music in pause menus.
      /// </summary>
      public bool IgnoreListenerPause;

      /// <summary>
      /// The to-be volume of the audio source for the sound (0.0 to 1.0).
      /// </summary>
      [Range(0.0f, 1.0f)] public float Volume = 0.5f;

      /// <summary>
      /// The pitch of the audio source for the sound.
      /// </summary>
      [Range(-3.0f, 3.0f)] public float Pitch = 1;
      

      [Header("3D Settings", order = 1)]

      /// <summary>
      /// Sets/Gets how the audio source attenuates over distance.
      /// </summary>
      public AudioRolloffMode RolloffMode;

      /// <summary>
      /// Within the minimum distance the audio source will cease to grow louder in volume.
      /// </summary>
      public float MinDistance = 1.0f;

      /// <summary>
      /// The distance where the sound either becomes inaudible or stops attenuation, depending on the rolloff mode.
      /// </summary>
      public float MaxDistance = 500;

      /// <summary>
      /// Sets how much the audio source will be affected by 3D spatialisation calculations (attenuation, doppler etc). 0.0 will make the sound full 2D, 1.0 will make it full 3D.
      /// </summary>
      [Range(0.0f, 1.0f)] public float spatialBlend = 1.0f;

      private void OnValidate() 
      {
        if (MinDistance < 0.0f)
          MinDistance = 0;
        
        if (MaxDistance < 0.0f)
          MaxDistance = 0;

        if (MinDistance > MaxDistance) 
          MaxDistance = MinDistance;
      }
    }

    #region sound track

    [CreateAssetMenu(fileName = "New SoundTrack", menuName = "Scriptable Objects/Audio/SoundTrack")]
    public class SoundTrack : Sound
    {
      [Header("Audio Clip", order = 0)]

      [Tooltip("The sound track clip.")]
      [SerializeField] private AudioClip clip;

      /// <summary>
      /// The sound track clip.
      /// </summary>
      public AudioClip Clip => clip;
    }

    #endregion

    #region sound effect

    [CreateAssetMenu(fileName = "New Sound Effect", menuName = "Scriptable Objects/Audio/Sound Effect")]
    public class SoundEffect : Sound
    {   
      [Header("Audio Clip(s)", order = 0)]

      [Tooltip("The sound effect clip(s). Add variants to the array if necessary. Variants will be selected at random.")]
      [SerializeField] private AudioClip[] clips;

      /// <summary>
      /// The sound effect clip(s). Add variants to the array if necessary. Variants will be selected at random.
      /// </summary>
      public AudioClip[] Clips => clips;

      /// <returns>A random index from the clips array.</returns>
      public int variant => Random.Range(0, Clips.Length);
    }

    #endregion
  }
}