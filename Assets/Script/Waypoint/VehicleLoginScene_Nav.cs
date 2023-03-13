using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleLoginScene_Nav : CharacterNavigatorController
{
    public override void Init()
    {
        movementSpeed = 4;
        rotationSpeed = 120;
        stopDistance = 2f;
    }


    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    private void Update()
    {
        Movement(this.gameObject);
    }


}
