using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleTruck_Nav : CharacterNavigatorController
{
    public bool isEvent;
    private WaypointNavigator waypointNavigator;
    public bool isFarward;

    public override void Init()
    {
        movementSpeed = 5;
        rotationSpeed = 120;
        stopDistance = 2f;
    }

    private void Start()
    {
        Init();
        waypointNavigator = gameObject.GetComponent<WaypointNavigator>();

    }

    

    public void Update()
    {
        if (!isEvent)
            return;
        MoveTruck();
    }

    void MoveTruck()
    {
   
        if (waypointNavigator.currentWaypoint.name == "Waypoint 5")
        {
            isFarward = true;
            movementSpeed = 13;
        }
        

        Movement(this.gameObject, true, isFarward);
    }
}
