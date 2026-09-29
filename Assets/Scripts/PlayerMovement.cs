using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")] 
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float groundDrag = 5f;
    public float baseStamina = 10f;
    public float currentStamina;
    public float drainRate = 1f;
    public float regenRate = 1f;

    public bool isSprinting;
    public bool isResting;
    public Transform orientation;
    public Rigidbody rb;
    public InputAction moveAction;
    public InputAction sprintAction;
    Vector3 moveDirection;
    
    Vector2 inputVector;

    private void Start()
    {
        rb.freezeRotation = true;
        rb.linearDamping = groundDrag;
        currentStamina = baseStamina;
    }

    private void OnEnable()
    {
        moveAction.Enable();
        sprintAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        sprintAction.Disable();
    }

    private void Update()
    {
        MyInput();
        if (sprintAction.IsPressed() && currentStamina > 0 && inputVector != Vector2.zero && !isResting)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        if (isSprinting)
        { 
            currentStamina -= Time.deltaTime * drainRate;
        }
        else
        {
            currentStamina += Time.deltaTime * regenRate;
            
        }
        currentStamina = Mathf.Clamp(currentStamina, 0, baseStamina);
        
        if (currentStamina == 0)
        {
            isResting = true;
        }
        
        if (currentStamina == baseStamina)
        {
            isResting = false;
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        inputVector = moveAction.ReadValue<Vector2>();
        
    }

    private void MovePlayer()
    {
        moveDirection = orientation.forward * inputVector.y + orientation.right * inputVector.x;

        if (isSprinting)
        {
            rb.AddForce(moveDirection * runSpeed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(moveDirection * walkSpeed * 10f, ForceMode.Force);
        }
    }
}
