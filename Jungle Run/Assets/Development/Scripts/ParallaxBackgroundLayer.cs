using UnityEngine;

public class ParallaxBackgroundLayer : MonoBehaviour
{
    [SerializeField] private float layerEffectMultiplier;

    private Transform _camera;
    private Vector3 _lastCameraPosition;
    private float _textureSize;

    private void Start()
    {
        _camera = Camera.main.transform;
        _lastCameraPosition = _camera.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureSize = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = _camera.position - _lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * layerEffectMultiplier, 0);
        _lastCameraPosition = _camera.position;

        float distanceFromCamera = _camera.position.x - transform.position.x;

        if (Mathf.Abs(distanceFromCamera) >= _textureSize)
        {
            float offsetPositionX = Mathf.Round(distanceFromCamera / _textureSize) * _textureSize;
            transform.position = new Vector3(transform.position.x + offsetPositionX, transform.position.y);
        }
    }
}
