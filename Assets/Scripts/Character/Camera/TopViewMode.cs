using UnityEngine;

public class TopViewMode : MonoBehaviour
{
    public Transform target;
    [SerializeField]
    private Vector3 positionOffset = new Vector3(0, 5.5f, -1.0f);
    [SerializeField]
    private Vector3 rotationOffset = new Vector3(75.0f, 0f, 0f);

    private void Update()
    {
        if (target == null) return;

        transform.position = target.position + positionOffset;
        transform.rotation = Quaternion.Euler(rotationOffset);
    }
}
