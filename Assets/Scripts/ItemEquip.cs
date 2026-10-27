using UnityEngine;
using UnityEngine.InputSystem;

public class ItemEquip : MonoBehaviour
{
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
            GameManager.Instance.AddSyringe(Mathf.RoundToInt(syringeCount));
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
        if (!GameManager.Instance.HasSyringe())
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
            Debug.Log("That's not the patient dumb fuck");
            return;
        }
        if (!GameManager.Instance.UseSyringe())
        {
            Debug.Log("Broke boy");
            return;
        }

        patient.Cure(1.0);
        Debug.Log($"[SYRINGE] Injected actual ID: {patient.patientID}");
        syringe.SetActive(false);
        syringeEquipped = false;
        Debug.Log("Injected " + patient.gameObject.name);
    }
}