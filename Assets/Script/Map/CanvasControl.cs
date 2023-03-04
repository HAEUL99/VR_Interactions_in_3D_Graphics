using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasControl : MonoBehaviour
{

    private GameObject canvas;
    private bool IsOpen;
    private void Start()
    {
        IsOpen = false;
        canvas = GameObject.Find("Canvas");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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
