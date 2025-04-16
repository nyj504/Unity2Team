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
        PoolingManager.Instance.CreatePool("MonsterPool", MonsterPrefab, _spawnCount * 2);
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
            Vector3 spawnPosition = GetSpawnPosition();

            GameObject monster = PoolingManager.Instance.Pop("MonsterPool");
            monster.transform.position = spawnPosition;
            monster.transform.rotation = Quaternion.identity;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        Vector3 spawnPosition;
        bool _isOutside = false;

        do
        {
            Vector2 _randomDirection = Random.insideUnitSphere.normalized * _spawnRange;
            spawnPosition = new Vector3(_randomDirection.x, 0, _randomDirection.y);

            Vector3 _viewportPosition = _mainCamera.WorldToViewportPoint(spawnPosition);
            _isOutside = _viewportPosition.x < 0 - _cameraBuffer || _viewportPosition.x > 1 + _cameraBuffer ||
                         _viewportPosition.y < 0 - _cameraBuffer || _viewportPosition.y > 1 + _cameraBuffer;
        } while (_isOutside);

        return spawnPosition;
    }
}
