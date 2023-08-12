using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn_manager : MonoBehaviour
{
    [SerializeField]
    private GameObject _EnemyPrefab;
    [SerializeField]
    private GameObject _EnemyContainer;
    [SerializeField]
    private GameObject[] Powerups;

    private bool _stopspawning = false;



    // Start is called before the first frame update
    void Start()
    {


    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }
    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopspawning == false)
        {
            float randomx = Random.Range(-4.8f, 4.8f);
            GameObject newEnemy = Instantiate(_EnemyPrefab, transform.position + new Vector3(randomx, 4, 0), Quaternion.identity);
            newEnemy.transform.parent = _EnemyContainer.transform;
            yield return new WaitForSeconds(5);

        }
    }
    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopspawning == false)
        {
            float randomxp = Random.Range(-4.8f, 4.8f);
            int randompup = Random.Range(0, 3);
            Instantiate(Powerups[randompup], transform.position + new Vector3(randomxp, 4, 0), Quaternion.identity);
            yield return new WaitForSeconds(7);
            
        }

    }
    public void onplayerdeath()
    {
        _stopspawning = true;
    }
}
