using System.Threading;
using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject MonsterPrefab;
    private int _spawnCount = 5;
    private float _spawnInterval = 5.0f;
    private float _spawnRange = 10.0f;
    private float _cameraBuffer = 5.0f;
    private float _timer = 0;

    private Camera _mainCamera;

    void Start()
    {
        _mainCamera = Camera.main;
    }

    void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _spawnInterval)
        {
            SpawnMonster();
            _timer = 0;
        }
    }

    private void SpawnMonster()
    {
        for (int i = 0; i < _spawnCount; i++)
        {
            Vector3 _spawnPosition = GetSpawnPosition();
            Instantiate(MonsterPrefab, _spawnPosition, Quaternion.identity);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 _spawnPosition;
        bool _isOutside = false;

        do
        {
            Vector2 _randomDirection = Random.insideUnitSphere.normalized * _spawnRange;
            _spawnPosition = new Vector3(_randomDirection.x, 0, _randomDirection.y) + transform.position;

            Vector3 _viewportPosition = _mainCamera.WorldToViewportPoint(_spawnPosition);
            _isOutside = _viewportPosition.x < 0 - _cameraBuffer || _viewportPosition.x > 1 + _cameraBuffer ||
                         _viewportPosition.y < 0 - _cameraBuffer || _viewportPosition.y > 1 + _cameraBuffer;
        } while (_isOutside);

        return _spawnPosition;
    }
}
