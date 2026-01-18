using UnityEngine;
using UnityEngine.SceneManagement;

public class Menü : MonoBehaviour
{
    public GameObject pauseMenuUI;
    private bool isPaused = false;
    void Start()
    {

    }
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(isPaused)
                Resume();
        
            else
                Pause();
        }
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;


    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;


    }
     public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
}  

