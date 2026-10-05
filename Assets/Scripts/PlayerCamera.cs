using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [Header("References")] 
    public Transform playerBody;
    public Transform head;

    [Header("Camera Settings")] 
    public float sensitivity = 15f;
    public float maxLookAngle = 89f;
    
    private float cameraPitch = 0f;
    private Vector2 lookInput;
    private void Start()
    {
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
        
        cameraPitch -= mouseY;
        cameraPitch = Mathf.Clamp(cameraPitch, -maxLookAngle, maxLookAngle);
        head.localRotation = Quaternion.Euler(cameraPitch, 0f, 0f);
        
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
