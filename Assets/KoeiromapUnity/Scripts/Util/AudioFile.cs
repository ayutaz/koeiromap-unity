using System.IO;
using NAudio.Wave;
using UnityEngine;

namespace KoeiromapUnity.Util
{
    public class AudioFile
    {
        public static void Save(string path, AudioClip audioClip)
        {
            var extension = GetFileExtension(path);
            if (extension.Equals(".wav"))
                SaveAudioClipToWav(audioClip, path);
            else
                Debug.LogError("Not supported file extension: " + extension);
        }

        private static string GetFileExtension(string filePath)
        {
            return Path.GetExtension(filePath);
        }

        private static void SaveAudioClipToWav(AudioClip audioClip, string outputPath)
        {
            var samples = new float[audioClip.samples * audioClip.channels];
            audioClip.GetData(samples, 0);

            using var waveFileWriter =
                new WaveFileWriter(outputPath, new WaveFormat(audioClip.frequency, 16, audioClip.channels));
            foreach (var sample in samples)
            {
                int sampleAsInt = (short)(sample * short.MaxValue);
                waveFileWriter.WriteSample(sampleAsInt);
            }
        }
    }
}