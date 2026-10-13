using UnityEngine;
using UnityEngine.InputSystem;

public class Inspection : MonoBehaviour
{
    public InputAction inspectAction;
    public InputAction selectBodyPartAction;
    public InputAction leaveInspectionAction;
    public InputAction amputateAction;
    
    public bool isInspecting = false;
    
    public Transform currentAnchor;
    
    public Light spotLight;
    public Transform selectedBodyPart;
    public LayerMask inspectableLayer;
    public PlayerMovement playerMovement;
    
    [Header("Manipulation Setiings")]
    public bool isManipulating = false;
    public float rotationSpeed = 0.5f;
    private Quaternion initialRotation;
    private float boneTwistAmount;
    
    private void OnEnable()
    {
        inspectAction.Enable();
        leaveInspectionAction.Enable();
        selectBodyPartAction.Enable();
        amputateAction.Enable();
    }

   private void OnDisable()
    {
        inspectAction.Disable();
        leaveInspectionAction.Disable();
        selectBodyPartAction.Disable();
        amputateAction.Disable();
    }
    
    void Start()
    {
        
    }

    void Update()
    {
        if (!playerMovement.isInHouse)
        {
            if (isInspecting == true)
            {
                if (isManipulating == true)
                {
                    spotLight.gameObject.SetActive(true);

                    Vector2 mouseDelta = Mouse.current.delta.ReadValue();
                    boneTwistAmount += mouseDelta.x * rotationSpeed;

                    boneTwistAmount = Mathf.Clamp(boneTwistAmount, -90f, 90f);

                    selectedBodyPart.localRotation = initialRotation * Quaternion.Euler(0f, boneTwistAmount, 0f);

                    if (selectBodyPartAction.WasReleasedThisFrame())
                    {
                        isManipulating = false;
                        selectedBodyPart.localRotation = initialRotation;
                        selectedBodyPart = null;
                    }

                    if (amputateAction.WasPressedThisFrame())
                    {
                        selectedBodyPart.localScale = Vector3.zero;
                        selectedBodyPart.GetComponent<Collider>().enabled = false;

                        isManipulating = false;
                        selectedBodyPart = null;
                        spotLight.gameObject.SetActive(false);
                    }
                }
                else
                {

                    if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 3f,
                            inspectableLayer))
                    {
                        spotLight.gameObject.SetActive(true);
                        spotLight.transform.LookAt(hit.point);

                        if (selectBodyPartAction.WasPressedThisFrame())
                        {
                            selectedBodyPart = hit.transform;
                            initialRotation = selectedBodyPart.localRotation;
                            boneTwistAmount = 0f;
                            isManipulating = true;
                        }

                    }

                    else
                    {
                        spotLight.gameObject.SetActive(false);
                    }


                    if (leaveInspectionAction.WasPressedThisFrame())
                    {
                        isInspecting = false;
                        spotLight.gameObject.SetActive(false);
                    }
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
                    }
                }
            }
        }
    }
}

