using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    
    private Marking mark;
    
    private float moveSpeed = 5f;
    private float jumpForce = 1.5f;
    private float gravity = -9.8f;
    public float lookSpeed = 1f;
    private float lookVert;
    private float lookHorz;
    
    
    private bool isCrouched = false;
    private bool isProned = false;
    private bool isSprinting = false;
    
    Vector2 moveAmount;
    Vector2 lookAmount;
    Vector2 velocity;

    
    public CharacterController controller;

    public void OnMove(InputAction.CallbackContext context)
    {
        // reads the move amount, rest of code is in Update
        moveAmount = context.ReadValue<Vector2>();
        //Debug.Log($"Move input: {moveAmount}");

    }    
    public void OnLook(InputAction.CallbackContext context)
    {
        //reads the look value and sets the mouse to be invisible and at the centre of the screen
        lookAmount = context.ReadValue<Vector2>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        //where the player will be able to shoot -- MarkEnemy is here for testing
        if (context.performed)
        {
            mark.MarkEnemy();
            Debug.Log("This is when the gun will shoot");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        //where most interactions should appear
        if (context.performed)
        {
            
            Debug.Log("interacted With The Object, if there is one");
        }
    }

    public void OnCrouch(InputAction.CallbackContext context)
    {
        //set player speed to be slower, and prevent jumping
        if (context.performed && controller.isGrounded && isSprinting == false)
        {
            //Debug.Log($"Crouching with {context.action}");
            isCrouched = true;
            moveSpeed = 2.5f;
            jumpForce = 0f;
        }

        if (context.canceled && isSprinting == false)
        {
            isCrouched = false;
            moveSpeed = 5f;
            jumpForce = 1.5f;
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        //if player is on the ground, jump -- rest of code is in Update
        //Debug.Log($"Jumping: {context.performed} - Is Grounded: {controller.isGrounded}");
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //player moves faster if they're not crouched or proned
        if (context.performed && controller.isGrounded && isCrouched == false && isProned == false)
        {
            isSprinting = true;
            moveSpeed = 10f;
        }

        if (context.canceled && isCrouched == false && isProned == false)
        {
            isSprinting = false;
            moveSpeed = 5f;
        }
    }

    public void OnProne(InputAction.CallbackContext context)
    {
        //make player prone if they're not sprinting and they're on the ground
        if (context.performed && controller.isGrounded && isSprinting == false)
        {
            isProned = true;
            moveSpeed = 1f;
            jumpForce = 0f;
        }

        if (context.canceled&& isSprinting == false)
        {
            isProned = false;
            moveSpeed = 5f;
            jumpForce = 1.5f;
        }
    }

    public void Start()
    {
        controller = GetComponent<CharacterController>();
        mark = GetComponent<Marking>();
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        //Move Code -- still broken

        Vector3 horizontal = (transform.right * moveAmount.x + transform.forward * moveAmount.y)* moveSpeed;
        controller.Move((horizontal + Vector3.up * moveAmount.y) * Time.deltaTime);
        
                
        lookVert += lookAmount.x * lookSpeed;
        lookHorz -= lookAmount.y * lookSpeed;
        lookHorz = Mathf.Clamp(lookHorz, -70f, 70f);
        transform.rotation = Quaternion.Euler(lookHorz, lookVert, 0f);

        
        //Jump Code
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}