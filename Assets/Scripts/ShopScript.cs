using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class ShopScript : MonoBehaviour
{
    private MoneyScript moneyScript;
    public UIDocument shopMenu;
    private Button bandageBuyButton;
    private Button syringeBuyButton;
    private Button leaveShopButton;
    
    private Label syringePriceLabel;
    private Label bandagePriceLabel;
    private Label balanceLabel;

    public int syringePrice = 0;
    public int bandagePrice = 0;

    void Start()
    {
        moneyScript = MoneyScript.Instance;
        if (moneyScript == null)
        {
            Debug.Log("No MoneyScript found");
            return;
        }

        balanceLabel = shopMenu.rootVisualElement.Q<Label>("Balance");
        balanceLabel.text = moneyScript.moneyAmount.ToString();
        syringePriceLabel = shopMenu.rootVisualElement.Q<Label>("SyringePrice");
        syringePriceLabel.text = $"$" + syringePrice;
        bandagePriceLabel = shopMenu.rootVisualElement.Q<Label>("BandagePrice");
        bandagePriceLabel.text = $"$" + bandagePrice;
        bandageBuyButton = shopMenu.rootVisualElement.Q<Button>("BandageBuy");
        bandageBuyButton.clicked += () => BuyItem(bandagePrice, "Bandage");
        syringeBuyButton = shopMenu.rootVisualElement.Q<Button>("SyringeBuy");
        syringeBuyButton.clicked += () => BuyItem(syringePrice, "Syringe");
        leaveShopButton = shopMenu.rootVisualElement.Q<Button>("LeaveButton");
        leaveShopButton.clicked += ToVillage;
    }
    
    void BuyItem(int price, string itemName)
    {
        if (moneyScript.moneyAmount >= price)
        {
            moneyScript.SubtractMoneyAmount(price);
            GameManager.Instance.AddItem(1, itemName);
            balanceLabel.text = moneyScript.moneyAmount.ToString();
            Debug.Log($"Added Item " + itemName);
        }

        else Debug.Log("Not enough money");
        
        
    }
    void ToVillage()
    {
        SceneManager.LoadScene("Village");
    }
}
