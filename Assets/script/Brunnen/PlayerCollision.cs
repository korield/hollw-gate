using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Object.FindFirstObjectByType<FlappyBirdGame>().GameOver();
        }

        if (other.CompareTag("Score"))
        {
            Object.FindFirstObjectByType<FlappyBirdGame>().AddScore();
            Destroy(other.gameObject);
        }
    }
}           