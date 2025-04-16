using UnityEngine;

public class TopViewMode : MonoBehaviour
{
    [SerializeField]
    private Transform _target;
    [SerializeField]
    private Vector3 _positionOffset = new Vector3(0, 5.5f, -1.0f);
    [SerializeField]
    private Vector3 _rotationOffset = new Vector3(75.0f, 0f, 0f);

    private void Start()
    {
        _target = GameManager.Instance._playerInstance.transform;
    }
    private void Update()
    {
        if (_target == null) return;

        transform.position = _target.position + _positionOffset;
        transform.rotation = Quaternion.Euler(_rotationOffset);
    }
}
