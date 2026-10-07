using UnityEngine;
using UnityEngine.InputSystem;

public class Inspection : MonoBehaviour
{
    public InputAction inspectAction;
    public InputAction leaveInspectionAction;
    public bool isInspecting = false;
    public bool isEntering;
    public Transform currentAnchor;

    private void OnEnable()
    {
        inspectAction.Enable();
        leaveInspectionAction.Enable();
    }

   private void OnDisable()
    {
        inspectAction.Disable();
        leaveInspectionAction.Disable();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (isInspecting == true)
        {
            if (leaveInspectionAction.WasPressedThisFrame())
            {
                isInspecting = false;
            }
        }
        else
        { 
            if (inspectAction.WasPressedThisFrame())
            {
                if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 3f))
                {
                    if (hit.collider.tag == "Patient")
                    {
                        currentAnchor = hit.transform.GetChild(0);
                        isInspecting = true;
                    }
                    
                    if (hit.collider.tag == "House")
                    {
                        isEntering = true;
                    }
                }
                }
        }
    }
}
