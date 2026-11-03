using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public static MoneyScript Instance;
    public int moneyAmount = 0;

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
}
