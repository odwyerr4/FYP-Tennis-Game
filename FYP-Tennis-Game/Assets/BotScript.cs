using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotScript : MonoBehaviour
{
    float moveSpeed = 3f;
    float hitForce = 12;
    public float shotSpread;
    public Transform ball;
    public Transform hitTarget;
    public Transform AI;
    public Transform user;
    Vector3 AIPlayerPosition;

    ShotTypes shotTypes;
    Shot currentShot;

    // Start is called before the first frame update
    void Start()
    {
        AIPlayerPosition = AI.position;         //set AI player position to AI start position
        shotTypes = GetComponent<ShotTypes>();  //initialise shotTypes
        currentShot = shotTypes.flatShot;       //set current shot to flat shot
    }

    // Update is called once per frame
    void Update()
    {
        MoveAI();
         if(ball.position.y > 1)
        {
            currentShot = shotTypes.serve;          //if ball is above head, serve shot
        }
        else if(ball.position.z < user.position.z)     
        {
            currentShot = shotTypes.flatShot;       //if ball is to the left of AI, flat shot
        }
        else
        {
            currentShot = shotTypes.topSpin;        //if ball is on the right of AI, top spin
        }
    }

    void MoveAI()
    {
        AIPlayerPosition.z = ball.position.z;       //set AI z position to ball z position
        AI.position = Vector3.MoveTowards(AI.position, AIPlayerPosition, moveSpeed * Time.deltaTime);   //AI moves side to side in the direction of the ball
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))    //if other collides with ball
        {
            Vector3 dir = hitTarget.position - AI.position;  //direction of the ball is the target minus where the ball is hit from
            if(user.position.z < 0)     //if user is on the left
            {
                dir = dir + new Vector3(0, 0, shotSpread);     //hit right
            }else
            {
                dir = dir - new Vector3(0, 0, shotSpread);      //else hit left
            }
            other.GetComponent<Rigidbody>().velocity = dir.normalized * hitForce + new Vector3(0, 5, 0);    //the velocity of the ball
            ball.GetComponent<BallScript>().lastHitBy = "AI";   //set ball was last hit by to AI
        }
    }
}
