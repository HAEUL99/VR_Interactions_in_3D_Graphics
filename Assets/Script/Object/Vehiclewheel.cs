using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehiclewheel : MonoBehaviour
{
    public SphereCollider[] wheelCol;
    public Transform[] wheelTransform;

    private void Update()
    {
        for (int i = 0; i < 4; i++)
        {
            wheelTransform[i].Rotate(new Vector3(200f, 0f, 0f) * Time.deltaTime);
        }
    }
}
