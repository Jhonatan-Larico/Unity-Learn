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
    //Powerups
    private bool hasPowerup = false;
    private bool hasPowerup2 = false;
    private bool hasPowerup3 = false;
    public float powerupStrength = 15.0f;

    // Variables involved in AddExplosionForce
    public float powerForce = 10.0f;
    public float radius = 5.0f;
    public float upForce = 1.0f;

    private bool isOnGround ;
    public bool hasJump;
    public float jumpForce = 25.0f;
    public float downForce = 20.0f;

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

    // Detect collision with powerups
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup-1") && !hasPowerup2 && !hasPowerup3)
        {
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("Powerup-2") & !hasPowerup && !hasPowerup3)
        {
            hasPowerup2 = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
        if (other.CompareTag("Powerup-3") && !hasPowerup && !hasPowerup2 )
        {   
            hasPowerup3 = true;
            powerupIndicator.SetActive(true);
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
        }
    }

    // Wait x seconds for deactivate the powerups
    IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        hasPowerup2 = false;
        hasPowerup3 = false;
        powerupIndicator.SetActive(false);
    }

    // Powerup1, hit balls adding an extra force
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.gameObject.transform.position - gameObject.transform.position;

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);

        }
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
        }

    }

    // Powerup2, bullets are spawn against the enemies
    private void OnPowerUpBullets()
    {
        if (Input.GetKeyDown(KeyCode.Space) && hasPowerup2)
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

    }

    //Powerup3, ExplosionForce
    private void Smashattack()
    {   
        // Force push to the ground
        if (playerRb.velocity.y < 0f && !isOnGround)
        {
            playerRb.AddForce(Vector3.down * downForce, ForceMode.Force);       
        }

        if (Input.GetKeyDown(KeyCode.R)  && isOnGround && hasPowerup3 )
        {               
            playerRb.AddForce(Vector3.up*jumpForce,ForceMode.Impulse);              
            
            isOnGround = false;
            hasJump = true;
        }

        if (isOnGround && hasJump)
        {
            Vector3 explosionForcePosition = transform.position;
            Collider[] colliders = Physics.OverlapSphere(explosionForcePosition, radius);
            foreach (var enemy in colliders)
            {
                Rigidbody enemyRb = enemy.GetComponent<Rigidbody>();
                if (enemyRb != null)
                {
                    enemyRb.AddExplosionForce(powerForce, explosionForcePosition, radius, upForce, ForceMode.Impulse);
                }

            }
            hasJump = false;
        }
    }
}
