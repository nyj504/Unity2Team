using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    public static Weapon Instance { get; private set; }

    protected GameObject _qSkillPrefab;
    protected GameObject _eSkillPrefab;

    protected Player _player;

    protected virtual void Awake()
    {
        Instance = this;
    }
    protected void Start()
    {
        _player = GameManager.PlayerInstance;
    }

    public abstract void UseQSkill();
    public abstract void UseESkill();

    protected void SetSkill(int key)
    {
        SkillData data = DataManager.Instance.GetSkillData(key);

        if(data.Type == "Q")
            _qSkillPrefab = Resources.Load<GameObject>(data.PrefabPath);
        else if(data.Type == "E")
            _eSkillPrefab = Resources.Load<GameObject>(data.PrefabPath);
    }

    protected void PlayEffect(GameObject effectPrefab, Vector3 position, Vector3 forward)
    {
        if (effectPrefab != null)
        {
            Quaternion rotation = Quaternion.LookRotation(forward);
            GameObject effect = Instantiate(effectPrefab, position, rotation, _player.transform);
            Destroy(effect, 2.0f);
        }
    }
}
