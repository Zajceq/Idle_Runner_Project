using UnityEngine;

public class MoveRightOnUpdate : MonoBehaviour
{
    [SerializeField] private float speed = 5f; // Speed at which the object moves to the right

    void Update()
    {
        // Move the object to the right every frame based on the speed and frame time
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }
}