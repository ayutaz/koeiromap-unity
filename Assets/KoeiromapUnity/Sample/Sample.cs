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
                speaker_x = 0.0f,
                speaker_y = 0.0f,
                style = "talk",
                seed = "1234567890"
            };
            var token = this.GetCancellationTokenOnDestroy();
            var voice = await KoeiromapExtensions.GetVoice(voiceParam, token);
            _audioSource.clip = voice;
            _audioSource.Play();
        }
    }
}