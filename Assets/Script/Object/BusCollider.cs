using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivedNearBusStopEvntArgs : EventArgs
{
    public string currentBusStopName;
}

public class PlayerEnterBusEvntArgs : EventArgs
{
    public GameObject bus;
}


//this component is on the Bus Enterance Trigger
public class BusCollider : MonoBehaviour
{

    //event sender
    public event EventHandler ArrivedNearBusStopEvnt;
    public event EventHandler PlayerEnterBusEvnt;


    private void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag == "Player")
        {
            PlayerEnterBusEvntArgs arg = new PlayerEnterBusEvntArgs
            {
                bus = this.gameObject
            };
            this.PlayerEnterBusEvnt(this, arg);

            


        }

        //near bus stop
        if (other.gameObject.tag == "busstop")
        {
       
            //버스 속도 줄이기 이벤트
            ArrivedNearBusStopEvntArgs arg = new ArrivedNearBusStopEvntArgs
            {
                currentBusStopName = other.name
            };

            this.ArrivedNearBusStopEvnt(this, arg);
        }
    }

   
}
