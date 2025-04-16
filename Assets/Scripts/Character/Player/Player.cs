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

    private bool _isMoving;
    private Vector3 _velocity;

    [SerializeField] 
    private Transform _weaponSocket;
    private GameObject _playerWeapon;
    private WeaponData _weaponData;

    private void Start()
    {
        _animator = GetComponent<Animator>();

        _characterData.Key = DataManager.Instance.GetCharacterData(101).Key;

        LoadPlayerData(_characterData.Key);

        _curState = State.Move;
    }
    private void Update()
    {
        Control();
        Move();
        UpdateAnimator();

        if (Input.GetKeyDown(KeyCode.F1))
        {
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

    private void Control()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _curState = State.BaseAttack;
            _animator.Play("MagicAttack");
        }
    }
    private void Move()
    {
        if (_curState != State.Move)
        {
            _velocity = Vector3.zero;
            return;
        }

        else
        {
            Vector3 playerScreenPos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 mouseScreenPos = Input.mousePosition;
            Vector3 dir = mouseScreenPos - playerScreenPos;
            float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            if(Input.GetKey(KeyCode.W))
            {
                _isMoving = true;
                _velocity = transform.forward * _characterData.MoveSpeed;
                transform.position += _velocity * Time.deltaTime;
            }
            else
            {
                _isMoving = false;
                _velocity = Vector3.zero;
            }
        }  
    }
    private void LoadPlayerData(int key)
    {
        CharacterData data = DataManager.Instance.GetCharacterData(key);

        _characterData.Level = data.Level;
        _characterData.AttackPower = data.AttackPower;
        _characterData.MaxHp = data.MaxHp;
        _characterData.MoveSpeed = data.MoveSpeed;
        _characterData.AttackSpeed = data.AttackSpeed;
    }
    private void UpdateAnimator()
    {
        _animator.SetBool("IsMoving", _isMoving);
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
