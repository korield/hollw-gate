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

    // --- Win / Fly-out Einstellungen ---
    public float winFlySpeed = 7f;
    public Vector2 winFlyDirection = new Vector2(1f, 1f);
    public float winDuration = 2f;
    private bool isWinFlying = false;
    private float winTimer = 0f;

    void Start()
    {
        scoreText.text = "Score: 0";
    }

    void Update()
    {
        if (!gameOver && !isWinFlying)
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

        // Wenn der Spieler gerade "nach oben-rechts fliegt", Timer hochz�hlen und evtl. Szene laden
        if (isWinFlying)
        {
            winTimer += Time.deltaTime;
            if (winTimer >= winDuration)
            {
                LoadNextLevel();
            }
        }

        // Restart mit R
        if (gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void FixedUpdate()
    {
        if (isWinFlying)
        {
            // konstante Fluggeschwindigkeit nach oben-rechts setzen
            Vector2 dir = winFlyDirection.normalized;
            player.linearVelocity = dir * winFlySpeed;
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

        if (score >= 10) // PunktezumGewinnen
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
            scoreText.text = "Game Over! Dr�cke R zum Neustarten";
        }
    }

    void Win()
    {
        if (gameOver) return;
        gameOver = true;
        scoreText.text = "Gewonnen!";

        // Start Win-Fly
        StartWinFly();

        // Fallback: lade nach winDuration (wenn FixedUpdate/Update den Load nicht �bernimmt)
        // Hier nicht zus�tzlich Invoke benutzt, weil LoadNextLevel durch winTimer/FixedUpdate getriggert wird
    }

    void StartWinFly()
    {
        isWinFlying = true;
        winTimer = 0f;

        // Deaktiviere Gravitation und Kollision, damit Flug sauber ist
        if (player != null)
        {
            player.gravityScale = 0f;
            player.linearVelocity = winFlyDirection.normalized * winFlySpeed;
            Collider2D col = player.GetComponent<Collider2D>();
            if (col != null) col.enabled = false;
        }

        // Versuche Kamera-Tracking zu stoppen:
        if (Camera.main != null)
        {
            // Falls die Kamera als Kind des Spielers h�ngt => entkoppeln
            Camera.main.transform.SetParent(null);

            // Heuristisch: alle Komponenten an der Kamera deaktivieren, deren Typname "Follow" oder "Cinemachine" enth�lt.
            // Falls ihr ein spezifisches Kamera-Follow-Skript habt, besser direkt den Typnamen anpassen.
            var behaviours = Camera.main.GetComponents<MonoBehaviour>();
            foreach (var b in behaviours)
            {
                if (b == null) continue;
                var typeName = b.GetType().Name;
                if (typeName.Contains("Follow") || typeName.Contains("Cinemachine"))
                {
                    b.enabled = false;
                }
            }
        }
    }

    void LoadNextLevel()
    {
        SceneManager.LoadScene("LVL 1");
    }
}