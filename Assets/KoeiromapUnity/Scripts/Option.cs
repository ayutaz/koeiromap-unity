using System;
using UnityEngine;

namespace KoeiromapUnity.Scripts
{
    public class Option
    {
        public Option(string saveFolderPath, string fileName, AudioType audioType, bool isStream = true)
        {
            SaveFolderPath = saveFolderPath;
            FileName = fileName;
            AudioType = audioType;
            IsStream = isStream;
        }

        public Option(string savePath, AudioType audioType, bool isStream)
        {
            SaveFolderPath = savePath[..savePath.LastIndexOf("/", StringComparison.Ordinal)];
            FileName = savePath[(savePath.LastIndexOf("/", StringComparison.Ordinal) + 1)..];
            AudioType = audioType;
            IsStream = isStream;
        }

        public string FileName { get; }

        public string SaveFolderPath { get; }
        public AudioType AudioType { get; }
        public bool IsStream { get; }

        public string AudioFileExtension()
        {
            return AudioType switch
            {
                AudioType.ACC => ".acc",
                AudioType.AIFF => ".aiff",
                AudioType.IT => ".it",
                AudioType.MOD => ".mod",
                AudioType.MPEG => ".mp3",
                AudioType.OGGVORBIS => ".ogg",
                AudioType.S3M => ".s3m",
                AudioType.WAV => ".wav",
                AudioType.XM => ".xm",
                AudioType.XMA => ".xma",
                AudioType.VAG => ".vag",
                AudioType.AUDIOQUEUE => ".audioqueue",
                AudioType.UNKNOWN => throw new Exception("AudioType is unknown"),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}