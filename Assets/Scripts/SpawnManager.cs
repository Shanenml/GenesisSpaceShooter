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
    private int _PlayersLives;

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
        StartCoroutine(SpawnRefillablesRoutine());
        StartCoroutine(SpawnRarePowerUpRoutine());
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
            Debug.LogError("Power Up Spawn" + randompowerup);
            yield return new WaitForSeconds(Random.Range(10f, 20f));
        }
    }

    IEnumerator SpawnRarePowerUpRoutine()
    {
        yield return new WaitForSeconds(20f);
        while (_PlayerAlive == true)
        {
            Vector3 postToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(powerup[5], postToSpawn, Quaternion.identity);
            Debug.LogError("Rare Power Up Spawn");
            yield return new WaitForSeconds(Random.Range(20f, 30f));
        }
    }

    IEnumerator SpawnRefillablesRoutine()
    {

        yield return new WaitForSeconds(10f);
        while (_PlayerAlive == true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            if(_PlayersLives < 3)
            {
                //spawn Health Collectable
                Instantiate(powerup[4], posToSpawn, Quaternion.identity);
                Debug.LogError("Health Pack Spawned");
            }
            else
            {
                //spawn Ammo Collectable
                Instantiate(powerup[3], posToSpawn, Quaternion.identity);
                Debug.LogError("Ammo Pack Spawned");
            }
            
            yield return new WaitForSeconds(Random.Range(15f, 20f));
        }
    }

    public void OnPlayerDeath()
    {
        _PlayerAlive = false;
    }

    public void UpdateLives(int PlayersLives)
    {
        _PlayersLives = PlayersLives;
    }
   
}
