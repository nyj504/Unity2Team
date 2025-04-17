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

    protected void PlayEffectAtPosition(GameObject effectPrefab, Vector3 position)
    {
        if (effectPrefab != null)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.transform.forward);
            GameObject effect = Instantiate(effectPrefab, position, rotation);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ParticleSystem.MainModule main = ps.main;
                main.simulationSpeed = _player.GetPlayerData.AttackSpeed;
            }

            Destroy(effect, 2.0f);
        }
    }
    protected void PlayEffectAttachedToPlayer(GameObject effectPrefab, Vector3 position)
    {
        if (effectPrefab != null)
        {
            Quaternion rotation = Quaternion.LookRotation(_player.transform.forward);
            GameObject effect = Instantiate(effectPrefab, position, rotation, _player.transform);
            ParticleSystem ps = effect.GetComponent<ParticleSystem>();

            if (ps != null)
            {
                ParticleSystem.MainModule main = ps.main;
                main.simulationSpeed = _player.GetPlayerData.AttackSpeed;
            }

            Destroy(effect, 2.0f);
        }
    }
    protected Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero); // y=0 평면 기준
        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }
        return _player.transform.position; // 실패 시 플레이어 위치
    }
}
