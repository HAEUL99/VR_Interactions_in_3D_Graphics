using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleNPC_Nav : CharacterNavigatorController
{
    public override void Init()
    {
        movementSpeed = 4;
        rotationSpeed = 120;
        stopDistance = 2f;
    }

    public void Start()
    {
        Init();
    }

    public void Update()
    {
        Movement(this.gameObject);
    }
}
