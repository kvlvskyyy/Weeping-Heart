using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
public class ShopScript : MonoBehaviour
{
    public UIDocument shopMenu;
    private Button bandageBuyButton;
    private Button syringeBuyButton;
    private Button leaveShopButton;
    
    private Label syringePriceLabel;
    private Label bandagePriceLabel;

    public int syringePrice = 0;
    public int bandagePrice = 0;

    void Start()
    {
        syringePriceLabel = shopMenu.rootVisualElement.Q<Label>("SyringePrice");
        syringePriceLabel.text = syringePrice.ToString();
        bandagePriceLabel = shopMenu.rootVisualElement.Q<Label>("BandagePrice");
        bandagePriceLabel.text = bandagePrice.ToString();
        bandageBuyButton = shopMenu.rootVisualElement.Q<Button>("BandageBuy");
        bandageBuyButton.clicked += BuyBandage;
        syringeBuyButton = shopMenu.rootVisualElement.Q<Button>("SyringeBuy");
        syringeBuyButton.clicked += BuySyringe;
        leaveShopButton = shopMenu.rootVisualElement.Q<Button>("LeaveButton");
        leaveShopButton.clicked += ToVillage;
    }

    void BuyBandage()
    {
        Debug.Log("Bangage");
    }
    void BuySyringe()
    {
        GameManager.Instance.AddSyringe(1);
        Debug.Log("Syring");
    }
    void ToVillage()
    {
        SceneManager.LoadScene("Village");
    }
}
