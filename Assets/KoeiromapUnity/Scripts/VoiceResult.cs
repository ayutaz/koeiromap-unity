using System;
using System.Collections.Generic;
using UnityEngine;

namespace KoeiromapUnity.Scripts
{
    [Serializable]
    public class VoiceResult
    {
        public AudioClip audioClip;
        public List<string> phonemes;
        public int seed;
    }
}