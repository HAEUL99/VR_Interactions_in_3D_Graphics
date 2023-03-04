using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivedNearBusStopEvntArgs : EventArgs
{

}


//this component is on the Bus Enterance Trigger
public class BusCollider : MonoBehaviour
{

    //event sender
    public event EventHandler ArrivedNearBusStopEvnt;


    private void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag == "Player")
        {
            
        }

        //near bus stop
        if (other.gameObject.tag == "busstop")
        {
            Debug.Log("닿음");
            //버스 속도 줄이기 이벤트
            ArrivedNearBusStopEvntArgs arg = new ArrivedNearBusStopEvntArgs
            {
      
            };

            this.ArrivedNearBusStopEvnt(this, arg);
        }
    }

   
}
