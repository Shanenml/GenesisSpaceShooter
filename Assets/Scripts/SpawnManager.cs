using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _tripleshotpowerupPrefab;
    [SerializeField]
    private GameObject _enemyContainer;

    private bool _PlayerAlive = true;

    void Start()
    {
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    void Update()
    {
        
    }

    IEnumerator SpawnEnemyRoutine()
    {

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
        while (_PlayerAlive == true)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            GameObject newPowerUp = Instantiate(_tripleshotpowerupPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }

    public void OnPlayerDeath()
    {
        _PlayerAlive = false;
    }
   
}
