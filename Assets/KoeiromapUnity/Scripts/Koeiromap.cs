using System;
using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace KoeiromapUnity.Scripts
{
    public static class Koeiromap
    {
        private const string ApiUrl = "https://api.rinna.co.jp/models/cttse/koeiro";

        public static async UniTask<VoiceResult> GetVoice(VoiceParam voiceParameters, CancellationToken token)
        {
            var response = await SendVoiceRequest(voiceParameters, token);
            var base64Data = ExtractBase64AudioData(response);
            var audioClip = ConvertBase64ToAudioClip(base64Data);

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

        private static AudioClip ConvertBase64ToAudioClip(string base64EncodedWavString)
        {
            var audioBytes = Convert.FromBase64String(base64EncodedWavString);
            return AudioConverter.GetAudio(audioBytes);
        }
    }
}