using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperEnemies : MonoBehaviour
{
    public float speed = 3.0f;
    private GameObject player;
    private Rigidbody superEnemyRb;

    // Start is called before the first frame update
    void Start()
    {
        superEnemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        superEnemyRb.AddForce(lookDirection * speed);

        if (superEnemyRb.position.y < -10) { Destroy(gameObject); }

      
    }
}
