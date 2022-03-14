using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollow : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);     //set a ray as the mouse position

        if(Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask)){
            transform.position = raycastHit.point;
        }
    }
}
