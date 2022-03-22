using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joint : MonoBehaviour
{
   public Joint child;

   public Joint GetChild()
   {
       return child;    //returns child of a joint
   }

   public void Rotate(float angle)
   {
       transform.Rotate(Vector3.up * angle);    //rotate the joint by an angle
   }
}
