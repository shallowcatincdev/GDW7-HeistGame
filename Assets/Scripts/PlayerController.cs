using UnityEngine;
using UnityEngine.Assemblies;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    // TEMP

    public WeaponScript[] weapons;
    public int currentWeapon;

    GameObject currentHeldWeapon;

    // TEMP


    public Rigidbody rb;
    public Transform rotVector;
    public Transform handVector;
    private Marking mark;

    public AimPos aim;
    public float handRotSpeed = 5f;
    
    private float moveSpeed = 5f;
    private float jumpForce = 1.5f;
    private float gravity = -9.8f;
    public float lookSpeed = 1f;
    private float lookVert;
    private float lookHorz;
    
    
    public bool isCrouched = false;
    public bool isProned = false;
    public bool isSprinting = false;
    
    Vector2 moveAmount;
    Vector2 lookAmount;
    Vector2 velocity;

    public int detectionRate = 0;
    public bool isInBush = false;
    
    public CharacterController controller;

    public void OnMove(InputValue value)
    {
        moveAmount = value.Get<Vector2>();
    }

    public void OnLook(InputValue value)
    {
        lookAmount = value.Get<Vector2>();
        Cursor.lockState = CursorLockMode.Locked; // TODO cursor stuff should be moved
        Cursor.visible = false;
    }

    public void OnAttack(InputValue value)
    {
        // TODO in future this will interact with the inventory to find out the currently equipped item but for now will be hard coded.
        weapons[currentWeapon].PrimaryAction();
    }

    public void OnReload(InputValue value)
    {
        if (weapons[currentWeapon].itemInstance.isGunType)
        {
            weapons[currentWeapon].GetComponent<GunScript>().ReloadGun();
        }
    }

    public void OnMark(InputValue value)
    {
        //mark.MarkEnemy();
    }

    public void OnInteract(InputValue value)
    {
        // TODO
    }

    public void OnCrouch(InputValue value)
    {
        //set player speed to be slower, and prevent jumping
        if (value.Get<float>() > 0.5f && controller.isGrounded && isSprinting == false)
        {
            isCrouched = true;
            moveSpeed = 2.5f;
            jumpForce = 0f;
        }

        if (value.Get<float>() <= 0.5f && isSprinting == false)
        {
            isCrouched = false;
            moveSpeed = 5f;
            jumpForce = 1.5f;
        }
    }
    
    public void OnJump(InputValue value)
    {
        if (controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
        }
    }

    public void OnSprint(InputValue value)
    {
        if (value.Get<float>() > 0.5f && controller.isGrounded && isCrouched == false && isProned == false)
        {
            isSprinting = true;
            moveSpeed = 10f;
        }

        if (value.Get<float>() <= 0.5f && isCrouched == false && isProned == false)
        {
            isSprinting = false;
            moveSpeed = 5f;
        }
    }


    public void OnNext()
    {
        //TODO
    }

    public void OnPrevious()
    {
        // TODO
    }

    void newWeapon()
    {
        //TODO
    }

    public void Start()
    {
        controller = GetComponent<CharacterController>();
        mark = GetComponent<Marking>();
        rb = GetComponent<Rigidbody>();
    }
    public void Update()
    {
        // Camera
        lookVert += lookAmount.x * lookSpeed;
        lookHorz -= lookAmount.y * lookSpeed;
        lookHorz = Mathf.Clamp(lookHorz, -70f, 70f);
        rotVector.rotation = Quaternion.Euler(lookHorz, lookVert, 0f);

        // Hand Rotation
        handVector.LookAt(aim.aimPoint);



        // Movement
        Vector3 horizontal = (transform.right * moveAmount.x + transform.forward * moveAmount.y)* moveSpeed;


        if (horizontal != Vector3.zero) // if moving update player rotation
        {
            transform.rotation = Quaternion.Euler(0f, lookVert, 0f); // TODO: Smooth rotation for player model
        }


        controller.Move((horizontal + Vector3.up * moveAmount.y) * Time.deltaTime);
        
                
        // Jump
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

}