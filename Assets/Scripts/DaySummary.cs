using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
public class DaySummary : MonoBehaviour
{
    private UIDocument uiDocument;
    
    private Label villagersAliveText;
    private Label moneyEarnedText;

    private void Awake()
    {
        uiDocument = GetComponent<UIDocument>();
    }

    private void OnEnable()
    {
        VisualElement root = uiDocument.rootVisualElement;

        villagersAliveText = root.Q<Label>("Villagers");
        moneyEarnedText = root.Q<Label>("Money");
        
        UpdateSummaryPanel();
    }

    public void UpdateSummaryPanel()
    {
        MoneyScript moneyScript = MoneyScript.Instance;
        GameManager manager = GameManager.Instance;
        if (moneyScript == null || manager == null) return;

        int aliveCount = 0;
        foreach (PatientData patient in manager.allPatients)
        {
            if (!patient.lost)
            {
                aliveCount++;
            }
        }

        villagersAliveText.text = aliveCount.ToString();
        moneyEarnedText.text = moneyScript.moneyAmount.ToString();
    }
}
