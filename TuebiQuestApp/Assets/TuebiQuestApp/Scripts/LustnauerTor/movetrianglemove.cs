using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movetrianglemove : MonoBehaviour
{

    public int JumpCount = 0;
    public int horizontal;
    public float speed = 50;
    public float power = 12;
    public bool RightMaybe;
    private Vector2 touchOrigin = -Vector2.one;

    //Optional: in case if we want change speeds depending on difficulty
    //otherwise to be initialized up

    void Update()
    {
        MakeTheMove();
    }

    void MakeTheMove()
    {
        RightMaybe = true;
        horizontal = (int)(Input.GetAxisRaw("Horizontal"));
        //float moveOnX = Input.GetAxis("Mouse X");
        //Vector3 movement = new Vector3(moveOnX * speed, gameObject.GetComponent<Rigidbody2D>().velocity.y, 0);
        //movement *= Time.deltaTime;
        //FlipFlip(moveOnX);
        //transform.Translate(movement);
        if (Input.touchCount > 0)
        {
            //Store the first touch detected.
            Touch myTouch = Input.touches[0];

            //Check if the phase of that touch equals Began
            if (myTouch.phase == TouchPhase.Began)
            {
                //If so, set touchOrigin to the position of that touch
                touchOrigin = myTouch.position;
            }

            //If the touch phase is not Began, and instead is equal to Ended and the x of touchOrigin is greater or equal to zero:
            else if (myTouch.phase == TouchPhase.Ended && touchOrigin.x >= 0)
            {
                //Set touchEnd to equal the position of this touch
                Vector2 touchEnd = myTouch.position;

                //Calculate the difference between the beginning and end of the touch on the x axis.
                float x = touchEnd.x - touchOrigin.x;

                //Set touchOrigin.x to -1 so that our else if statement will evaluate false and not repeat immediately.
                touchOrigin.x = -1;
            }
        }

        if (horizontal < 0)
        {
            GetComponent<Animator>().SetBool("RightMaybe", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("RightMaybe", false);
        }

        if (Input.GetMouseButtonDown(0) && JumpCount < 1)
        {
            Jump();
            JumpCount += 1;
        }


    }
    void Jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, power), ForceMode2D.Impulse);
    }

    void FlipFlip(float moveOnX)
    {
        if (moveOnX > 0 && !RightMaybe || moveOnX < 0 && RightMaybe)
            {
            RightMaybe = !RightMaybe;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.name == "Block")
        {
            JumpCount = 0;
        }
    }

}