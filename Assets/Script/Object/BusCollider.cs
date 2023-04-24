using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrivedNearBusStopEvntArgs : EventArgs
{
    public string currentBusStopName;
    public int busdirection; // 0: forward, 1: back
    public GameObject bus;
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
    public int Busdirection;
    public bool isNearBusStop;

    void Start()
    {
        if (transform.parent.CompareTag("busforward"))
        {
            Busdirection = 0;
        }
        if (transform.parent.CompareTag("busback"))
        {
            Busdirection = 1;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag == "Player" && isNearBusStop)
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
            //isNearBusStop = true;
            //버스 속도 줄이기 이벤트
            ArrivedNearBusStopEvntArgs arg = new ArrivedNearBusStopEvntArgs
            {
                currentBusStopName = other.name,
                busdirection = Busdirection,
                bus = this.gameObject
            };

            this.ArrivedNearBusStopEvnt(this, arg);
        }


    }




}
