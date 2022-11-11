using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    public GameObject enemyPrefab;
    public GameObject[] powerups;

    public GameObject[] superEnemies;

    private float spwanRange =9.0f;
    public int enemyCount;
    public int waveNumber = 1;

    private float timeToSawnSuperEnemies = 5.0f;
    private float time = 0;

    GameObject powerup;
    GameObject powerup2;



    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemies(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        int randomPowerup = Random.Range(0, powerups.Length);
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if(enemyCount == 0) {
            waveNumber++;
            SpawnEnemies(waveNumber); 
            powerup =  Instantiate(powerups[randomPowerup], GenerateSpawPosition(), powerups[randomPowerup].transform.rotation);
            StartCoroutine(destroyPowerIcon(powerup) );
        }

        //Spawn an superEnemy every 5 seconds and  powerup
        time += Time.deltaTime;
        if (time > timeToSawnSuperEnemies) {
            SpawnSuperEnemies();
            powerup2 = Instantiate(powerups[randomPowerup], GenerateSpawPosition(), powerups[randomPowerup].transform.rotation);
            StartCoroutine(destroyPowerIcon(powerup2));
            time = 0;
        }


    }
    //Destroy power icon after 5s
    IEnumerator destroyPowerIcon(GameObject obj)
    {
        yield return new WaitForSeconds(5);
        Destroy(obj);
    }

    void SpawnEnemies(int enemisToSpawn)
    {
        for(int i = 0; i < enemisToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawPosition(), enemyPrefab.transform.rotation);

        }
    }

    void SpawnSuperEnemies()
    {
        int randomSuperEnemy = Random.Range(0, superEnemies.Length);
        Instantiate(superEnemies[randomSuperEnemy], GenerateSpawPosition(), enemyPrefab.transform.rotation);
    }

    private Vector3 GenerateSpawPosition()
    {
        float spawnPosX = Random.Range(-spwanRange, spwanRange);
        float spawnPosZ = Random.Range(-spwanRange, spwanRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
