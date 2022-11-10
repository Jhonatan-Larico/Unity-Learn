using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullets : MonoBehaviour
{

    private Rigidbody bulletsRb;
    private PlayerController playerControllerScript;
    public Vector3 distanceToEnemy;
    // Start is called before the first frame update
    void Start()
    {
        bulletsRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //todo: how this code works?
        //gameObject.transform.Translate(new Vector3(5,0,2) * Time.deltaTime * 10);
        bulletsRb.AddForce(distanceToEnemy);



        if (gameObject.transform.position.x > 15 || gameObject.transform.position.x < -15)
            {
                Destroy(gameObject);
            }
            if (gameObject.transform.position.z > 15 || gameObject.transform.position.z < -15)
            {
                Destroy(gameObject);
            }
        }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            Destroy(other.gameObject);
        }
    }
}
