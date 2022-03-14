using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour
{
    Vector3 initialPosition;
    // Start is called before the first frame update
    void Start()
    {
        initialPosition = transform.position;   //set initial position variable to the start position of the ball
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.CompareTag("Wall"))      //if the ball hits a wall...
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;  //set the velocity of the ball to zero
            transform.position = initialPosition;               //set the ball position back to the start position
        }
    }
}
