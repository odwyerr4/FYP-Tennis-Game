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
    public int scoreDiff;
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
        if(userScore > AIScore)
        {
            scoreDiff = userScore - AIScore;
        }else if(AIScore > userScore)
        {
            scoreDiff = AIScore - userScore;
        }else
        {
            scoreDiff = 0;
        }

        if(userScore > 40 && scoreDiff > 15)                             //if user wins
        {
            userScore = 0;
            AIScore = 0;
            userGameScore++;                        //increase game wins    
        }else if(AIScore > 40 && scoreDiff > 15)                      //if AI wins
        {
            userScore = 0;
            AIScore = 0;
            AIGameScore++;                          //increase game wins
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
                if (userScore > 29)
                {
                    userScore = userScore + 10;
                }else
                {
                    userScore = userScore + 15;
                }
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if(lastHitBy == "AI")
            {
                if (AIScore > 29)
                {
                    AIScore = AIScore + 10;
                }else
                {
                    AIScore = AIScore + 15;
                }
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
                if (userScore > 29)
                {
                    userScore = userScore + 10;
                }else
                {
                    userScore = userScore + 15;
                }
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "user") && (bouncedOnAISide == false))
            {
                if (AIScore > 29)
                {
                    AIScore = AIScore + 10;
                }else
                {
                    AIScore = AIScore + 15;
                }
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "AI") && (bouncedOnUserSide == true))
            {
                if (AIScore > 29)
                {
                    AIScore = AIScore + 10;
                }else
                {
                    AIScore = AIScore + 15;
                }
                bouncedOnAISide = false;
                bouncedOnUserSide = false;
            }
            else if((lastHitBy == "AI") && (bouncedOnUserSide == false))
            {
                if (userScore > 29)
                {
                    userScore = userScore + 10;
                }else
                {
                    userScore = userScore + 15;
                }
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
