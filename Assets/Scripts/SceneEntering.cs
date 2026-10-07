using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEntering : MonoBehaviour
{

    public Inspection inspectionScript;
    
    public string sceneToLoad;
    
    void Update()
    {
        if (inspectionScript.isEntering)
        {
            inspectionScript.isEntering = false;
            
            SceneManager.LoadScene(sceneToLoad);
        }
}
}
