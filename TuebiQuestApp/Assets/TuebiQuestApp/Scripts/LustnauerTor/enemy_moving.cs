using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_moving : MonoBehaviour {

    public float speed;
    public float LeftGrenze;       
    public float RightGrenze;
    public bool walkInThisDirection = true;
    Vector2 walkAmount;
    float originalX;


    void Start()
    {
        this.originalX = this.transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (walkInThisDirection)
        {
            walkAmount.x = 1.0f * speed * Time.deltaTime;
            if (transform.position.x >= RightGrenze)
            {
                walkInThisDirection = false;
                GetComponent<Animator>().SetBool("FacesRight", true);

            }
        }
        else
        {
            walkAmount.x = -1.0f * speed * Time.deltaTime;
            if (transform.position.x <= LeftGrenze)
            {
                walkInThisDirection = true;
                GetComponent<Animator>().SetBool("FacesRight", false);
            }

        }

        transform.Translate(walkAmount);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        healthyhealth.health -= 1;
        points.counter -= 10;
    }
}
