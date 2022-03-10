using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserScript : MonoBehaviour
{
    public Transform aimTarget;
    float moveSpeed = 2f;
    public float targetX;
    public float targetY;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float upAndDown = Input.GetAxisRaw("Horizontal");
        float sideToSide = Input.GetAxisRaw("Vertical");

        if((upAndDown != 0) || (sideToSide != 0))
        {
            transform.Translate(new Vector3(sideToSide, 0, upAndDown) * moveSpeed * Time.deltaTime);
        }

        if(Input.GetKeyDown(KeyCode.I))
        {
            targetX = targetX - 0.2f;
            aimTarget.Translate(new Vector3(targetY, 0, targetX));
        }
        else if(Input.GetKeyDown(KeyCode.K))
        {
            targetX = targetX + 0.2f;
            aimTarget.Translate(new Vector3(targetY, 0, targetX));
        }
        Debug.Log(targetX);
    }
}