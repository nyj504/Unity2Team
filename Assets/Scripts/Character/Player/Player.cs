using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public enum State
    {
        Move,
        BaseAttack,
        Skill,
        Dead
    }

    private Animator _animator;
   
    private State _curState;

    private CharacterData _characterData;
    public CharacterData GetPlayerData
    {
        get { return _characterData; }
    }

    private float _curHp;
    private float _life;
    private float _baseMoveSpeed = -1f;

    private Vector3 _velocity;
    private Vector3 _targetVelocity = Vector3.zero;

    [SerializeField] 
    private Transform _weaponSocket;
    private GameObject _playerWeapon;
    private WeaponData _weaponData;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _characterData.Key = DataManager.Instance.CharacterDatas.Keys.First();

        LoadPlayerData(_characterData.Key);

        _curState = State.Move;
    }
    private void Update()
    {
        Move();
        Attack();
        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.F1))
        {
            if (_characterData.Level == 5) return;

            //UIManager.Instance.OpenChoiceUI();
            LoadPlayerData(_characterData.Key++);
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetWeapon(501);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetWeapon(502);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetWeapon(503);
        }
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            _animator.SetTrigger("QAttack");
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _animator.SetTrigger("EAttack");
        }
    }
    private void Move()
    {
        Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
        Vector3 mouseScreenPos = Input.mousePosition;
        Vector3 dir = mouseScreenPos - playerScreenPos;
        float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, angle, 0f);

        if (Input.GetKey(KeyCode.W))
        {
            _targetVelocity = transform.forward * _characterData.MoveSpeed;
        }
        else
        {
            _targetVelocity = Vector3.zero;
        }

        _velocity = Vector3.Lerp(_velocity, _targetVelocity, Time.deltaTime * 10f);

        transform.position += _velocity * Time.deltaTime;

        Vector3 pos = transform.position;
        pos.y = 0.0f;
        transform.position = pos;
    }
    private void LoadPlayerData(int key)
    {
        CharacterData data = DataManager.Instance.GetCharacterData(key);

        _characterData.Level = data.Level;
        _characterData.AttackPower = data.AttackPower;
        _characterData.MaxHp = data.MaxHp;
        _characterData.MoveSpeed = data.MoveSpeed;
        _characterData.AttackSpeed = data.AttackSpeed;

        if (_baseMoveSpeed < 0f)
        {
            _baseMoveSpeed = data.MoveSpeed;
        }

        _animator.speed = _characterData.MoveSpeed / _baseMoveSpeed;
    }
    private void UpdateAnimator()
    {
        float speedNormalized = _velocity.magnitude / _characterData.MoveSpeed;
        _animator.SetFloat("Speed", speedNormalized);
    }
    public void OnAnimationEnd()
    {
        _curState = State.Move;
    }

    public void SetWeapon(int key)
    {
        WeaponData weaponData = DataManager.Instance.GetWeaponData(key);

        GameObject prefab = Resources.Load<GameObject>(weaponData.PrefabPath);
        if (prefab != null)
        {
            if (_playerWeapon != null)
            {
                Destroy(_playerWeapon);
            }

            _playerWeapon = Instantiate(prefab, _weaponSocket);
            _playerWeapon.transform.localPosition = new Vector3(weaponData.PosX, weaponData.PosY, weaponData.PosZ);
            _playerWeapon.transform.localRotation = Quaternion.Euler(weaponData.RotX, weaponData.RotY, weaponData.RotZ);
        }

        _animator.SetTrigger(weaponData.Name);
    }

    public void UseQSkill()
    {
        Weapon.Instance.UseQSkill();
    }

    public void UseESkill()
    {
        Weapon.Instance.UseESkill();
    }

    public void EnhancePlayerStatus(int key)
    {
        UpgradeData upgradeData = DataManager.Instance.GetUpgradeData(key);
        if (upgradeData.Type == "AttackPower")
        {
            _characterData.AttackPower += upgradeData.Value;
        }
        else if (upgradeData.Type == "MoveSpeed")
            _characterData.MoveSpeed += upgradeData.Value;
        else if (upgradeData.Type == "AttackSpeed")
            _characterData.AttackSpeed += upgradeData.Value;
        //else if (upgradeData.Type == "Money")
        //   _characterData.MoneyGain += upgradeData.Value;
        else if (upgradeData.Type == "MaxHP")
        {
            _characterData.MaxHp += upgradeData.Value;
            _curHp += upgradeData.Value;
        }
        else if (upgradeData.Type == "ExtraLife")
            _life += upgradeData.Value;
        //else if (upgradeData.Type == "SkillArea")
        //    _characterData.SkillArea += upgradeData.Value;
        //else if (upgradeData.Type == "CritRate")
        //    _characterData.Critical += upgradeData.Value;

    }
}
