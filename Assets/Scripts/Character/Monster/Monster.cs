using NUnit.Framework;
using UnityEngine;

public class Monster : MonoBehaviour
{
    private Player _player;
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _stopDistance = 1.0f;
    [SerializeField]
    private float _lifeTime = 5.0f;
    private float _timer;

    private void Start()
    {
        _player = GameManager.PlayerInstance;
    }

    private void OnEnable()
    {
        _timer = 0;
    }

    void Update()
    {
        _timer += Time.deltaTime;

        if(_timer >= _lifeTime)
        {
            ReturnPool();
        }

        float _distance = Vector3.Distance(transform.position, _player.transform.position);

        Vector3 _direction = (_player.transform.position - transform.position).normalized;
        transform.Translate(_direction * _speed * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    private void ReturnPool()
    {
        PoolingManager.Instance.Release("MonsterPool", gameObject);
    }
}
