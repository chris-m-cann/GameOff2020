using System;
using UnityEngine;
using UnityEngine.Audio;

namespace Util
{
    [Serializable]
    public class AudioClipEx
    {
        public AudioClip Clip;
        [Range(0, 1)] public float Volume = 1f;
        public AudioMixerGroup Mixer;
    }
}