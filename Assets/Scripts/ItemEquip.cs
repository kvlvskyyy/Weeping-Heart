using UnityEngine;
using UnityEngine.InputSystem;

public class ItemEquip : MonoBehaviour
{
    public Inventory inventory;
    public GameObject syringe;

    public InputAction addSyringeAction;
    public InputAction equipSyringeAction;
    public InputAction injectAction;

    public float syringeCount;

    private bool syringeEquipped = false;


    private void OnEnable()
    {
        addSyringeAction.Enable();
        equipSyringeAction.Enable();
        injectAction.Enable();
    }

    private void OnDisable()
    {
        addSyringeAction.Disable();
        equipSyringeAction.Disable();
        injectAction.Disable();
    }
    void Update()
    {
        if (addSyringeAction.WasPressedThisFrame())
        {
            inventory.AddSyringe(syringeCount);
        }

        if (equipSyringeAction.WasPressedThisFrame())
        {
            EquipSyringe();
        }

        if (injectAction.WasPressedThisFrame() && syringeEquipped)
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