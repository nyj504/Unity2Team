using NUnit.Framework;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public enum MonState
    {
        MonMove,
        MonAttack,
        MonDead
    }

    private Player _player;
    private SphereCollider _sphereCollider;
    private Animator _MonAnimator;

    private MonState _curState;
    private bool _isMoving = true;

    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _stopDistance = 1.0f;
    [SerializeField]
    private float _lifeTime = 5.0f;
    [SerializeField]
    private float pushRadius = 1.0f;
    [SerializeField]
    private float pushStrength = 2.0f;

    private float _timer;

    private void Start()
    {
        _player = GameManager.PlayerInstance;
        _sphereCollider = GetComponent<SphereCollider>();

        _curState = MonState.MonMove;
    }

    private void OnEnable()
    {
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if (_timer >= _lifeTime)
        {
            ReturnPool();
            return;
        }

        Trace();
        MonsterPush();
        PlayAttackAnim();
    } 

    private void Trace()
    {
        float distance = Vector3.Distance(transform.position, _player.transform.position);

        if (distance > _stopDistance)
        {
            _isMoving = false;

            Vector3 direction = (_player.transform.position - transform.position).normalized;
            transform.Translate(direction * _speed * Time.deltaTime);

            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);

        }
    }

    private void MonsterPush()
    {
        Collider[] nearby = Physics.OverlapSphere(transform.position, pushRadius);

        foreach (var other in nearby)
        {
            if (other.CompareTag("Monster") && other.transform != transform)
            {
                Vector3 dir = (transform.position - other.transform.position).normalized;
                float distance = Vector3.Distance(transform.position, other.transform.position);
                float pushPower = (pushRadius - distance) / pushRadius;

                // 위치 직접 보정
                transform.position += dir * pushStrength * pushPower * Time.deltaTime;
            }
        }
    }

    private void PlayAttackAnim()
    {
        if (_curState == MonState.MonMove)
            return;

        if(!_isMoving)
        {
            _curState = MonState.MonAttack;
            _MonAnimator.Play("Attack");
        }
    }

    private void ReturnPool()
    {
        PoolingManager.Instance.Release("MonsterPool", gameObject);
    }
}
