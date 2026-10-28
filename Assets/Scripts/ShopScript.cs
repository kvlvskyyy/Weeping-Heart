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

    void Start()
    {
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
    }
    void ToVillage()
    {
        SceneManager.LoadScene("Village");
    }
}
