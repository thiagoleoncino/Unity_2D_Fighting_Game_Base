using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scr_03_Character_Stats : MonoBehaviour
{
    [Header("Principal Stats")]
    public float groundFowardSpeed;
    public float groundBackwardSpeed;

    [Space]
    public int jumpAmount;
    public int jumpHeight;

    [Space]
    public int dashFowardSpeed;
    public int dashBackwardSpeed;
}
