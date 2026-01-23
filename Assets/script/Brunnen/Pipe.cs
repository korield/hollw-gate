using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float speed = 3f;

    void Update()
    {
        transform.position += Vector3.left * speed * Time.deltaTime;

        // Zerstöre Pipe wenn außerhalb des Bildschirms
        if (transform.position.x < -20)
        {
            Destroy(gameObject);
        }
    }
}