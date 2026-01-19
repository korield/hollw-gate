using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace TMPro.Examples
{

    public class LevelTrigger : MonoBehaviour
    {
        [SerializeField] private string levelName;
        private bool playerInRange = false;

        private void OnTriggerEnter2D(Collider2D other)  // 2D statt normal!
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)  // 2D statt normal!
        {
            if (other.CompareTag("Player"))
            {
                playerInRange = false;
            }
        }

        private void Update()
        {
            if (playerInRange && Input.GetKeyDown(KeyCode.E))
            {
                SceneManager.LoadScene(levelName);
            }
        }
    }
}