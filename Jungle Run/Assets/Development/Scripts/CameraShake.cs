using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeTotalTime; // Duration of the camera shake
    [SerializeField] private float amplitude; // Intensity of the shake (how much the camera moves)
    [SerializeField] private float frequency; // Speed of the shake (how fast the camera shakes)

    private CinemachineVirtualCamera _cinemachineVirtualCamera; // Reference to the Cinemachine virtual camera
    private float _shakeTime = 0f; // Time left for the camera shake

    public void ShakeCamera()
    {
        // Get the currently active virtual camera from Cinemachine
        _cinemachineVirtualCamera = (CinemachineVirtualCamera)CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;

        // Get the Perlin noise component for shaking the camera
        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        // Set the shake amplitude and frequency
        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;

        // Set the shake time to the total duration
        _shakeTime = shakeTotalTime;
    }

    private void Update()
    {
        // If shake time is still ongoing
        if (_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime; // Decrease shake time

            // Get the Perlin noise component again
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            // Gradually reduce the shake amplitude and frequency over time
            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(amplitude, 0f, 1 - _shakeTime / shakeTotalTime);
            cinemachineBasicMultiChannelPerlin.m_FrequencyGain =
                Mathf.Lerp(frequency, 0f, 1 - _shakeTime / shakeTotalTime);
        }
    }
}