using UnityEngine;

public class ItemEquip : MonoBehaviour
{
    public Inventory inventory;
    public GameObject syringe;

    private bool syringeEquipped = false;
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

        if (Input.GetMouseButtonDown(0) && syringeEquipped)
        {
            TryInject();
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

    void TryInject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (!Physics.Raycast(ray, out hit))
        {
            Debug.Log("What the fuck are you aiming at");
            return;
        }

        PatientInfection patient = hit.collider.GetComponentInParent<PatientInfection>();

        if (patient == null)
        {
            return;
        }

        if (!inventory.UseSyringe())
        {
            Debug.Log("Nope");
            return;
        }

        patient.Cure(1.0);
        syringe.SetActive(false);
        syringeEquipped = false;

        Debug.Log("Success");
    }
}