using Cysharp.Threading.Tasks;
using KoeiromapUnity.Scripts;
using UnityEngine;

namespace KoeiromapUnity.Sample
{
    [RequireComponent(typeof(AudioSource))]
    public class Sample : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        private async void Start()
        {
            var voiceParam = new VoiceParam
            {
                text = "こんにちは",
                speaker_x = 3.0f,
                speaker_y = -3.0f,
                style = "talk",
                seed = "1234567890"
            };
            var option = new Option($"{Application.dataPath}/voice", AudioType.WAV, false);
            var token = this.GetCancellationTokenOnDestroy();
            var voice = await KoeiromapExtensions.GetVoice(voiceParam, token, option);
            _audioSource.clip = voice.audioClip;
            _audioSource.Play();
        }
    }
}