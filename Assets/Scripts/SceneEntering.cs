using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntering : MonoBehaviour
{
    public PlayerMovement playerMovement;
    public string sceneToLoad;

    public void EnterScene()
    {
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
        {
            return;
        }

        PlayerMovement.isInHouse = true;
        SceneManager.LoadScene(sceneToLoad);
    }
}