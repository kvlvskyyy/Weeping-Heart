using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public static MoneyScript Instance;
    public int moneyAmount = 0;
    public int moneyEarned = 0;
    public int moneyPerVillager = 0;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
