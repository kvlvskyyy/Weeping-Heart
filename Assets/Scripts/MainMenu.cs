using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;


public class MainMenu : MonoBehaviour
{
    public UIDocument mainMenu;
    private Button playButton;
    private Button tutorialButton;
    private Button quitButton;

    private void Start()
    {
        playButton = mainMenu.rootVisualElement.Q<Button>("StartButton");
        playButton.clicked += PlayClicked;
        tutorialButton = mainMenu.rootVisualElement.Q<Button>("TutButton");
        tutorialButton.clicked += TutClicked;
        quitButton = mainMenu.rootVisualElement.Q<Button>("QuitButton");
        quitButton.clicked += QuitClicked;
    }
    void PlayClicked()
    {
        Debug.Log("Play");
        SceneManager.LoadScene("ShopMenu");
    }

    void TutClicked()
    {
        Debug.Log("Tut");
    }
    void QuitClicked()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}
