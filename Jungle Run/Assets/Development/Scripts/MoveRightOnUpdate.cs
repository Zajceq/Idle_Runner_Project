using UnityEngine;

public class MoveRightOnUpdate : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime); 
    }
}
