using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntering : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public string sceneToLoad;

    private void Update()
    {
        if (playerMovement == null ||
            !playerMovement.isEntering)
            return;

        playerMovement.isEntering = false;

        GameManager manager = GameManager.Instance;

        if (manager == null)
        {
            Debug.LogError("No gamemanager doofus");
            return;
        }

        if (!manager.CanVisitPatient())
        {
            Debug.Log("Done 4 2day");
            return;
        }

        if (!manager.RegisterVisit())
            return;

        PlayerMovement.isInHouse = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}