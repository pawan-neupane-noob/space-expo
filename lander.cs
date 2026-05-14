using UnityEngine;
using UnityEngine.InputSystem;

public class lander : MonoBehaviour
{   
    public float forcefactor = 100f;
    public float rotationfactor = 100f;
     private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
    
       if (Keyboard.current.upArrowKey.isPressed)
        {
            rb.AddForce(transform.up *forcefactor*Time.deltaTime);
        }
        if (Keyboard.current.leftArrowKey.isPressed)
        {
            rb.AddTorque(+rotationfactor*Time.deltaTime);
        }
        if (Keyboard.current.rightArrowKey.isPressed)
        {
            rb.AddTorque(-rotationfactor*Time.deltaTime);
        }
    }
}
    