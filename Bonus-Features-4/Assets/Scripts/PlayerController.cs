using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject bullet;

    public GameObject powerupIndicator;

    public float speed = 5.0f;
    private bool hasPowerup = false;
    private bool hasPowerIcon = false;
    public float powerupStrength = 15.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.5f, 0);

        // powerup of bullets
        OnPowerUpBullets();

        // powerup of Smash attack
        Smashattack();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup") && !hasPowerIcon)
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("PowerIcon") & !hasPowerup)
        {
            hasPowerIcon = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }

    }

    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        hasPowerIcon = false;
        powerupIndicator.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - gameObject.transform.position;

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

        }
    }

    private void OnPowerUpBullets()
    {

        if (Input.GetKeyDown(KeyCode.Space) && hasPowerIcon)
        {
            StartCoroutine(OnPowerUpBulletsCoroutine());
        }

    }
    IEnumerator OnPowerUpBulletsCoroutine()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Vector3[] enemiesPositions = new Vector3[0];
        Vector3 offset = new Vector3(0, 0.5f, 0);
        Vector3 playerPosition = gameObject.transform.position;

        foreach (var enemy in enemies)
        {
            enemiesPositions = enemiesPositions.Append(enemy.transform.position).ToArray();
        }
        WaitForSeconds wait = new WaitForSeconds(0.1f);

        for (int i = 0; i < 3; i++)
        {
            //Do some logic
            foreach (var position in enemiesPositions)
            {
                Vector3 distance = (position - playerPosition);
                GameObject gunObj = Instantiate(bullet, playerPosition + offset, bullet.transform.rotation);
                gunObj.GetComponent<Bullets>().distanceToEnemy = distance;

            }

            yield return wait; //Pause the loop for 0.1 seconds.



        }
        hasPowerIcon = false;

    }


    private void Smashattack()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            playerRb.AddForce(Vector3.up *20, ForceMode.Impulse);
            //Todo
        }
    }
}
