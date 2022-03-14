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

    // Start is called before the first frame update
    void Start()
    {
        AIPlayerPosition = AI.position;   //set AI player position to AI start position
    }

    // Update is called once per frame
    void Update()
    {
        MoveAI();
    }

    void MoveAI()
    {
        AIPlayerPosition.z = ball.position.z;
        AI.position = Vector3.MoveTowards(AI.position, AIPlayerPosition, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Ball"))    //if other collides with ball
        {
            Vector3 dir = hitTarget.position - AI.position;  //direction of the ball is the target minus where the ball is hit from
            if(user.position.z < 0)
            {
                dir = dir + new Vector3(0, 0, shotSpread);
            }else
            {
                dir = dir - new Vector3(0, 0, shotSpread);
            }
            other.GetComponent<Rigidbody>().velocity = dir.normalized * hitForce + new Vector3(0, 5, 0);    //the velocity of the ball
        }
    }
}
