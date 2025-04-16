using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public GameObject _playerInstance { get; private set; }

    private void Awake()
    {
        _instance = this;
        
        DataManager.Instance.LoadUpgradeData();
        DataManager.Instance.LoadCharacterData();

        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/PicoChan");
        if (prefab != null)
        {
            Vector3 spawnPos = Vector3.zero;
            _playerInstance = Instantiate(prefab, spawnPos, Quaternion.identity);
        }
    }
}
