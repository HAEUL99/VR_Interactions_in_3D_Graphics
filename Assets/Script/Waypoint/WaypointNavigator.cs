using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaypointNavigator : MonoBehaviour
{
    //CharacterNavigatorController controller;
    VehicleNPC_Nav controller;
    public Waypoint currentWaypoint;
    private string gameObjectName;
    public event EventHandler CurrentWaypointNull;
    CurrentWaypointNullArgs arg;


    private void Awake()
    {
        controller = GetComponent<VehicleNPC_Nav>();
    }

    private void Start()
    {
        controller.SetDestination(currentWaypoint.GetPosition());
        gameObjectName = gameObject.name;
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
