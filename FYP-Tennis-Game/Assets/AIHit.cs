using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AIHit : MonoBehaviour
{
    public Transform racquetHead;
    public Transform hitTarget;
    public Transform user;
    public Transform ball;
    public float shotSpread;
    ShotTypes shotTypes;
    Shot currentShot;
    public GameObject easyToggle;
    public GameObject mediumToggle;
    public GameObject hardToggle;
    // Start is called before the first frame update
    void Start()
    {
        shotTypes = GetComponent<ShotTypes>();  //initialise shotTypes
        currentShot = shotTypes.forehand;       //set current shot to top spin
    }

    // Update is called once per frame
    void Update()
    {
        if(easyToggle.GetComponent<Toggle>().isOn == true)                  //if easy difficulty selected
        {
            shotSpread = 1;
        }
        else if(mediumToggle.GetComponent<Toggle>().isOn == true)           //if medium difficulty selected
        {
            shotSpread = 2;
        }
        else if(hardToggle.GetComponent<Toggle>().isOn == true)             //if hard difficulty selected
        {
            shotSpread = 3;
        }

         if(ball.position.y > 1)
        {
            currentShot = shotTypes.serve;          //if ball is above head, serve shot
        }
        else if(ball.position.x < 0)     
        {
            currentShot = shotTypes.backhand;       //if ball is to the left of AI, back spin
        }
        else
        {
            currentShot = shotTypes.forehand;        //if ball is on the right of AI, top spin
        }
    }

    
   private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))    //if other collides with ball
        {
            Vector3 dir = hitTarget.position - racquetHead.position;  //direction of the ball is the target minus where the ball is hit from
            if(user.position.x < 0)     //if user is on the left
            {
                dir = dir + new Vector3(shotSpread, 0, 0);     //hit right
            }else
            {
                dir = dir - new Vector3(shotSpread, 0, 0);      //else hit left
            }
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.shotPower + new Vector3(0, currentShot.upForce, 0);    //the velocity of the ball
            ball.GetComponent<BallScript>().lastHitBy = "AI";   //set ball was last hit by to AI
        }
    }
}
