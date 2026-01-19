using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class FlappyBirdGame : MonoBehaviour
{
    public Rigidbody2D player;
    public float jumpForce = 5f;
    public TextMeshProUGUI scoreText;
    public GameObject pipePrefab;
    public Transform spawnPoint;

    private int score = 0;
    private float spawnTimer = 0;
    private bool gameOver = false;

    void Start()
    {
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        if (!gameOver)
        {
            // Springen mit Space oder E
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
            {
                player.linearVelocity = Vector2.up * jumpForce;
            }

            // Hindernisse spawnen
            spawnTimer += Time.deltaTime;
            if (spawnTimer > 2.5f)
            {
                SpawnPipe();
                spawnTimer = 0;
            }
        }

        // Restart mit R
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void SpawnPipe()
    {
        float randomY = Random.Range(-1.5f, 1.5f);
        Vector3 spawnPos = new Vector3(10, randomY, 0);
        Instantiate(pipePrefab, spawnPos, Quaternion.identity);
    }

    public void AddScore()
    {
        score++;
        scoreText.text = "Score: " + score;

        if (score >= 5) // Gewonnen hier nach 5 Punkten
        {
            Win();
        }
    }

    public void GameOver()
    {
        if (!gameOver)
        {
            gameOver = true;
            player.linearVelocity = Vector2.zero;
            scoreText.text = "Game Over! Drücke R zum Neustarten";
        }
    }

    void Win()
    {
        gameOver = true;
        scoreText.text = "Gewonnen!";
        Invoke("LoadNextLevel", 2f);
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene("LVL 1");
    }
}