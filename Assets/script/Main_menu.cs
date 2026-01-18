using UnityEngine;
using UnityEngine.SceneManagement;

public class Main_menu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene(1);
       
    }
    public void QuitGame()
    {
        Debug.Log("Quit!");
        Application.Quit();
    }
     public void QuitToMenu()
    {
        SceneManager.LoadScene(0);
    }
   
}
