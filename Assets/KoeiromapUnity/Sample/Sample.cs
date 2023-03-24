using System.Threading;
using KoeiromapUnity.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace KoeiromapUnity.Sample
{
    [RequireComponent(typeof(AudioSource))]
    public class Sample : MonoBehaviour
    {
        private AudioSource _audioSource;
        [SerializeField] private TMP_InputField inputText;
        [SerializeField] private Slider xValueSlider;
        [SerializeField] private TMP_InputField xValueInputValue;
        [SerializeField] private Slider yValueSlider;
        [SerializeField] private TMP_InputField yValueInputValue;
        [SerializeField] private TMP_Dropdown talkStyleDropdown;
        [SerializeField] private TMP_InputField seed;
        [SerializeField] private Button playVoiceButton;
        private CancellationTokenSource _cancellationTokenSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        private void Start()
        {
            xValueSlider.onValueChanged.AddListener(xValue => xValueInputValue.text = xValue.ToString("F2"));
            yValueSlider.onValueChanged.AddListener(yValue => yValueInputValue.text = yValue.ToString("F2"));

            xValueInputValue.onValueChanged.AddListener(value =>
            {
                if (string.IsNullOrEmpty(value)) return;
                xValueSlider.value = float.Parse(value);
            });

            yValueInputValue.onValueChanged.AddListener(value =>
            {
                if (string.IsNullOrEmpty(value)) return;
                yValueSlider.value = float.Parse(value);
            });

            playVoiceButton.onClick.AddListener(async () =>
            {
                if (string.IsNullOrEmpty(inputText.text)) return;

                var voiceParam = new VoiceParam
                {
                    text = inputText.text,
                    speaker_x = xValueSlider.value,
                    speaker_y = yValueSlider.value,
                    style = talkStyleDropdown.options[talkStyleDropdown.value].text,
                    seed = seed.text?.Length > 0 ? seed.text : Random.Range(-99999, 99999).ToString()
                };
                var voice = await Koeiromap.GetVoice(voiceParam, _cancellationTokenSource.Token);
                _audioSource.clip = voice.audioClip;
                _audioSource.Play();
            });
        }

        private void OnDestroy()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource?.Dispose();
        }
    }
}