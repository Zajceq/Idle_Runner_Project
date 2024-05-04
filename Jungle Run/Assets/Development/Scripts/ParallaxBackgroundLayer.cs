using UnityEngine;

public class ParallaxBackgroundLayer : MonoBehaviour
{
    [SerializeField] private float layerEffectMultiplier;

    private Transform _camera;
    private Vector3 lastCameraPosition;
    private float _textureSize;

    private void Start()
    {
        _camera = Camera.main.transform;
        lastCameraPosition = _camera.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        _textureSize = texture.width / sprite.pixelsPerUnit;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = _camera.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * layerEffectMultiplier, 0);
        lastCameraPosition = _camera.position;

        if (Mathf.Abs(_camera.position.x - transform.position.x) >= _textureSize)
        {
            float offsetPositionX = (_camera.position.x - transform.position.x) % _textureSize;
            transform.position = new Vector3(_camera.position.x + offsetPositionX, transform.position.y);
        }
    }
}