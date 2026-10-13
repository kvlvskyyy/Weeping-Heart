using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntering : MonoBehaviour
{

    public PlayerMovement playerMovement;
    
    public string sceneToLoad;
    
    void Update()
    {
        if (playerMovement.isEntering)
        {
            playerMovement.isEntering = false;
            
            SceneManager.LoadScene(sceneToLoad);
        }
}
}
