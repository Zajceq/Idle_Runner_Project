using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    private Slider _volumeSlider; // Reference to the UI Slider component

    private void Awake()
    {
        // Get the Slider component attached to the GameObject
        _volumeSlider = GetComponent<Slider>();
    }

    private void Start()
    {
        // Set the slider's value to the saved volume from the SaveManager
        _volumeSlider.value = SaveManager.Instance.GetVolume();

        // Add a listener to the slider to handle changes in volume
        _volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    private void SetVolume(float volume)
    {
        // Set the global volume using AudioListener
        AudioListener.volume = volume;

        // Save the volume using the SaveManager
        SaveManager.Instance.SaveVolume(volume);
    }

    private void OnDestroy()
    {
        // Remove the listener to prevent memory leaks when the object is destroyed
        _volumeSlider.onValueChanged.RemoveListener(SetVolume);
    }
}