using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleNPC_Nav : CharacterNavigatorController
{

    //event receiver
    private BusCollider busCollider;

    //animator on Doors
    private Animator frontdoorRight;
    private Animator frontdoorLeft;

    private bool IsNearBusStop = false;
    private float stopSpeed = 0.5f;

    


    public override void Init()
    {
        movementSpeed = 4;
        rotationSpeed = 120;
        stopDistance = 2f;
    }

    public void Start()
    {
        Init();
        busCollider = transform.Find("EntranceTrigger").GetComponent<BusCollider>();
        busCollider.ArrivedNearBusStopEvnt += new EventHandler(Slowdown);
        frontdoorRight = transform.Find("FrontDoorRight").GetComponent<Animator>();
        frontdoorLeft = transform.Find("FrontDoorLeft").GetComponent<Animator>();
    }

    public void Update()
    {
        if (!IsNearBusStop)
            Movement(this.gameObject);
        else
        {

            if (stopSpeed >= movementSpeed)
            {
                Debug.Log($"stopSpeed: {stopSpeed}");
                StartCoroutine(Stop());
                movementSpeed = 0f;

            }
            else
            {
                stopSpeed += Time.deltaTime * stopSpeed;
                Movement(this.gameObject, stopSpeed);
            }
            
            

        }
            
    }

    void Slowdown(object sender, EventArgs e)
    {
        IsNearBusStop = true;
    }


    IEnumerator Stop()
    {
        frontdoorRight.SetBool("IsOpen", true);
        frontdoorLeft.SetBool("IsOpen", true);

        for (int i = 0; i < 10; i++)
        {
            Debug.Log($"{i} 초 지남");
            yield return new WaitForSeconds(1f);

        }
        stopSpeed = 0.5f;
        movementSpeed = 4f;
        IsNearBusStop = false;
        frontdoorRight.SetBool("IsOpen", false);
        frontdoorLeft.SetBool("IsOpen", false);
    }

}
