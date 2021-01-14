using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab; //name of variable should always describe what it contains or what is doing
    [SerializeField]
    private GameObject _TripleShotPrefab;
    [SerializeField]
    private GameObject _explodingEnemyPrefab;

[SerializeField]
private GameObject _enemyContainer;

private bool _StopSpawning = false;

    void Start()
    {
        StartCoroutine(SpawnRoutine()); //can also do "SpawnRoutine" but its less efficient
        StartCoroutine(SpawnPowerupRoutine());
        StartCoroutine(SpawnExplodingEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator SpawnRoutine()
    {
        //spawn our enemies every 5s
        while(_StopSpawning == false)
        {
            //posToSpawn variable containing the position where the enemy would spawn
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f, 9.2f), 7.5f, 0);
            //Create a copy of the enemy
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity); //so it uses rotation of object(quaternion) /instantiate creates copy of one object
            // set newEnemy as a child to enemy Container
            newEnemy.transform.SetParent(_enemyContainer.transform);
            //wait for 5s before repeating the code above
            yield return new WaitForSeconds(5f);
        }
        
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while(_StopSpawning ==false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f,9.2f), 7f, 0);
            Instantiate(_TripleShotPrefab, posToSpawn, Quaternion.identity);
            //wait a random number of seconds within the range of 6 to 9 secs before spawning another powerup
            yield return new WaitForSeconds(Random.Range(6,10));
        }
    }

    IEnumerator SpawnExplodingEnemyRoutine()
    {
        while(_StopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-9.2f, 9.2f),7f,0);
            Instantiate(_explodingEnemyPrefab,posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3,7));
        }
    }

    public void OnPlayerDeath()
    {
        _StopSpawning = true;
        
    }
}
