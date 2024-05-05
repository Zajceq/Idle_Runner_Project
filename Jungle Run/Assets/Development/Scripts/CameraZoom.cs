using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera walkCamera;
    [SerializeField] private CinemachineVirtualCamera runCamera;

    public void ZoomIn()
    {
        if (walkCamera != null && runCamera != null) 
        {
            walkCamera.enabled = true;
            runCamera.enabled = false;
        }
    }

    public void ZoomOut()
    {
        if (walkCamera != null && runCamera != null)
        {
            walkCamera.enabled = false;
            runCamera.enabled = true;
        }
    }
}