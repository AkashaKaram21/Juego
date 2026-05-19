using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] Transform point1;
    [SerializeField] Transform point2;
    float _elapsedTime = 0f;
    float _timeToArrive = 2f;
    bool reverse = false;

    void Update()
    {
        _elapsedTime += Time.deltaTime;
        if (_elapsedTime > _timeToArrive)
        {
            _elapsedTime = 0;
            reverse = !reverse;
        }

        if (reverse)
        {
            transform.position = Vector3.Lerp(point1.position, point2.position, _elapsedTime);
        }
        else
        {
            transform.position = Vector3.Lerp(point2.position, point1.position, _elapsedTime);
        }
    }
}
