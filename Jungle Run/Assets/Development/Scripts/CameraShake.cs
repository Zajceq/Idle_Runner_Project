using Cinemachine;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private float shakeTotalTime;
    [SerializeField] private float amplitude;
    [SerializeField] private float frequency;

    private CinemachineVirtualCamera _cinemachineVirtualCamera;
    private float _shakeTime = 0f;

    public void ShakeCamera()
    {
        _cinemachineVirtualCamera = (CinemachineVirtualCamera)CinemachineCore.Instance.GetActiveBrain(0).ActiveVirtualCamera;

        CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
            _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = amplitude;
        cinemachineBasicMultiChannelPerlin.m_FrequencyGain = frequency;
        _shakeTime = shakeTotalTime;
    }

    private void Update()
    {
        if (_shakeTime > 0)
        {
            _shakeTime -= Time.deltaTime;
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin =
                _cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                Mathf.Lerp(amplitude, 0f, 1 - _shakeTime / shakeTotalTime);

            cinemachineBasicMultiChannelPerlin.m_FrequencyGain =
                Mathf.Lerp(frequency, 0f, 1 - _shakeTime / shakeTotalTime);
        }
    }
}
