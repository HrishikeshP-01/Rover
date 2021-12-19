using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapTile : MonoBehaviour
{
    private float startPosX;
    private float startPosY;
    private bool isBeingHeld = false;
    private bool isBeingRotated = false;
    public float zPosOfSprite = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (isBeingHeld)
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            this.gameObject.transform.localPosition = new Vector3(mousePos.x - startPosX, mousePos.y - startPosY, zPosOfSprite);
        }        
    }

    private void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos;
            mousePos = Input.mousePosition;
            mousePos = Camera.main.ScreenToWorldPoint(mousePos);

            startPosX = mousePos.x - this.transform.localPosition.x;
            startPosY = mousePos.y - this.transform.localPosition.y;

            isBeingHeld = true;
        }
    }
    private void OnMouseUp()
    {
        isBeingHeld = false;
    }

    /*
     The RMB doesn't get detected in the onMouseDown() fn for some reason and using an else case in it doesn't work as it seems that the function can only be called
    using the LMB.
    So instead I've used the OnMouseOver() function that can detect the RMB click when the mouse is over the object but then as the OnMouseOver is called multiple times
    i.e. all the frames that the cursor is over the object, a single click could trigger the same functionality multiple times. To avoid this I've added a flag which
    lets you call the functionality just once with each click as the delay ensures that multiple calls are not possible.
    To use the delay we use the: yeild return new WaitForSeconds(int seconds)
    this only works in IEnumerators so change Rot is an IEnumerator, the IEnumerators can only be called using the StartCoroutine() fn*/

    private void OnMouseOver()
    {
        if (Input.GetMouseButton(1))
        {
            if (!isBeingRotated)
            {
                StartCoroutine(changeRot());
            }
        }
    }

    IEnumerator changeRot()
    {
        isBeingRotated = true;
        Debug.Log("RMB");
        this.transform.Rotate(0f, 0f, 90.0f);
        yield return new WaitForSeconds(1);
        isBeingRotated = false;
    }
}
