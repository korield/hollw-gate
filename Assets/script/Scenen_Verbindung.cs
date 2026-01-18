using UnityEngine;

public class SceneEntrance : MonoBehaviour
{
    public string entranceID; // Muss mit der 'targetExitID' übereinstimmen

    void Start()
    {
        // Prüfen, ob dies der richtige Eingang für den Spieler ist
        if (PlayerPrefs.GetString("LastExitID") == entranceID)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                player.transform.position = transform.position;
            }
        }
    }
}