using UnityEngine;
using Cinemachine;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera walkCamera; // Reference to the walk camera
    [SerializeField] private CinemachineVirtualCamera runCamera; // Reference to the run camera

    public void ZoomIn()
    {
        if (walkCamera != null && runCamera != null)
        {
            walkCamera.enabled = true; // Enable walk camera
            runCamera.enabled = false; // Disable run camera
        }
    }

    public void ZoomOut()
    {
        if (walkCamera != null && runCamera != null)
        {
            walkCamera.enabled = false; // Disable walk camera
            runCamera.enabled = true; // Enable run camera
        }
    }
}