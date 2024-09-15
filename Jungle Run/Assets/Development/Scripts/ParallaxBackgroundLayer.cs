using UnityEngine;

public class ParallaxBackgroundLayer : MonoBehaviour
{
    [SerializeField] private float layerEffectMultiplier; // Controls how much this layer moves relative to the camera

    private Transform _camera; // Reference to the main camera's transform
    private Vector3 _lastCameraPosition; // Store the camera's last position for calculating movement
    private float _textureSize; // Size of the texture used in the background layer

    private void Start()
    {
        _camera = Camera.main.transform; // Get the main camera's transform
        _lastCameraPosition = _camera.position; // Initialize the last camera position

        // Get the sprite and its texture to calculate the texture size
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureSize = texture.width / sprite.pixelsPerUnit; // Calculate the size of the texture in world units
    }

    private void LateUpdate()
    {
        // Calculate how much the camera has moved since the last frame
        Vector3 deltaMovement = _camera.position - _lastCameraPosition;

        // Move the layer based on the camera's movement, applying the parallax effect
        transform.position += new Vector3(deltaMovement.x * layerEffectMultiplier, 0);

        // Update the last camera position for the next frame
        _lastCameraPosition = _camera.position;

        // Check how far the layer is from the camera horizontally
        float distanceFromCamera = _camera.position.x - transform.position.x;

        // If the layer is too far from the camera, reposition it to create a seamless looping effect
        if (Mathf.Abs(distanceFromCamera) >= _textureSize)
        {
            // Calculate how much to move the layer to keep the parallax effect seamless
            float offsetPositionX = Mathf.Round(distanceFromCamera / _textureSize) * _textureSize;
            transform.position = new Vector3(transform.position.x + offsetPositionX, transform.position.y);
        }
    }
}