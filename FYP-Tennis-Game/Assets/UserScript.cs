using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScript : MonoBehaviour
{
    public Transform aimTarget;
    public Transform ball;
    public Transform user;
    public Transform racquetHead;
    float moveSpeed = 3f;
    ShotTypes shotTypes;
    Shot currentShot;
    
    // Start is called before the first frame update
    void Start()
    {
        shotTypes = GetComponent<ShotTypes>();  //initialise shotTypes
        currentShot = shotTypes.serve;          //set start shot to serve
    }

    // Update is called once per frame
    void Update()
    {
        float sideToSide = Input.GetAxisRaw("Horizontal");   //for horizontal movements
        float upAndDown = Input.GetAxisRaw("Vertical");    //for vertical movements

        if((upAndDown != 0) || (sideToSide != 0))   //if we are pressing to move
        {
            user.Translate(new Vector3(upAndDown, 0, sideToSide) * moveSpeed * Time.deltaTime);    //move user
        }

        Vector3 ballDirection = ball.position - racquetHead.position;     //gets position of ball relative to user
        
        Debug.DrawRay(racquetHead.position, ballDirection);               //draws ray from ball to user

        if(Input.GetKeyDown(KeyCode.C))                             //if C is pressed
        {
            currentShot = shotTypes.serve;                          //set current shot to serve
            GetComponent<BoxCollider>().enabled = false;            //turn off collider to throw the ball up


        }
        else if(Input.GetKeyUp(KeyCode.C))                          //when C is released
        {
            currentShot = shotTypes.serve;
            ball.transform.position = user.position + new Vector3(0, 1.5f, 0); //throw the ball up
            GetComponent<BoxCollider>().enabled = true;                        //turn box collider back on
        }

        Vector3 ballToUser = ball.position - user.position;
        Debug.Log(ballToUser.z);

        if(ballDirection.y > 1)
        {
            currentShot = shotTypes.serve;          //if ball is above head, serve shot
        }
        else if(ballToUser.z < 0)     
        {
            currentShot = shotTypes.backSpin;       //if ball is to the left of user, flat shot
        }
        else if(ballToUser.z > 0)
        {
            currentShot = shotTypes.topSpin;        //if ball is on the right of user, top spin
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))    //if other collides with ball
        {
            Vector3 dir = aimTarget.position - user.position;  //direction of the ball is the target minus where the ball is hit from
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.shotPower + new Vector3(0, currentShot.upForce, 0);    //the velocity of the ball
            ball.GetComponent<BallScript>().lastHitBy = "user";   //set ball was last hit by to: user
        }
    }

    void flatShotTorque()
    {

    }
}