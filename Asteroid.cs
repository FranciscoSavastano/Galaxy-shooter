using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField]
    private float _AsteroidRotationSpeed = 18.0f;
    [SerializeField]
    private GameObject _ExplosionSprite;
    private GameObject _Player;
    private Spawn_manager _SpawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _SpawnManager = GameObject.Find("Spawn_manager").GetComponent<Spawn_manager>();
        if(_SpawnManager == null)
        {
            Debug.Log("SpawnManager is null");
        }

    }

    // Update is called once per frame
    void Update()
    {

        transform.Rotate(Vector3.forward * _AsteroidRotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            Instantiate(_ExplosionSprite, transform.position + new Vector3(0, 0, 0), Quaternion.identity);
            Destroy(this.gameObject, 0.25f);
            _SpawnManager.StartSpawning();
            Destroy(_ExplosionSprite, 2.8f);
        }
    }
}
