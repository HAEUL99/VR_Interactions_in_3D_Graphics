using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;


public class CanvasControl : MonoBehaviour
{

    public InputAction leftPri, rightPri;
    public GameObject mapUI, listUI;
    public bool ismapOpen, islistOpen; 

    private void Start()
    {
        leftPri.Enable();
        rightPri.Enable();
        mapUI.SetActive(false);
        listUI.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

        if (leftPri.triggered || Input.GetKeyDown(KeyCode.C)) //list
        {
            if (!ismapOpen) //false
            {
                listUI.SetActive(true);
            }
            else //true
            {
                listUI.SetActive(false);
            }
            ismapOpen = !ismapOpen;
        }

        if (rightPri.triggered || Input.GetKeyDown(KeyCode.D)) //map
        {
            if (!islistOpen) //false
            {
                mapUI.SetActive(true);
            }
            else //true
            {
                mapUI.SetActive(false);
            }
            islistOpen = !islistOpen;
        }
            
           

    }
    
}
