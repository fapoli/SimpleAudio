using System.Collections.Generic;
using UnityEngine;

namespace MoodyLib.SimpleAudio {

    [RequireComponent(typeof(AudioSource))]
    public class SimpleAudio : MonoBehaviour {
        private static SimpleAudio _instance;

        public int initialAudioSourceCount = 10;

        private AudioSource _mainAudioSource;
        private List<AudioSource> _audioSourcePool;
        
        private void Awake() {
            _mainAudioSource = GetComponent<AudioSource>();
            _audioSourcePool = new List<AudioSource>();
            transform.position = Vector3.zero;

            for (var i = 0; i < initialAudioSourceCount; i++) {
                CreateAudioSource();
            }
            
            _instance = this;
        }
        
        /// <summary>
        /// Play a sound with the given volume.
        /// </summary>
        /// <param name="audioClip">The AudioClip that will be played.</param>
        /// <param name="volume">The volume in which the AudioClip will be played.</param>
        public static void Play(AudioClip audioClip, float volume = 1){
            if (!audioClip) return;
        
            if (_instance) {
                _instance.PlaySound(audioClip, volume);
            } else {
                Debug.LogWarning("No component with SimpleAudio has been detected in the scene. Some sounds may not play.");
            }
        }

        /// <summary>
        /// Play a random AudioClip from the given list with the given volume.
        /// </summary>
        /// <param name="audioClips">The List of AudioClips from which one random clip will be selected.</param>
        /// <param name="volume">The volume in which the AudioClip will be played.</param>
        public static void PlayAny(List<AudioClip> audioClips, float volume = 1) {
            if (audioClips == null || audioClips.Count == 0) return;
            Play(audioClips[Random.Range(0, audioClips.Count)], volume);
        }

        /// <summary>
        /// Play a sound at the given position with the given volume and spatial blend.
        /// </summary>
        /// <param name="audioClip">The AudioClip that will be played.</param>
        /// <param name="position">The position where the sound will be played at.</param>
        /// <param name="volume">The volume in which the AudioClip will be played.</param>
        /// <param name="spatialBlend">The spatial blend of the AudioClip</param>
        public static void PlayAtPoint(AudioClip audioClip, Vector3 position, float volume = 1, float spatialBlend = 1) {
            if (!audioClip) return;
        
            if (_instance) {
                _instance.PlayAt(audioClip, position, volume, spatialBlend);
            } else {
                Debug.LogWarning("No component with SimpleAudio has been detected in the scene. Some sounds may not play.");
            }
        }
        
        /// <summary>
        /// Play a sound at the given position with the given volume and spatial blend.
        /// </summary>
        /// <param name="audioClips">The List of AudioClips from which one random clip will be selected.</param>
        /// <param name="position">The position where the sound will be played at.</param>
        /// <param name="volume">The volume in which the AudioClip will be played.</param>
        /// <param name="spatialBlend">The spatial blend of the AudioClip.</param>
        public static void PlayAnyAtPoint(List<AudioClip> audioClips, Vector3 position, float volume = 1, float spatialBlend = 1) {
            if (audioClips == null || audioClips.Count == 0) return;
            PlayAtPoint(audioClips[Random.Range(0, audioClips.Count)], position, volume, spatialBlend);
        }
        
        private void PlaySound(AudioClip audioClip, float volume = 1) {
            _mainAudioSource.PlayOneShot(audioClip, volume);
        }
        
        private void PlayAt(AudioClip audioClip, Vector3 position, float volume = 1, float spatialBlend = 1) {
            if (spatialBlend <= 0) {
                PlaySound(audioClip, volume);
                return;
            }

            if (spatialBlend > 1) spatialBlend = 1;
            
            var audioSource = GetUnusedAudioSource();
            audioSource.transform.position = position;
            audioSource.spatialBlend = spatialBlend;
            audioSource.volume = volume;
            audioSource.clip = audioClip;
            audioSource.Play();
        }

        private AudioSource GetUnusedAudioSource() {
            foreach (var audioSource in _audioSourcePool) {
                if (!audioSource.isPlaying) {
                    return audioSource;
                }
            }
        
            return CreateAudioSource();
        }

        private AudioSource CreateAudioSource() {
            var audioSource = new GameObject("AudioSource").AddComponent<AudioSource>();
            audioSource.transform.parent = transform;
            audioSource.playOnAwake = false;
            audioSource.loop = false;
            
            _audioSourcePool.Add(audioSource);
            return audioSource;
        }
    }
}
