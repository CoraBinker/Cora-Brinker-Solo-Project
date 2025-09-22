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
    



    public float speed = 5f;
    public float jumpHeight = 300f;
    public float groundDetectLength = 5f;
    float verticalMove;
    float horizontalMove;
    public int health = 10;
    public int maxHealth = 15;


    public void Start()
    {

        cameraoffset = new Vector3(0, .5f, .5f);
        rb = GetComponent<Rigidbody>();
        playerCam = Camera.main;
        lookVector = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        cameraRotation = Vector2.zero;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        Ray ray = new Ray(transform.position, transform.forward);
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

    private void OnTriggerEnter(Collider other)
    {

        // DeathZone

        if (other.tag=="deathZone")
           health = 0;

        // Hea lth and Damage system

        if ((other.tag == "Health") && (health < maxHealth))
            health += 1;
        Destroy(other.gameObject);

   
    
    }

   

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "hazard")
            health -= 1;

        if (collision.gameObject.tag == "Basic Enemy")
            health -= 2;
    }

   
   
}
