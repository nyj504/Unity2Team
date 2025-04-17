using TMPro;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private GameObject _healingItemPrefab;
    private GameObject _shieldPrefab;
    private GameObject _magnetPrefab;

    private static ItemManager _instance;
    public static ItemManager Instance
    { get { return _instance; } }

    private int _spawnCount = 10;

    private void Awake()
    {
        _instance = this;

        _healingItemPrefab = Resources.Load<GameObject>("Prefabs/Item/Heart");
        _shieldPrefab = Resources.Load<GameObject>("Prefabs/Item/Shield");
        _magnetPrefab = Resources.Load<GameObject>("Prefabs/Item/Magnet");
        //_expItemPrefab = Resources.Load<GameObject>("Prefabs/Item/ExpItem");

        PoolingManager.Instance.CreatePool("HealingItemPool", _healingItemPrefab, _spawnCount);
        PoolingManager.Instance.CreatePool("ShieldItemPool", _shieldPrefab, _spawnCount);
        PoolingManager.Instance.CreatePool("MagnetItemPool", _magnetPrefab, _spawnCount);
    }

    private void Start()
    {
        SpawnRandomItem(1);
        SpawnRandomItem(2);
        SpawnRandomItem(3);
    }

    private void Update()
    {
    }

    private void SpawnRandomItem(int key)
    {
        string poolName = key switch
        {
            1 => "HealingItemPool",
            2 => "ShieldItemPool",
            3 => "MagnetItemPool",
            _ => null
        };

        GameObject itemObj = PoolingManager.Instance.Pop(poolName);
        if (itemObj == null) return;

        Item item = itemObj.GetComponent<Item>();
        if (item == null) return;

        item.SpawnItem(key);
    }
}
