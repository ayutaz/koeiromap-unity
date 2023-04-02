using System;
using System.IO;
using NAudio.Wave;
using UnityEngine;

namespace KoeiromapUnity.Core
{
    public static class AudioConverter
    {
        private static MemoryStream ConvertByteArrayToMemoryStream(byte[] wavData)
        {
            if (wavData == null) throw new ArgumentNullException(nameof(wavData));

            // WAVデータをメモリストリームにコピー
            var memoryStream = new MemoryStream(wavData);

            // ストリームの位置をリセット
            memoryStream.Seek(0, SeekOrigin.Begin);

            return memoryStream;
        }

        /// <summary>
        ///     Converts a byte array representing audio data into an AudioClip.
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static AudioClip GetAudio(byte[] bytes)
        {
            var headerStream = ConvertByteArrayToMemoryStream(bytes);
            var audioData = ConvertMemoryStreamToAudioClip(headerStream);
            return audioData;
        }

        private static AudioClip ConvertMemoryStreamToAudioClip(MemoryStream memoryStream)
        {
            if (memoryStream == null) throw new ArgumentNullException(nameof(memoryStream));

            // NAudioライブラリを使用して、WAVデータを読み込みます
            using var reader = new WaveFileReader(memoryStream);
            if (reader.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
                throw new InvalidOperationException("Only IEEE Float format is supported.");

            var channels = reader.WaveFormat.Channels;
            var sampleRate = reader.WaveFormat.SampleRate;
            var sampleCount = reader.Length / (sizeof(float) * channels);
            var samples = new float[sampleCount];

            for (var i = 0; i < sampleCount; i++)
            for (var j = 0; j < channels; j++)

            {
                var sample = reader.ReadNextSampleFrame()[j];
                samples[i * channels + j] = sample;
            }

            // AudioClipの作成
            var audioClip = AudioClip.Create("ConvertedClip", (int)sampleCount / channels, channels, sampleRate,
                false);
            audioClip.SetData(samples, 0);

            return audioClip;
        }
    }
}