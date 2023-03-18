using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaypointNavigator : MonoBehaviour
{
    //CharacterNavigatorController controller;
    CharacterNavigatorController controller;
    public Waypoint currentWaypoint;
    private string gameObjectName;
    public event EventHandler CurrentWaypointNull;
    CurrentWaypointNullArgs arg;



    private void Start()
    {
        gameObjectName = gameObject.name;
        if (gameObjectName == "Mother")
        {
            controller = GetComponent<MomNPC_Nav>();
        }
        else if (gameObjectName == "SchoolBus")
        {
            controller = GetComponent<VehicleLoginScene_Nav>();
        }
        else
        {
            controller = GetComponent<VehicleNPC_Nav>();
        }

        controller.SetDestination(currentWaypoint.GetPosition());
        
        arg = new CurrentWaypointNullArgs();
        

    }

    public class CurrentWaypointNullArgs : EventArgs
    {

    }

    private void Update()
    {
        if (controller.reachedDestination)
        {
            //CurrentWaypointNullArgs arg = new CurrentWaypointNullArgs();
            if (currentWaypoint.nextWaypoint != null)
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
                controller.SetDestination(currentWaypoint.GetPosition());
            }
            if (gameObjectName == "Mother")
            {
                if (currentWaypoint.name == "Waypoint 4")
                { 
                    this.CurrentWaypointNull(this, arg);
                }
            }


          
            
        }
    }
}
