using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IKManager : MonoBehaviour
{
    public Joint root;  //root of the armature
    public Joint mid1;   //middle joint
    public Joint mid2;   //middle joint
    public Joint end;   //end joint

    //public GameObject target;   //sphere

    public float threshold = 0.05f;
    public float rate = 5f;
    public int steps;
    public float shotSpread;
    public GameObject easyToggle;
    public GameObject mediumToggle;
    public GameObject hardToggle;
    ShotTypes shotTypes;
    Shot currentShot;
    public Transform ball;
    public Transform hitTarget;
    public Transform user;
    public Transform racquetHead;

    void Start()
    {
        shotTypes = GetComponent<ShotTypes>();  //initialise shotTypes
        currentShot = shotTypes.topSpin;       //set current shot to top spin
    }

    float CalculateSlope(Joint joint)
    {
        float deltaTheta = 0.1f;
        float distance1 = GetDistance(end.transform.position, ball.transform.position);

        joint.Rotate(deltaTheta);

        float distance2 = GetDistance(end.transform.position, ball.transform.position);

        joint.Rotate(-deltaTheta);

        return (distance2 - distance1) / deltaTheta;
    }

    

    // Update is called once per frame
    public void Update()
    {
        Rigidbody root_rb = root.GetComponent<Rigidbody>();
        Destroy(root_rb);

        Rigidbody end_rb = end.GetComponent<Rigidbody>();
        //Destroy(end_rb);

        Rigidbody mid1_rb = mid1.GetComponent<Rigidbody>();
        Destroy(mid1_rb);

        Rigidbody mid2_rb = mid2.GetComponent<Rigidbody>();
        Destroy(mid2_rb);

        if(easyToggle.GetComponent<Toggle>().isOn == true)                  //if easy difficulty selected
        {
            steps = 4;
        }
        else if(mediumToggle.GetComponent<Toggle>().isOn == true)           //if medium difficulty selected
        {
            steps = 6;
        }
        else if(hardToggle.GetComponent<Toggle>().isOn == true)             //if hard difficulty selected
        {
            steps = 8;
        }

        for(int i = 0; i < steps; ++i){
            if(GetDistance(end.transform.position, ball.transform.position) > threshold)
            {
                Joint current = root;
                while(current != null)
                {
                    float slope = CalculateSlope(current);
                    current.Rotate(-slope * rate);
                    current = current.GetChild();
                }
            }
        }
    }

    float GetDistance(Vector3 point1, Vector3 point2)
   {
       return Vector3.Distance(point1, point2);     //distance between point 1 and 2
   }
}
