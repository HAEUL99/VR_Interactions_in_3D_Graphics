using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CurrentWaypointNullArgs : EventArgs { }
public class CurrentWaypointNull1Args : EventArgs { }

public class WaypointNavigator : MonoBehaviour
{
    //CharacterNavigatorController controller;
    CharacterNavigatorController controller;
    public Waypoint currentWaypoint;
    private string gameObjectName;
    public event EventHandler CurrentWaypointNull;
    CurrentWaypointNullArgs arg;


    //private bool IsMomFinishedDialogue;
    public event EventHandler CurrentWaypointNull1;
    public bool Ischeck;

    public GameObject Mom_before;
    public GameObject Mom_after;



    private void Start()
    {
        gameObjectName = gameObject.name;
        if (gameObjectName == "Mother" || gameObjectName == "Mother_after")
        {
            controller = GetComponent<MomNPC_Nav>();
            arg = new CurrentWaypointNullArgs();
            
        }
        else if (gameObjectName == "SchoolBus")
        {
            controller = GetComponent<VehicleLoginScene_Nav>();
        }
        else if (gameObjectName == "TutorialBus")
        {
            controller = GetComponent<VehicleNPCTutorial_Nav>();
        }
        else
        {
            controller = GetComponent<VehicleNPC_Nav>();
        }

        controller.SetDestination(currentWaypoint.GetPosition());
    }

    //private void motherCharMove(object sender, EventArgs e)
    //{
    //    IsMomFinishedDialogue = true;
    //}

    private void Update()
    {
        if (controller.reachedDestination)
        { 
            if (currentWaypoint.nextWaypoint != null && gameObjectName != "Mother")
            {
                currentWaypoint = currentWaypoint.nextWaypoint;
                controller.SetDestination(currentWaypoint.GetPosition());
            }
            if (gameObjectName == "Mother")
            {
                if (currentWaypoint.nextWaypoint != null && currentWaypoint.name != "Waypoint 4")
                {
                    currentWaypoint = currentWaypoint.nextWaypoint;
                    controller.SetDestination(currentWaypoint.GetPosition());
                }
                if (currentWaypoint.name == "Waypoint 4")
                {
                    this.CurrentWaypointNull(this, arg);
                    currentWaypoint = currentWaypoint.nextWaypoint;
                }
                if (currentWaypoint.name == "Waypoint 9" && Ischeck == false)
                {
                    Ischeck = true;
                    CurrentWaypointNull1Args arg1 = new CurrentWaypointNull1Args();
                    this.CurrentWaypointNull1(this, arg1);


                    Mom_after.SetActive(true);
                    Mom_after.transform.position = Mom_before.transform.position;
                    Mom_after.transform.rotation = Mom_before.transform.rotation;

                    Mom_before.SetActive(false);
                }
            }           
        }
    }
}
