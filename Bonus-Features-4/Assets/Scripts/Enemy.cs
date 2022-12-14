using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 3.0f;
    private GameObject player;
    private Rigidbody enemyRb;
    private Rigidbody superEnemyRb;
    // Start is called before the first frame update
    void Start()
    {
     enemyRb = GetComponent<Rigidbody>();
     player = GameObject.Find("Player");
    
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce( lookDirection * speed);

        if(enemyRb.position.y < -10) { Destroy(gameObject); }

        if(gameObject.tag == "SuperEnemy")
        {
            Vector3 lookDirection2 = (player.transform.position - transform.position)   ;
            enemyRb.AddForce(lookDirection2 * speed);
        }
    }
}
