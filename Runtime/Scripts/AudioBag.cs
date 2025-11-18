using System.Collections.Generic;
using UnityEngine;

namespace MoodyLib.SimpleAudio {

    /// <summary>
    /// This class is used to store a collection of AudioClips on any GameObject. It will internally manage the audio
    /// clips and play them when requested, by using the SimpleAudio component.
    /// </summary>
    public class AudioBag : MonoBehaviour {
        public AudioSet[] sounds;

        private Dictionary<string, AudioSet> _soundMap;

        private void Awake() {
            _soundMap = new Dictionary<string, AudioSet>();

            foreach (var sound in sounds) {
                _soundMap.Add(sound.label, sound);
            }
        }
        
        /// <summary>
        /// Play a random clip from the set of clips associated with the given label with spatial blend enabled, at the
        /// GameObject's position.
        /// </summary>
        /// <param name="label">The name of the AudioClip to play.</param>
        /// <param name="position">The position where the sound will be played at.</param>
        /// <param name="volume">The volume in which the clip will play.</param>
        /// <param name="spatialBlend">The spatial blend of the clip.</param>
        public void PlayAnyAtPoint(string label, float volume = 1, float spatialBlend = 1) {
            if (!_soundMap.TryGetValue(label, out var set)) return;

            var clip = set.clips[Random.Range(0, set.clips.Length)];
            SimpleAudio.PlayAtPoint(clip, transform.position, volume);
        }

        /// <summary>
        /// Play a random clip from the set of clips associated with the given label with spatial blend enabled, at a
        /// given position.
        /// </summary>
        /// <param name="label">The name of the AudioClip to play.</param>
        /// <param name="position">The position where the sound will be played at.</param>
        /// <param name="volume">The volume in which the clip will play.</param>
        /// <param name="spatialBlend">The spatial blend of the clip.</param>
        public void PlayAnyAtPoint(string label, Vector3 position, float volume = 1, float spatialBlend = 1) {
            if (!_soundMap.TryGetValue(label, out var set)) return;

            var clip = set.clips[Random.Range(0, set.clips.Length)];
            SimpleAudio.PlayAtPoint(clip, position, volume, spatialBlend);
        }

        /// <summary>
        ///  Play a random clip from the set of clips associated with the given label without spatial blend.
        /// </summary>
        /// <param name="label">The name of the AudioClip to play.</param>
        /// <param name="volume">The volume in which the clip will play.</param>
        public void PlayAny(string label, float volume = 1) {
            if (!_soundMap.TryGetValue(label, out var set)) return;

            var clip = set.clips[Random.Range(0, set.clips.Length)];
            SimpleAudio.Play(clip, volume);
        }
    }

    /// <summary>
    /// The data structure used to store a collection of AudioClips associated with a label.
    /// </summary>
    [System.Serializable]
    public struct AudioSet {
        public string label;
        public AudioClip[] clips;
    }
}
