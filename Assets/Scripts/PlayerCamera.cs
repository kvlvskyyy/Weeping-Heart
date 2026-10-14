using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")] 
    public Transform playerBody;
    public Transform head;
    public Inspection inspectionScript;

    [Header("Lantern Settings")] 
    public Transform lanternAnchor;
    public InputAction lanternAction;
    public float lanternMoveSpeed = 0.005f;
    public float lanternLimitX = 0.8f;
    public float lanternLimitY = 0.8f;
    private Vector3 lanternStartPos;

    [Header("Camera Settings")] 
    public float sensitivity = 15f;
    public float inspectionSensitivity = 15f;
    public float maxLookAngle = 89f;
    
    [Header("Inspection Settings")]
    public float panSpeed = 1f;

    public float panLimitX = 1f;
    public float panLimitY = 1f;
    
    private float cameraPitch = 0f;
    private Vector3 startPosition;
    private Vector2 lookInput;
    private void Start()
    {
        startPosition = head.localPosition;
        lanternStartPos = lanternAnchor.localPosition;
        
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        lanternAction.Enable();
    }

    private void OnDisable()
    {
        lanternAction.Disable();
    }

    private void Update()
    {
        if (Mouse.current != null)
        {
            lookInput = Mouse.current.delta.ReadValue();
        }
    }

    private void LateUpdate()
    {
        float mouseX;
        float mouseY;

        if (!inspectionScript.isInspecting)
        {
            mouseX = lookInput.x * sensitivity * 0.01f;
            mouseY = lookInput.y * sensitivity * 0.01f;
            
            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
            head.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            head.localPosition = startPosition;
            
            lanternAnchor.localPosition = Vector3.Lerp(lanternAnchor.localPosition, lanternStartPos, Time.deltaTime * 5f);
            
        }

        else
        {
            mouseX = lookInput.x * inspectionSensitivity * 0.01f;
            mouseY = lookInput.y * inspectionSensitivity * 0.01f;
            
            cameraPitch = 0f;
            head.localRotation = Quaternion.Euler(0f, 0f, 0f);
            playerBody.rotation = inspectionScript.currentAnchor.rotation;
            playerBody.position = inspectionScript.currentAnchor.position;

            if (!inspectionScript.isManipulating)
            {
                if (lanternAction.IsPressed())
                {
                    Vector3 newPos = lanternAnchor.localPosition;
                    newPos.x += lookInput.x * lanternMoveSpeed;
                    newPos.y += lookInput.y * lanternMoveSpeed;
                    
                    newPos.x = Mathf.Clamp(newPos.x, lanternStartPos.x - lanternLimitX, lanternStartPos.x + lanternLimitX);
                    newPos.y = Mathf.Clamp(newPos.y, lanternStartPos.y - lanternLimitY, lanternStartPos.y + lanternLimitY);
                    
                    lanternAnchor.localPosition = newPos;
                }
                else
                {
                    Vector3 newPosition = head.localPosition;
                    newPosition.x += mouseX * panSpeed;
                    newPosition.y += mouseY * panSpeed; 
                    newPosition.x = Mathf.Clamp(newPosition.x, startPosition.x - panLimitX, startPosition.x + panLimitX);
                    newPosition.y = Mathf.Clamp(newPosition.y, startPosition.y - panLimitY, startPosition.y + panLimitY);
                    head.localPosition = newPosition;
                    
                   

                }
            }
            
        }
       
    }
}
