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
        _volumeSlider.value = SaveManager.Instance.GetVolume();

        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        AudioListener.volume = volume;
        SaveManager.Instance.SaveVolume(volume);
    }

    private void OnDestroy()
    {
        _volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}
