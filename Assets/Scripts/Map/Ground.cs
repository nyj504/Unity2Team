using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private int _tileSize = 20;
    private float _moveThreshold = 2.0f;

    private Dictionary<(int, int), GameObject> _tiles = new Dictionary<(int, int), GameObject>();
    private (int, int) _centerIndex = (0, 0);

    private Transform _playerTransform;
    private void Awake()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Ground");
        if (prefab == null) return;

        Vector3 centerPos = Vector3.zero;
        
        for (int z = -1; z <= 1; z++)
        {
            for (int x = -1; x <= 1; x++)
            {
                Vector3 offset = new Vector3(x * _tileSize, 0, z * _tileSize);
                Vector3 spawnPos = centerPos + offset;

                GameObject tile = Instantiate(prefab, spawnPos, Quaternion.identity, transform);
                tile.name = $"Tile_{x}_{z}";

                _tiles[(x, z)] = tile;
            }
        }
    }
    private void Start()
    {
        if (GameManager.PlayerInstance != null)
            _playerTransform = GameManager.PlayerInstance.transform;
    }

    private void Update()
    {
        if (_playerTransform == null) return;

        Vector3 playerPos = _playerTransform.position;

        float dx = playerPos.x - (_centerIndex.Item1 * _tileSize);
        float dz = playerPos.z - (_centerIndex.Item2 * _tileSize);

        if (Mathf.Abs(dx) > _tileSize * 0.5f)
        {
            int step = dx > 0 ? 1 : -1;
            ShiftTilesHorizontal(step);
            _centerIndex = (_centerIndex.Item1 + step, _centerIndex.Item2);
        }
        else if (Mathf.Abs(dz) > _tileSize * 0.5f)
        {
            int step = dz > 0 ? 1 : -1;
            ShiftTilesVertical(step);
            _centerIndex = (_centerIndex.Item1, _centerIndex.Item2 + step);
        }
    }


    private void ShiftTilesHorizontal(int direction)
    {
        GameObject leftTop = _tiles[(-1, 1)];
        GameObject leftCenter = _tiles[(-1, 0)];
        GameObject leftBottom = _tiles[(-1, -1)];

        GameObject centerTop = _tiles[(0, 1)];
        GameObject center = _tiles[(0, 0)];
        GameObject centerBottom = _tiles[(0, -1)];

        GameObject rightTop = _tiles[(1, 1)];
        GameObject rightCenter = _tiles[(1, 0)];
        GameObject rightBottom = _tiles[(1, -1)];

        if (direction == 1)
        {
            leftTop.transform.position += new Vector3(_tileSize * 3, 0, 0);
            leftCenter.transform.position += new Vector3(_tileSize * 3, 0, 0);
            leftBottom.transform.position += new Vector3(_tileSize * 3, 0, 0);
            
            _tiles[(-1, 1)] = centerTop;
            _tiles[(-1, 0)] = center;
            _tiles[(-1, -1)] = centerBottom;

            _tiles[(0, 1)] = rightTop;
            _tiles[(0, 0)] = rightCenter;
            _tiles[(0, -1)] = rightBottom;

            _tiles[(1, 1)] = leftTop;
            _tiles[(1, 0)] = leftCenter;
            _tiles[(1, -1)] = leftBottom;
        }
        else
        {
            rightTop.transform.position += new Vector3(-_tileSize * 3, 0, 0);
            rightCenter.transform.position += new Vector3(-_tileSize * 3, 0, 0);
            rightBottom.transform.position += new Vector3(-_tileSize * 3, 0, 0);

            _tiles[(1, 1)] = centerTop;
            _tiles[(1, 0)] = center;
            _tiles[(1, -1)] = centerBottom;

            _tiles[(0, 1)] = leftTop;
            _tiles[(0, 0)] = leftCenter;
            _tiles[(0, -1)] = leftBottom;

            _tiles[(-1, 1)] = rightTop;
            _tiles[(-1, 0)] = rightCenter;
            _tiles[(-1, -1)] = rightBottom;
        }
    }

    private void ShiftTilesVertical(int direction)
    {
        GameObject topLeft = _tiles[(-1, 1)];
        GameObject topCenter = _tiles[(0, 1)];
        GameObject topRight = _tiles[(1, 1)];

        GameObject centerLeft = _tiles[(-1, 0)];
        GameObject center = _tiles[(0, 0)];
        GameObject centerRight = _tiles[(1, 0)];

        GameObject bottomLeft = _tiles[(-1, -1)];
        GameObject bottomCenter = _tiles[(0, -1)];
        GameObject bottomRight = _tiles[(1, -1)];

        if (direction == 1) // 플레이어 위쪽
        {
            bottomLeft.transform.position += new Vector3(0.0f, 0.0f, _tileSize * 3);
            bottomCenter.transform.position += new Vector3(0.0f, 0.0f, _tileSize * 3);
            bottomRight.transform.position += new Vector3(0.0f, 0.0f, _tileSize * 3);

            _tiles[(-1, -1)] = centerLeft;
            _tiles[(0, -1)] = center;
            _tiles[(1, -1)] = centerRight;

            _tiles[(-1, 0)] = topLeft;
            _tiles[(0, 0)] = topCenter;
            _tiles[(1, 0)] = topRight;

            _tiles[(-1, 1)] = bottomLeft;
            _tiles[(0, 1)] = bottomCenter;
            _tiles[(1, 1)] = bottomRight;
        }
        else // 플레이어 아래쪽
        {
            topLeft.transform.position += new Vector3(0.0f, 0.0f, -_tileSize * 3);
            topCenter.transform.position += new Vector3(0.0f, 0.0f, -_tileSize * 3);
            topRight.transform.position += new Vector3(0.0f, 0.0f, -_tileSize * 3);

            _tiles[(-1, 1)] = centerLeft;
            _tiles[(0, 1)] = center;
            _tiles[(1, 1)] = centerRight;

            _tiles[(-1, 0)] = bottomLeft;
            _tiles[(0, 0)] = bottomCenter;
            _tiles[(1, 0)] = bottomRight;

            _tiles[(-1, -1)] = topLeft;
            _tiles[(0, -1)] = topCenter;
            _tiles[(1, -1)] = topRight;
        }
    }
}
