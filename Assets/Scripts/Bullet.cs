using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed = 7f;
    [SerializeField] float lifetime = 3f;
    private Vector2 _direction;

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        transform.position += (Vector3)(_direction * speed * Time.deltaTime);
    }
}