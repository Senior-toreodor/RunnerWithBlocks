using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boundary : MonoBehaviour
{ 
    public static float leftSide = -2f;
    public static float rightSide = 2f;
    public float internalLeft;
    public float internalRight;
    void Update()
    { 
        internalLeft = leftSide;
        internalRight = rightSide;
    }
}
