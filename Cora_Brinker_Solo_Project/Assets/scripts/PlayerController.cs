using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    Vector2 cameraRotation;
    InputAction lookVector;
    Camera playerCam;
    Rigidbody rb;


    public float speed = 5f;
    public float jumpHeight = 10f;
    float verticalMove;
    float horizontalMove;
    public float Xsensitivity=.30f;
    public float ysensitivity = .30f;
    public float cameraRotationLimit = 90.0f;

    public void Start()
    {
        rb=GetComponent<Rigidbody>();
        playerCam = Camera.main;
        lookVector = GetComponent<PlayerInput>().currentActionMap.FindAction("Look");
        cameraRotation = Vector2.zero;

    }

    // Update is called once per frame
    void Update()
    {
        //CAmera Rotation system

        cameraRotation.x += lookVector.ReadValue<Vector2>().x * Xsensitivity;
        cameraRotation.y += lookVector.ReadValue<Vector2>().y * ysensitivity;

        cameraRotation.y = Mathf.Clamp(cameraRotation.y, -cameraRotationLimit, cameraRotationLimit);

        transform.localRotation = Quaternion.AngleAxis(cameraRotation.x, Vector3.up);
        playerCam.transform.rotation = Quaternion.Euler(-cameraRotation.y, cameraRotation.x, 0);

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
}
