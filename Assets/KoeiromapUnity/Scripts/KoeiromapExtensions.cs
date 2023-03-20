using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace KoeiromapUnity.Scripts
{
    public static class KoeiromapExtensions
    {
        private const string ApiUrl = "https://api.rinna.co.jp/models/cttse/koeiro";

        public static async UniTask<VoiceResult> GetVoice(VoiceParam voiceParameters, CancellationToken token)
        {
            var option = new Option(Application.persistentDataPath, "tmpWavBase64", AudioType.WAV);
            return await GetVoice(voiceParameters, token, option);
        }

        public static async UniTask<VoiceResult> GetVoice(VoiceParam voiceParameters, CancellationToken token,
            Option option)
        {
            var response = await SendVoiceRequest(voiceParameters, token);
            var base64Data = ExtractBase64AudioData(response);
            var audioClip = await ConvertBase64ToAudioClip(base64Data, option);
            return new VoiceResult
            {
                audioClip = audioClip,
                phonemes = response.phonemes,
                seed = response.seed
            };
        }

        private static async UniTask<VoiceResponse> SendVoiceRequest(VoiceParam voiceParam, CancellationToken token)
        {
            var json = JsonUtility.ToJson(voiceParam);
            var bodyRaw = Encoding.UTF8.GetBytes(json);
            var request = new UnityWebRequest(ApiUrl, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            await request.SendWebRequest().WithCancellation(token);
            return JsonUtility.FromJson<VoiceResponse>(request.downloadHandler.text);
        }

        private static string ExtractBase64AudioData(VoiceResponse voiceResponse)
        {
            var audio = voiceResponse.audio;
            return audio[(audio.IndexOf(",", StringComparison.Ordinal) + 1)..];
        }

        private static async UniTask<AudioClip> ConvertBase64ToAudioClip(string base64EncodedWavString, Option option)
        {
            var audioBytes = Convert.FromBase64String(base64EncodedWavString);
            var tempPath = Path.Combine(option.SaveFolderPath, $"{option.FileName}{option.AudioFileExtension()}");
            if (!Directory.Exists(option.SaveFolderPath)) Directory.CreateDirectory(option.SaveFolderPath);
            await File.WriteAllBytesAsync(tempPath, audioBytes);
            using var request = UnityWebRequestMultimedia.GetAudioClip(tempPath, option.AudioType);
            await request.SendWebRequest();
            if (request.result.Equals(UnityWebRequest.Result.ConnectionError))
            {
                Debug.LogError(request.error);
                throw new WebException(request.error);
            }

            var content = DownloadHandlerAudioClip.GetContent(request);
            request.Dispose();
            return content;
        }
    }
}