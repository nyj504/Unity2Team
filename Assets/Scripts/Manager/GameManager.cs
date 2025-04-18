using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool isGameStart = false;
    private static GameManager _instance;

    public static GameManager Instance
    {
        get { return _instance; }
    }

    public static Player PlayerInstance { get; private set; }

    private void Awake()
    {
        _instance = this;
        
        DataManager.Instance.LoadUpgradeData();
        DataManager.Instance.LoadCharacterData();
        DataManager.Instance.LoadWeaponData();
        DataManager.Instance.LoadSkillData();
        DataManager.Instance.LoadItemData();

        SpawnPlayer();
    }
    private void SpawnPlayer()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Player/PicoChan");
        if (prefab != null)
        {
            Vector3 spawnPos = Vector3.zero;
            PlayerInstance = Instantiate(prefab, spawnPos, Quaternion.identity).GetComponent<Player>();
        }
    }

    public void SetPlayerWeapon(int key)
    {
        isGameStart = true;
        PlayerInstance.SetWeapon(key);
    }
}
