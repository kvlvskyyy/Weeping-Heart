using UnityEngine;
using UnityEngine.InputSystem;

public class ItemEquip : MonoBehaviour
{
    public GameObject syringe;

    public InputAction addSyringeAction;
    public InputAction equipSyringeAction;
    public InputAction injectAction;
    
    public LayerMask injectableLayer;

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
        
            GameManager.Instance.AddItem(Mathf.RoundToInt(syringeCount), "Syringe");

        if (equipSyringeAction.WasPressedThisFrame()) EquipSyringe();

        if (injectAction.WasPressedThisFrame() && syringeEquipped) TryInject();
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

        if (!Physics.Raycast(ray, out hit, 5f))
        {
            Debug.Log("genuinely how do you miss this hard");
            return;
        }

        Debug.Log("missed");

        PatientInfection patient = hit.collider.GetComponentInParent<PatientInfection>();
        if (patient == null)
        {
            Debug.Log("there's.. no patient??");
            return;
        }

        if (!GameManager.Instance.UseSyringe())
        {
            Debug.Log("broke ass boy");
            return;
        }

        patient.Cure(1.0);

        syringe.SetActive(false);
        syringeEquipped = false;

        Debug.Log("Injected " + patient.gameObject.name);
    }
}