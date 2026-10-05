using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")] 
    public Transform playerBody;
    public Transform head;
    public Inspection inspectionScript;

    [Header("Camera Settings")] 
    public float sensitivity = 15f;
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
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        float mouseX = lookInput.x * sensitivity * 0.01f;
        float mouseY = lookInput.y * sensitivity * 0.01f;

        if (!inspectionScript.isInspecting)
        {
            cameraPitch -= mouseY;
            cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
            head.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
            head.localPosition = startPosition;
        }

        else
        {
            cameraPitch = 0f;
            head.localRotation = Quaternion.Euler(0f, 0f, 0f);
            playerBody.rotation = inspectionScript.currentAnchor.rotation;
            playerBody.position = inspectionScript.currentAnchor.position;
            
            Vector3 newPosition = head.localPosition;
            newPosition.x += mouseX * panSpeed;
            newPosition.y += mouseY * panSpeed; 
            newPosition.x = Mathf.Clamp(newPosition.x, startPosition.x - panLimitX, startPosition.x + panLimitX);
            newPosition.y = Mathf.Clamp(newPosition.y, startPosition.y - panLimitY, startPosition.y + panLimitY);
            head.localPosition = newPosition;
        }
        
       
    }
}
