using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour

{
    Vector2 cameraRotation;
    Vector3 cameraoffset;
    InputAction lookVector;
    Camera playerCam;
    Rigidbody rb;
    Ray ray;
    Ray interactRay;
    RaycastHit interactHit;
    GameObject pickupObject;
    GameManager gm;



    public float speed = 5f;
    public float jumpHeight = 300f;
    public float groundDetectLength = 5f;
    float verticalMove;
    float horizontalMove;
    public int health = 10;
    public int maxHealth = 15;

    public PlayerInput input;
    public Transform weaponSlot;
    public Weapon currentWeapon;
    public float interactionDistance = 1f;

    public bool attacking = false;
    

    public void Start()
    {
        gm = GameObject.FindGameObjectWithTag("game manager").GetComponent<GameManager>();
        cameraoffset = new Vector3(0, .5f, .5f);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        lookVector = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        cameraRotation = Vector2.zero;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Ray ray = new Ray(transform.position, transform.forward);
        interactRay = new Ray(transform.position, transform.forward);
        weaponSlot = transform.GetChild(0);
    }


    void Update()
    {

        // Health and Respawning
        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //CAmera Rotation system

        Quaternion playerRoatation = playerCam.transform.rotation;
        playerRoatation.x = 0;
        playerRoatation.z = 0;
        transform.rotation = playerRoatation;


        //Movement System

        Vector3 temp = rb.linearVelocity;
        temp.x = verticalMove * speed;
        temp.z = horizontalMove * speed;

        ray.origin = transform.position;
        ray.direction = -transform.up;
        rb.linearVelocity = (temp.x * transform.forward) + (temp.y * transform.up) + (temp.z * transform.right);

        interactRay.origin = playerCam.transform.position;
        interactRay.direction = playerCam.transform.forward;

        if (currentWeapon)
            if (currentWeapon.holdToAttack && attacking)
                currentWeapon.fire();

        if (Physics.Raycast(interactRay, out interactHit, interactionDistance))
        {
            if (interactHit.collider.gameObject.tag == "weapon")
            {
                pickupObject = interactHit.collider.gameObject;
            }
        }
        else
            pickupObject = null;

        if (health == 0)
        {
            SceneManager.LoadScene(2);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }


	}
    
    

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis = context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;
    }

    public void Jump()
    {

    if (Physics.Raycast(ray, groundDetectLength))
        rb.AddForce(transform.up* jumpHeight, ForceMode.Impulse);
       
    }

   

public void Attack(InputAction.CallbackContext context)
    {
        if (currentWeapon)
        {
            if (currentWeapon.holdToAttack)
            {
                if (context.ReadValueAsButton())
                    attacking = true;
                else
                    attacking = false;
            }

            else
                currentWeapon.fire();
        }
    }

    public void Interact()
    {
        if (pickupObject)
        {
            if (pickupObject.tag == "weapon")
            {
                pickupObject.GetComponent<Weapon>().equip(this);

                pickupObject = null;
            }
            else
                Reload();
        }
    }


    public void Reload()
    {
        if (currentWeapon)
           if (!currentWeapon.reloading)
                currentWeapon.reload();
    }

    public void DropWeapon()
    {
         if (currentWeapon)
        {
            currentWeapon.GetComponent<Weapon>().unequip();
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        // DeathZone

        if (other.tag=="deathZone")
           health = 0;

        // Hea lth and Damage system

        if ((other.tag == "Health") && (health < maxHealth))
        {
            health += 1;
            Destroy(other.gameObject);
        }

        if ((other.tag == "Basic enemy")  && (health > 0))
            health -= 2;
    }



    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hazard")
            health -= 1;

    }
    
}
