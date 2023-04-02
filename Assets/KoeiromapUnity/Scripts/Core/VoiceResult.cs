using System;
using System.Collections.Generic;
using UnityEngine;

namespace KoeiromapUnity.Core
{
    [Serializable]
    public class VoiceResult
    {
        public AudioClip audioClip;
        public string audioBase64;
        public List<string> phonemes;
        public int seed;
    }
}