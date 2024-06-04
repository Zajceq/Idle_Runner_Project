using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider _volumeSlider;

    private void Awake()
    {
        _volumeSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        _volumeSlider.value = AudioListener.volume;

        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
    }
}
