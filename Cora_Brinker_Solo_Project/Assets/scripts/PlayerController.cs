using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour

{
    Rigidbody rb;


    public float speed = 5f;
    public float jumpHeight = 10f;
    float verticalMove;
    float horizontalMove;

   public void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
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
