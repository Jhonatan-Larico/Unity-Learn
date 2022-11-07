using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{   
    public GameObject enemyPrefab;
    private float spwanRange =9.0f;

    // Start is called before the first frame update
    void Start()
    {
        //Vector3 randomPos = GenerateSpawPosition();

        Instantiate(enemyPrefab,GenerateSpawPosition(), enemyPrefab.transform.rotation );

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private Vector3 GenerateSpawPosition()
    {
        float spawnPosX = Random.Range(-spwanRange, spwanRange);
        float spawnPosZ = Random.Range(-spwanRange, spwanRange);

        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomPos;
    }
}
