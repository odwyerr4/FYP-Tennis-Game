using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallScript : MonoBehaviour
{
    Vector3 initialPosition;
    Vector3 initialUserPosition;
    public string lastHitBy;
    public int userScore;
    public int AIScore;
    public Transform user;
    public bool bouncedOnAISide;
    public bool bouncedOnUserSide;
    [SerializeField] Text userScoreText;
    [SerializeField] Text AIScoreText;

    // Start is called before the first frame update
    void Start()
    {
        initialUserPosition = user.position;    //set initial user postion to the start position of the user
        userScore = 0;
        AIScore = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AISide"))              //if the ball lands in bounds on the AI side....
        {
            bouncedOnAISide = true;
            bouncedOnUserSide = false;
        }

        if(other.CompareTag("UserSide"))            //if the ball lands in bounds on the user side....
        {
            bouncedOnAISide = false;
            bouncedOnUserSide = true;
        }

        if(other.CompareTag("Net") && (other.CompareTag("Out") != true) && (other.CompareTag("Wall") != true))     //if the ball hits the net
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;  //set the velocity of the ball to zero
            user.position = initialUserPosition;                //if the ball hits a wall set the user position back in initail position for serve

            if(lastHitBy == "user")
            {
                AIScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if(lastHitBy == "AI")
            {
                userScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }

            updateScores();
        }

        if((other.CompareTag("Out")) && (other.CompareTag("Wall") != true))      //if the ball lands out of bounds before hitting the wall...
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;  //set the velocity of the ball to zero
            user.position = initialUserPosition;                //if the ball hits a wall set the user position back in initail position for serve
            
            if((lastHitBy == "user") && (bouncedOnAISide == true))
            {
                userScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "user") && (bouncedOnAISide == false))
            {
                AIScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "AI") && (bouncedOnUserSide == true))
            {
                AIScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "AI") && (bouncedOnUserSide == false))
            {
                userScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }

            updateScores();
        }

        if((other.CompareTag("Wall")) && (other.CompareTag("Out") != true))      //if the ball hits a wall without landing out...
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;  //set the velocity of the ball to zero
            user.position = initialUserPosition;                //if the ball hits a wall set the user position back in initail position for serve
            
            if((lastHitBy == "user") && (bouncedOnAISide == true))
            {
                userScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "user") && (bouncedOnAISide == false))
            {
                AIScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "AI") && (bouncedOnUserSide == true))
            {
                AIScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "AI") && (bouncedOnUserSide == false))
            {
                userScore++;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }

            updateScores();
        }
    }

    void updateScores()
    {
        userScoreText.text = "User: " + userScore;      //show user score
        AIScoreText.text = "AI: " + AIScore;            //show AI score
    }
}
