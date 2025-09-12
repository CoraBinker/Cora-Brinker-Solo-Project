using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour

{
    Vector2 cameraRotation;
    Vector3 cameraoffset;
    InputAction lookVector;
    Transform playerCam;
    Rigidbody rb;
   


    public float speed = 5f;
    public float jumpHeight = 10f;
    float verticalMove;
    float horizontalMove;
    public float Xsensitivity=.30f;
    public float ysensitivity = .30f;
    public float cameraRotationLimit = 90.0f;
    public int health=10;
    public int maxHealth = 15;
        

    public void Start()
    {
      
        cameraoffset = new Vector3(0, .5f, .5f);
        rb=GetComponent<Rigidbody>();
        playerCam = transform.GetChild(0);
        lookVector = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        cameraRotation = Vector2.zero;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
      
    }

    
    void Update()
    {

        // Health and Respawning
        if (health <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        //CAmera Rotation system

        //playerCam.transform.position = transform.position + cameraoffset;
        cameraRotation.x += lookVector.ReadValue<Vector2>().x * Xsensitivity;
        cameraRotation.y += lookVector.ReadValue<Vector2>().y * ysensitivity;

        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -cameraRotationLimit, cameraRotationLimit);

        transform.localRotation = Quaternion.AngleAxis(cameraRotation.x, Vector3.up);
        playerCam.rotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);

        //Movement System

        Vector3 temp = rb.linearVelocity;
        temp.x = verticalMove * speed;
        temp.z = horizontalMove * speed;
        rb.linearVelocity = (temp.x * transform.forward) + (temp.y * transform.up) + (temp.z * transform.right);


       
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 inputAxis= context.ReadValue<Vector2>();

        verticalMove = inputAxis.y;
        horizontalMove = inputAxis.x;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="deathZone")
           health = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        
    }
    
   
}
