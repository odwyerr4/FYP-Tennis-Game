using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Shot
{
    public float upForce;
    public float shotPower;
}
public class ShotTypes : MonoBehaviour
{
   public Shot topSpin;
   public Shot flatShot;
   public Shot serve;
}
