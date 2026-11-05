using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public static MoneyScript Instance;
    public int moneyAmount = 0;
    public int moneyEarned = 0;
    public int aliveCount = 0;
    public int moneyPerVillager = 10;
    
    public GameManager manager;
    

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        manager = GameManager.Instance;
    }

    public void UpdateMoneyAmount()
    {
        aliveCount = 0;
        foreach (PatientData patient in manager.allPatients)
        {
            if (!patient.lost)
            {
                aliveCount++;
            }
        }
            
        moneyEarned = moneyPerVillager * aliveCount;
        moneyAmount += moneyEarned;
    }
    public void SubtractMoneyAmount(int amount)
    {
        moneyAmount -= amount;
    }
    
}
