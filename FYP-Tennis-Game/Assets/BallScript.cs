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
    public int userGameScore;
    public int AIGameScore;
    public Transform user;
    public bool bouncedOnAISide;
    public bool bouncedOnUserSide;
    public AudioSource ballBounce;
    [SerializeField] Text userScoreText;
    [SerializeField] Text AIScoreText;

    // Start is called before the first frame update
    void Start()
    {
        initialUserPosition = user.position;    //set initial user postion to the start position of the user
        userScore = 0;
        AIScore = 0;
        userGameScore = 0;
        AIGameScore = 0;
    }

    void Update()
    {
        if(userScore > 45)                             //if user wins
        {
            userScore = 0;
            AIScore = 0;
            userGameScore++;
            userScoreText.text = "User: " + userScore + " (" + userGameScore + ")";      //user is winner
            AIScoreText.text = "AI: " + AIScore + " (" + AIGameScore + ")";          
            updateScores();
        }else if(AIScore > 45)                      //if AI wins
        {
            userScore = 0;
            AIScore = 0;
            AIGameScore++;
            userScoreText.text = "User: Loser";     
            AIScoreText.text = "AI: Winner";            //AI is winner
            updateScores();
        }
        updateScores();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("AISide"))              //if the ball lands in bounds on the AI side....
        {
            bouncedOnAISide = true;
            bouncedOnUserSide = false;
            ballBounce.Play();                      //play bounce sound
        }

        if(other.CompareTag("UserSide"))            //if the ball lands in bounds on the user side....
        {
            bouncedOnAISide = false;
            bouncedOnUserSide = true;
            ballBounce.Play();                      //play bounce sound
        }

        if(other.CompareTag("Net") == true && (other.CompareTag("Out") != true) && (other.CompareTag("Wall") != true))     //if the ball hits the net
        {
            ballBounce.Play();                      //play bounce sound
            GetComponent<Rigidbody>().velocity = Vector3.zero;  //set the velocity of the ball to zero
            user.position = initialUserPosition;                //if the ball hits a wall set the user position back in initail position for serve

            if(lastHitBy == "user")
            {
                userScore = userScore + 15;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if(lastHitBy == "AI")
            {
                AIScore = AIScore + 15;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }

            updateScores();
        }

        if((other.CompareTag("Out") == true) || (other.CompareTag("Wall") == true))      //if the ball lands out of bounds before hitting the wall...
        {
            ballBounce.Play();                      //play bounce sound
            GetComponent<Rigidbody>().velocity = Vector3.zero;  //set the velocity of the ball to zero
            user.position = initialUserPosition;                //if the ball hits a wall set the user position back in initail position for serve
            
            if((lastHitBy == "user") && (bouncedOnAISide == true))
            {
                userScore = userScore + 15;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "user") && (bouncedOnAISide == false))
            {
                AIScore = AIScore + 15;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "AI") && (bouncedOnUserSide == true))
            {
                AIScore = AIScore + 15;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "AI") && (bouncedOnUserSide == false))
            {
                userScore = userScore + 15;
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }

            updateScores();
        }

        /*if((other.CompareTag("Wall") == true) && (other.CompareTag("Out") != true))      //if the ball hits a wall without landing out...
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;  //set the velocity of the ball to zero
            user.position = initialUserPosition;                //if the ball hits a wall set the user position back in initail position for serve
            
            if((lastHitBy == "user") && (bouncedOnAISide == true))
            {
                AIScore++;
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
                userScore++;
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
        }*/
    }

    void updateScores()
    {
        userScoreText.text = "User: " + userScore + " (" + userGameScore + ")";      //show user score
        AIScoreText.text = "AI: " + AIScore + " (" + AIGameScore + ")";            //show AI score
    }
}
