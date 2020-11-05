using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Util
{
    [Serializable]
    public class AudioClipEx
    {
        public AudioClip clip;
        [Range(0, 1)] public float volume = 1f;
        public AudioMixerGroup mixer;
    }
}