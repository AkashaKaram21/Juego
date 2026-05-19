using UnityEngine;

public class Spawner : MonoBehaviour
{
    float _elapsedTime = 0;
    float _spawnTime = 2f;
    int _spawnCount = 0;
    [SerializeField] int _objectsToSpawn = 5;
    public GameObject CoinPrefab;

    void Update()
    {
        if (_spawnCount >= _objectsToSpawn) { return; }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime > _spawnTime)
        {
            _elapsedTime = 0;
            _spawnCount++;
            GameObject coin = Instantiate(CoinPrefab);
            coin.transform.position = transform.position;
        }
    }
}