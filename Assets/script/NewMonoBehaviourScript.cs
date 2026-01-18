using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTeleporter : MonoBehaviour
{
    [Header("Einstellungen")]
    public string targetSceneName; // Name der Ziel-Szene
    public string targetExitID;    // ID des Ausgangspunkts in der neuen Szene

    
    void Start() {
    Debug.Log("Anzahl Szenen: " + SceneManager.sceneCountInBuildSettings);
    for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
    {
        // Dieser Befehl holt den Pfad der Szene aus den Build Settings
        string path = SceneUtility.GetScenePathByBuildIndex(i);
        Debug.Log("Szene am Index " + i + " heißt: " + path);
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        SceneManager.LoadScene(2);
        if (other.CompareTag("Player"))
        {
            Debug.Log("Versuche Szene zu laden: '" + targetSceneName + "'");
            // Wir speichern die ID, damit der Spieler weiß, wo er landen soll
            PlayerPrefs.SetString("LastExitID", targetExitID);
            SceneManager.LoadScene(targetSceneName);
        }
    }
}