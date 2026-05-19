using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    [SerializeField] Transform _target;
    [SerializeField] float _smoothFactor = 0.09f;
    [SerializeField] float _zOffset = -10f;

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, _target.position, _smoothFactor);
        transform.position = new Vector3(transform.position.x, transform.position.y, _zOffset);
    }
}
