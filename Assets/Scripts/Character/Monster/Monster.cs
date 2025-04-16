using UnityEngine;

public class Monster : MonoBehaviour
{
    private GameObject _player;
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _stopDistance = 1.0f;

    private void Start()
    {
        _player = GameManager.Instance._playerInstance;
    }
    void Update()
    {
        float _distance = Vector3.Distance(transform.position, _player.transform.position);

        Vector3 _direction = (_player.transform.position - transform.position).normalized;
        transform.Translate(_direction * _speed * Time.deltaTime);

        Quaternion lookRotation = Quaternion.LookRotation(_direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }


}
