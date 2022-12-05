using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using UnityEngine.SceneManagement;// Load and reload scenes
using UnityEngine.UI; // Interact with buttons

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;
    private float spawnRate = 1.0f;
    private int score;
    public bool isGameActive;
    public Button restartButton;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI livesText;
    public GameObject titleScreen;
    public Image backgroundPause;
    public int lives ;
    public bool isPause=false;

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        PauseGame();
        if(lives == 0)
        {
            GameOver(); 
        }

    }

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public  void GameOver()
    {
        GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = false;
        gameOverText.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(true);   
        isGameActive= false;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void StartGame(int difficulty)
    {
        GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = true;
        lives = 3;
        livesText.text = "Lives: " + lives;
        spawnRate /= difficulty;
        isGameActive = true;
        StartCoroutine(SpawnTarget());
        score = 0;
        UpdateScore(0);
        titleScreen.SetActive(false);
    }
    
    public void PauseGame()
    {   
        if (Input.GetKeyDown(KeyCode.Space) && !isPause)
        {   
            Time.timeScale = 0;
            isPause = true;
            backgroundPause.gameObject.SetActive(true);
            GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = false;
        }
        else if (Input.GetKeyDown(KeyCode.Space) && isPause)
        {
            Time.timeScale = 1;
            isPause = false;
            backgroundPause.gameObject.SetActive(false);
            GameObject.Find("Main Camera").GetComponent<CursorTrail>().enabled = true;


        }
    }

}
