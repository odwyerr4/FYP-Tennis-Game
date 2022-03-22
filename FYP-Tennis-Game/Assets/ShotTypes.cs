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
   public Shot forehand;
   public Shot backhand;
   public Shot serve;
}
