using UnityEngine;

public class Item : MonoBehaviour
{
    private Vector3 _startPos;
    private SphereCollider _collider;
    private Transform _player;
    private float _gainDistance = 0.5f;

    private GameObject _healingPrefab;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        //gameObject.SetActive(false);
    }
    private void Start()
    {
        if (GameManager.PlayerInstance != null)
            _player = GameManager.PlayerInstance.transform;

        _healingPrefab = Resources.Load<GameObject>("Prefabs/Item/Effects/healing");

        _startPos = transform.position;
    }
    private void Update()
    {
        transform.Rotate(0.0f, 45.0f * Time.deltaTime, 0.0f);

        float floatY = Mathf.Sin(Time.time * 2.0f) * 0.25f;
        transform.position = new Vector3(
            _startPos.x,
            _startPos.y + floatY,
            _startPos.z
        );

        float distance = Vector3.Distance(transform.position, _player.transform.position);
       
        if (distance < _gainDistance) 
        {
            GetItem();
        }
    }

    private void GetItem()
    {
        Vector3 spawnPos = _player.transform.position;
        GameObject healingEffect = Instantiate(_healingPrefab, spawnPos, Quaternion.identity, _player.transform);

        //Destroy(healingEffect, 2.0f);
        
        gameObject.SetActive(false);
    }
}
