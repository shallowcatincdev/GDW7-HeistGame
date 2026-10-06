using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    public Rigidbody rb;
    
    private float speed = 5f;
    private float jumpForce = 1.5f;
    private float gravity = -9.8f;
    public float lookSpeed = 1f;

    private bool isCrouched = false;
    private bool isProned = false;
    private bool isSprinting = false;
    
    Vector2 moveAmount;
    Vector2 lookAmount;
    Vector2 velocity;

    
    public CharacterController controller;

    public void OnMove(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        moveAmount = context.ReadValue<Vector2>();
        Debug.Log($"Move input: {moveAmount}");
    }    
    public void OnLook(InputAction.CallbackContext context)
    {
        // read the value for the "move" action each event call
        lookAmount = context.ReadValue<Vector2>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        //figure out how to get button callbacks
        if (context.performed)
        {
            Debug.Log("This is when the gun will shoot");
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
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
            speed = 2.5f;
            jumpForce = 0f;
        }

        if (context.canceled && isSprinting == false)
        {
            isCrouched = false;
            speed = 5f;
            jumpForce = 1.5f;
        }
    }
    
    public void OnJump(InputAction.CallbackContext context)
    {
        //Debug.Log($"Jumping: {context.performed} - Is Grounded: {controller.isGrounded}");
        // your jump code goes here.
        if (context.performed && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        //player moves faster
        if (context.performed && controller.isGrounded && isCrouched == false && isProned == false)
        {
            isSprinting = true;
            speed = 10f;
        }

        if (context.canceled && isCrouched == false && isProned == false)
        {
            isSprinting = false;
            speed = 5f;
        }
    }

    public void OnProne(InputAction.CallbackContext context)
    {
        if (context.performed && controller.isGrounded && isSprinting == false)
        {
            isProned = true;
            speed = 1f;
            jumpForce = 0f;
        }

        if (context.canceled&& isSprinting == false)
        {
            isProned = false;
            speed = 5f;
            jumpForce = 1.5f;
        }
    }
    public void Update()
    {
        Vector3 move = new Vector3(moveAmount.x, 0, moveAmount.y);
        controller.Move(move * speed * Time.deltaTime);
        
        //add look logic
        Vector3 look = new Vector3(lookAmount.x, 0, lookAmount.y);
        //rb.rotation = Quaternion.LookRotation(look);
        
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}