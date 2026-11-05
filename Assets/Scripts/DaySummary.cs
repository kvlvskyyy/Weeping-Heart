using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Cursor = UnityEngine.Cursor;

public class DaySummary : MonoBehaviour
{
    private UIDocument uiDocument;
    
    private Label villagersAliveText;
    private Label moneyEarnedText;
    private Label dayNumberText;


    private Button continueButton;
    private Button clinicButton;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = uiDocument.rootVisualElement;

        dayNumberText = root.Q<Label>("Day");
        villagersAliveText = root.Q<Label>("Villagers");
        moneyEarnedText = root.Q<Label>("Money");
        continueButton = root.Q<Button>("Continue");
        clinicButton = root.Q<Button>("Clinic");
        
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        
        UpdateSummaryPanel();

        continueButton.clicked += ContinueClicked;
        clinicButton.clicked += ClinicClicked;
    }

    public void UpdateSummaryPanel()
    {
        MoneyScript moneyScript = MoneyScript.Instance;
        GameManager manager = GameManager.Instance;
        
        if (moneyScript == null)
        {
            Debug.Log("No money script found");
            return;
        }

        if (manager == null)
        {
            Debug.Log("No gameManager script found");
            return;
        }

        int finishedDay = manager.currentDay - 1;
        
        dayNumberText.text = finishedDay.ToString();
        villagersAliveText.text = moneyScript.aliveCount.ToString();
        moneyEarnedText.text = moneyScript.moneyEarned.ToString();
    }

    void ContinueClicked()
    {
        Debug.Log("Continue clicked");
        SceneManager.LoadScene("Village");
    }

    void MainMenuClicked()
    {
        Debug.Log("MainMenu clicked");
        SceneManager.LoadScene("MainMenu");
    }

    void ClinicClicked()
    {
        Debug.Log("Clinic clicked");
        SceneManager.LoadScene("ShopMenu");
    }
}
