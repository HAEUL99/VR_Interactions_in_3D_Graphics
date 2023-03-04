using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;


public class CanvasControl : MonoBehaviour
{

    private GameObject canvas;
    private bool IsOpen;

    public InputActionProperty RightButtonAction;
    public InputActionProperty RightButtonAction1;

    private void Start()
    {
        IsOpen = false;
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
       

        /*
        if (buttonClicked)
        {
            if (IsOpen)
            {
                canvas.SetActive(false);
                IsOpen = !IsOpen;
            }
            else
            {
                canvas.SetActive(true);
                IsOpen = !IsOpen;
            }
                
             

        }
        */
    }
}
