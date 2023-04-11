using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.InputSystem;


public class CanvasControl : MonoBehaviour
{
    
    public InputAction buttonAction;
    private GameObject canvas;
    private bool IsOpen;


    private void Start()
    {
        buttonAction.Enable();
        IsOpen = false;
        canvas = GameObject.Find("Canvas");


    }

    // Update is called once per frame
    void Update()
    {
     
        if (buttonAction.triggered || Input.GetKeyDown(KeyCode.Space))
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

    }
    
}
