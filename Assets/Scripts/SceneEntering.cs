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

        if (manager.patientsVisited >= manager.maxPatients)
        {
            manager.EndDay();
            return;
        }

        if (!manager.RegisterVisit()) return;

        if (PlayerMovement.isInHouse == false)
        {
            PlayerMovement.isInHouse = true;
        }
        else if (PlayerMovement.isInHouse == true)
        {
            PlayerMovement.isInHouse = false;
            DaySystemsScript.LeaveHouse();
        }

            SceneManager.LoadScene(sceneToLoad);
    }
}