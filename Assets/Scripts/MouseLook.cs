using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float xRotation = 0f;

    // Control the mouse speed
    public float mouseSensitivity = 100.0f;
    public Transform playerBody;

    // Start is called before the first frame update
    void Start()
    {
        // Hide the cursor and lock it to screen centre
        Cursor.lockState = CursorLockMode.Locked;        
    }

    // Update is called once per frame
    void Update()
    {
        // Unity is working on a new input system so make necessary changes in those cases
        /* Differences in frame rate should not affect the speed of rotation
         so we multiply it with delta time which ensures the speed is same
        irrespective of the speed of the Update() fn */
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        playerBody.Rotate(Vector3.up * mouseX);

        /*Instead of just using the Rotate() method we use xRotation,
         perform the decrement and then set it using localRotation
        to be able to apply the clamp. Otherwise we won't know when the 
        limit has been reached.*/
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
}
