using System;
using System.IO;
using System.Threading;
using Cysharp.Threading.Tasks;

namespace KoeiromapUnity.Util
{
    public static class AudioFile
    {
        /// <summary>
        ///     Specify the folder name and phial name (including extension) to save the audio file.
        /// </summary>
        /// <param name="audioBase64Data"></param>
        /// <param name="folderPath"></param>
        /// <param name="fileName"></param>
        /// <param name="token"></param>
        public static async UniTask Save(string audioBase64Data, string folderPath, string fileName,
            CancellationToken token)
        {
            if (string.IsNullOrEmpty(audioBase64Data)) throw new NullReferenceException("audioBase64Data is null.");

            var audioBytes = Convert.FromBase64String(audioBase64Data);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, fileName);
            await File.WriteAllBytesAsync(filePath, audioBytes, token);
        }

        /// <summary>
        ///     Specify the phial name (including extension) to save the audio file.
        /// </summary>
        /// <param name="audioBase64Data"></param>
        /// <param name="filePath"></param>
        /// <param name="token"></param>
        public static async UniTask Save(string audioBase64Data, string filePath, CancellationToken token)
        {
            var folderPath = Path.GetDirectoryName(filePath);
            if (folderPath == null) throw new NullReferenceException("folderPath is null.");
            await Save(audioBase64Data, folderPath, filePath, token);
        }
    }
}