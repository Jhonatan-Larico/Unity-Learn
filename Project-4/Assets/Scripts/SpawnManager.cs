using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;
    private float spwanRange =9.0f;
    public int enemyCount;
    public int waveNumber = 1;


    // Start is called before the first frame update
    void Start()
    {

        SpawnEnemies(waveNumber);
        Instantiate(powerupPrefab, GenerateSpawPosition(), powerupPrefab.transform.rotation);

    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0) {
            waveNumber++;
            SpawnEnemies(waveNumber); 
            Instantiate(powerupPrefab,GenerateSpawPosition(),powerupPrefab.transform.rotation);
        }
    }
    void SpawnEnemies(int enemisToSpawn)
    {
        for(int i = 0; i < enemisToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawPosition(), enemyPrefab.transform.rotation);

        }
    }
    private Vector3 GenerateSpawPosition()
    {
        float spawnPosX = Random.Range(-spwanRange, spwanRange);
        float spawnPosZ = Random.Range(-spwanRange, spwanRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
