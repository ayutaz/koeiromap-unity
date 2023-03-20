using System;
using UnityEngine;

namespace KoeiromapUnity.Scripts
{
    public class Option
    {
        public Option(string saveFolderPath, string fileName, AudioType audioType)
        {
            SaveFolderPath = saveFolderPath;
            FileName = fileName;
            AudioType = audioType;
        }

        public Option(string savePath, AudioType audioType)
        {
            SaveFolderPath = savePath[..savePath.LastIndexOf("/", StringComparison.Ordinal)];
            FileName = savePath[(savePath.LastIndexOf("/", StringComparison.Ordinal) + 1)..];
            AudioType = audioType;
        }

        public string FileName { get; }

        public string SaveFolderPath { get; }
        public AudioType AudioType { get; }

        public string AudioFileExtension()
        {
            switch (AudioType)
            {
                case AudioType.ACC:
                    return ".acc";
                case AudioType.AIFF:
                    return ".aiff";
                case AudioType.IT:
                    return ".it";
                case AudioType.MOD:
                    return ".mod";
                case AudioType.MPEG:
                    return ".mp3";
                case AudioType.OGGVORBIS:
                    return ".ogg";
                case AudioType.S3M:
                    return ".s3m";
                case AudioType.WAV:
                    return ".wav";
                case AudioType.XM:
                    return ".xm";
                case AudioType.XMA:
                    return ".xma";
                case AudioType.VAG:
                    return ".vag";
                case AudioType.AUDIOQUEUE:
                    return ".audioqueue";
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}