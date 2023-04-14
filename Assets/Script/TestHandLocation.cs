using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Inputs;


public class TestHandLocation : MonoBehaviour
{

    public InputAction leftposition;

    void Start()
    {
        leftposition.Enable();
    }
    // Update is called once per frame
    void Update()
    {
        Vector3 vec = leftposition.ReadValue<Vector3>();
        float posx = vec.x;
        Debug.Log($"left hand position: {posx}");
    }
}
