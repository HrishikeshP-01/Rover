using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float x;
    private float z;
    private Vector3 move;
    private Vector3 velocity;
    bool isGrounded;

    public CharacterController controller;
    public float speed = 12f;
    public float gravity = -9.81f;
    // Reference to ground check object
    public Transform groundCheck;
    // Radius of sphere used for check
    public float groundDistance = 0.4f;
    /* Control what objects the sphere should check for 
     * (eg: Water meshes wont matter so we set the layer such that 
     * water meshes aren't incluedd*/
    public LayerMask groundMask;
    public float jumpHeight = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x = Input.GetAxis("Horizontal");
        z = Input.GetAxis("Vertical");

        move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        /*If we had just created a new vector instead like:
         move = new Vector3(x,0,z)
        then the position would be global but we want it to be relative
        so we get the player's right direction vector & forward direction vector
        and mulitply it with the axis value*/

        // Create a tiny physics sphere to check if the ground has been hit.
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        // If it's on the ground reset the downward velocity
        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2.0f;
            /* Sometimes changing it to 0 wont guarantee that the palyer
             is on the ground because the ground chaeck depends on the 
            input radius and we can't always be precise so just reset the 
            downward velocity to a small value but enough to force the player
            to the ground*/ 

        }

        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            // the equn for jump vel. (physics bodies) is sqrt(h x -2 x g)
            velocity.y = Mathf.Sqrt(jumpHeight * -2.0f * gravity);
            Debug.Log("Jump");
        }

        /*To calculate the effect of graviy we first calculate the velocity 
         due to gravity and the subsequent distance travelled by the object.
        This must be done only if the body is not on ground.
        Otherwise the values keeps increasing and the jump times will gradually decrease*/
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
