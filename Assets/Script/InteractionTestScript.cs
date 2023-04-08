using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;
using System.Diagnostics;

public class InteractionTestScript : MonoBehaviour
{
    //vr interaction
    public InputAction leftThum, rightThum, leftPri, rightPri;
    public int VrInput;

    void Start() 
    {
        leftThum.Enable();
        rightThum.Enable();
        leftPri.Enable();
        rightPri.Enable();
 
    }

    void Update()
    {
        if (leftThum.triggered || Input.GetKeyDown(KeyCode.A))
        {
            VrInput = 1;

        }
        if (rightThum.triggered || Input.GetKeyDown(KeyCode.B))
        {
            VrInput = 2;

        }
        if (leftPri.triggered || Input.GetKeyDown(KeyCode.C))
            VrInput = 3;
        if (rightPri.triggered || Input.GetKeyDown(KeyCode.D))
            VrInput = 4;
    }    
}
