using UnityEngine;
using System.Collections;
using UnityEngine.Assemblies;
using UnityEngine.InputSystem;
public class PlayerController : MonoBehaviour
{
    [Header("Obj Refrences")]
    public Transform rotVector;
    public Transform handVector;
    [Tooltip("Refrence to the AimPos Script on the main camera.")]
    public AimPos aim;
    

    [Header("Player Settings")]
    public float moveSpeed = 5f;
    public float jumpForce = 1.5f;
    public float gravity = -9.8f;
    public float lookSpeed = 1f;
    [Tooltip("The max angle between camera and player model.")]
    public float maxRelativeRotAngle;
    


    [Header("Player Flags")]
    public bool isCrouched = false;
    public bool isProned = false;
    public bool isSprinting = false;
    public bool isInBush = false;
    public int detectionRate = 0;

    [Header("Inventory")]
    public InventoryController invCon;
    public ItemInstance currentWeaponInstance;
    GameObject currentHeldWeapon = null;

    // Private Variables

    // Rotation and Movement
    private float lookH;
    private float lookV;
    Quaternion targetRotation;
    Vector2 moveAmount;
    Vector2 lookAmount;
    Vector2 velocity;

    // Character Controller
    CharacterController controller;

    public void Start()
    {
        controller = GetComponent<CharacterController>();
    }


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
        if(currentHeldWeapon != null)
        {
            currentHeldWeapon.GetComponent<WeaponScript>().PrimaryAction();
        }
        

    }

    public void OnReload(InputValue value)
    {
        if (currentWeaponInstance.isGunType && currentWeaponInstance != null)
        {
            currentHeldWeapon.GetComponent<GunScript>().ReloadGun();
        }
    }

    public void OnMark(InputValue value)
    {
        //mark.MarkEnemy();
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

    public void OnInteract()
    {

        if (aim.lookRef != null)
        {
            aim.lookRef.Activate();
        }
        
    }


    public void OnNext()
    {
        ItemInstance equipNew = invCon.NextEquip();
        if( equipNew == null )
        {
            Debug.Log("Null Weapon");
        }
        else if( equipNew == currentWeaponInstance)
        {
            Debug.Log("Same Weapon");
        }
        else
        {
            Debug.Log("NEW Weapon");
            Debug.Log(equipNew.itemData.uiName);
            newWeapon(equipNew);
        }
    }

    public void OnPrevious()
    {
        ItemInstance equipNew = invCon.PrevEquip();
        if (equipNew == null)
        {
            Debug.Log("Null Weapon");
        }
        else if (equipNew == currentWeaponInstance)
        {
            Debug.Log("Same Weapon");
        }
        else
        {
            Debug.Log("NEW Weapon");
            Debug.Log(equipNew.itemData.uiName);
            newWeapon(equipNew);
        }
    }

    void newWeapon(ItemInstance equipNew)
    {
        Destroy(currentHeldWeapon);
        currentHeldWeapon = null;
        currentHeldWeapon = Instantiate<GameObject>(equipNew.itemData.prefab, handVector);
        currentHeldWeapon.GetComponent<ItemScript>().Init(equipNew);
        currentWeaponInstance = equipNew;
    }

    
    public void Update()
    {
        // Camera
        lookH += lookAmount.x * lookSpeed;
        lookV -= lookAmount.y * lookSpeed;
        lookV = Mathf.Clamp(lookV, -70f, 70f);
        rotVector.rotation = Quaternion.Euler(lookV, lookH, 0f);

        // Hand Rotation
        handVector.LookAt(aim.aimPoint);



        // Movement
        Vector3 horizontal = (transform.right * moveAmount.x + transform.forward * moveAmount.y)* moveSpeed;


        float relativeAngle = Mathf.DeltaAngle(transform.rotation.eulerAngles.y, rotVector.rotation.eulerAngles.y);

        if (horizontal != Vector3.zero) // if moving update player rotation
        {
            targetRotation = Quaternion.Euler(0f, lookH, 0f); // TODO: Smooth rotation for player model
        }
        else if (relativeAngle > maxRelativeRotAngle)
        {
            targetRotation = Quaternion.Euler(0, lookH - maxRelativeRotAngle, 0);
        }
        else if (relativeAngle < maxRelativeRotAngle * -1)
        {
            targetRotation = Quaternion.Euler(0, lookH + maxRelativeRotAngle, 0);
        }

        transform.rotation = targetRotation;


        controller.Move((horizontal + Vector3.up * moveAmount.y) * Time.deltaTime);
        
                
        // Jump
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }


    


}