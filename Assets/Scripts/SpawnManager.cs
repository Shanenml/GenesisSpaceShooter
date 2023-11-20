using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject[] powerup;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _PlayerAlive = true;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void StartSpawning()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
        StartCoroutine(SpawnAmmoRoutine());
        StartCoroutine(SpawnHealthRoutine());
    }
    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_PlayerAlive == true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5.0f);
        }
            
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(3f);
        while (_PlayerAlive == true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randompowerup = Random.Range(0, 3);
            Instantiate(powerup[randompowerup], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
    }

    IEnumerator SpawnAmmoRoutine()
    {

        yield return new WaitForSeconds(5f);
        while (_PlayerAlive == true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(powerup[3], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }

    IEnumerator SpawnHealthRoutine()
    {
        yield return new WaitForSeconds(10f);
        while (_PlayerAlive == true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(powerup[4], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(20f, 30f));
        }
    }

    public void OnPlayerDeath()
    {
        _PlayerAlive = false;
    }
   
}
