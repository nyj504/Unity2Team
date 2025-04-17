using UnityEngine;

public class Item : MonoBehaviour
{
    private Vector3 _startPos;
    private SphereCollider _collider;
    private Transform _player;
    private float _gainDistance = 1.0f;

    private GameObject _particlePrefab;
    private ItemData _itemData;

    private void Awake()
    {
        _collider = GetComponent<SphereCollider>();
        gameObject.SetActive(false);
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

    public void SpawnItem(int key)
    {
        float range = 5.0f;

        float randomX = Random.Range(-range, range);
        float randomZ = Random.Range(-range, range);

        Vector3 randomPos = new Vector3(randomX, 0.0f, randomZ);

        transform.position += randomPos;
        _startPos = randomPos;

        if (GameManager.PlayerInstance != null)
            _player = GameManager.PlayerInstance.transform;

        string path = "Prefabs/Item/Effects/";
        _itemData = DataManager.Instance.GetItemData(key);
        _particlePrefab = Resources.Load<GameObject>(path + _itemData.ParticlePath);

        gameObject.SetActive(true);
    }

    private void GetItem()
    {
        Vector3 spawnPos = _player.transform.position;
        GameObject healingEffect = Instantiate(_particlePrefab, spawnPos, Quaternion.identity, _player.transform);

        //Destroy(healingEffect, 2.0f);
        
        gameObject.SetActive(false);
    }
}
