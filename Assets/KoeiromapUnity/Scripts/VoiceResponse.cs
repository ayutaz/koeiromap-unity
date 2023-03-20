﻿using System;
using System.Collections.Generic;

namespace KoeiromapUnity.Scripts
{
    [Serializable]
    public class VoiceResponse
    {
        public string audio;
        public List<string> phonemes;
        public int seed;
    }
}