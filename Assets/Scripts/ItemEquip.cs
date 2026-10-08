using UnityEngine;

public class ItemEquip : MonoBehaviour
{
    public Inventory inventory;
    public GameObject syringe;

    private bool syringeEquipped = false;

    private void Start()
    {
        Debug.Log("ItemEquip has started!");
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            inventory.AddSyringe(1);
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            EquipSyringe();
        }
    }

    void EquipSyringe()
    {
        if (!inventory.HasSyringe())
        {
            Debug.Log("Nope");
            return;
        }

        syringeEquipped = true;
        syringe.SetActive(true);

        Debug.Log("Syringe Equip");
    }
}