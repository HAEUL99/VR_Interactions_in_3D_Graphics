using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.InputSystem;

public class VRInputInteraction : MonoBehaviour
{
    //vr interaction
    public InputAction leftPri, rightPri;
    public bool ismapOpen, islistOpen;
    public GameObject mapUI;
    public GameObject listUI;


    void Start()
    {
        leftPri.Enable();
        rightPri.Enable();
        mapUI.SetActive(false);
        listUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (leftPri.triggered || Input.GetKeyDown(KeyCode.C))
        {
            if (ismapOpen == false)
            {
                ismapOpen = true;
                mapUI.SetActive(true);
            }
            else
            {
                ismapOpen = false;
                mapUI.SetActive(false);
            }
            
        }
        if (leftPri.triggered || Input.GetKeyDown(KeyCode.D))
        {
            if (islistOpen == false)
            {
                islistOpen = true;
                listUI.SetActive(true);
            }
            else
            {
                islistOpen = false;
                listUI.SetActive(false);
            }
        }
  
    }
}
