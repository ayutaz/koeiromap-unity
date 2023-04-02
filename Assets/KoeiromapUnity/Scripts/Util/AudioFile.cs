using System;
using System.IO;
using System.Threading;

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
        public static async void Save(string audioBase64Data, string folderPath, string fileName,
            CancellationToken token)
        {
            var audioBytes = Convert.FromBase64String(audioBase64Data);
            if (!Directory.Exists(folderPath)) Directory.CreateDirectory(folderPath);
            var filePath = Path.Combine(folderPath, fileName);
            await File.WriteAllBytesAsync(filePath, audioBytes, token);
        }
    }
}