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
    float hitForce = 12;
    
    // Start is called before the first frame update
    void Start()
    {
        
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
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))    //if other collides with ball
        {
            Vector3 dir = aimTarget.position - user.position;  //direction of the ball is the target minus where the ball is hit from
            other.GetComponent<Rigidbody>().velocity = dir.normalized * hitForce + new Vector3(0, 5, 0);    //the velocity of the ball
        }
    }
}