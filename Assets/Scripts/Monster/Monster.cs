using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform Player;
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private float _stopDistance = 1.0f;

    void Update()
    {
        Trace();
    }

    private void Trace()
    {
        float _distance = Vector3.Distance(transform.position, Player.transform.position);

        if (_distance > _stopDistance)
        {
            Vector3 _direction = (Player.position - transform.position).normalized;
            transform.position += _direction * _speed * Time.deltaTime;

            Quaternion lookRotation = Quaternion.LookRotation(_direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

}
