using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleNPCTutorial_Nav : CharacterNavigatorController
{
    //event receiver
    private BusCollider busCollider;

    //animator on Doors
    private Animator frontdoorRight;
    private Animator frontdoorLeft;

    public bool IsNearBusStop = false;
    private float stopSpeed = 0.5f;

    //vehicle wheel Object
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce;
    private bool isBreaking;


    [SerializeField] private float motorForce = 100f;
    [SerializeField] private float breakForce = 100f;
    [SerializeField] private float maxSteerAngle = 100f;
    [SerializeField] private float DownForceValue = 4000f;



    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    public Transform[] wheelTransform;

    private Rigidbody rb;
    private GameObject centerofMass;




    public override void Init()
    {

        movementSpeed = 4;
        rotationSpeed = 120;
        stopDistance = 2f;


        //bus stop at each station
        busCollider = transform.Find("EntranceTrigger").GetComponent<BusCollider>();
        busCollider.ArrivedNearBusStopEvnt += new EventHandler(Slowdown);
        busCollider.PlayerEnterBusEvnt += new EventHandler(IsPlayerRideFunc);
        frontdoorRight = transform.Find("FrontDoorRight").GetComponent<Animator>();
        frontdoorLeft = transform.Find("FrontDoorLeft").GetComponent<Animator>();

    }



    public void Start()
    {
        Init();


    }

    public void Update()
    {

        MoveBus();


    }

    void VehicleRotate()
    {
        for (int i = 0; i < 4; i++)
        {
            wheelTransform[i].Rotate(new Vector3(200f, 0f, 0f) * Time.deltaTime);
        }
    }

    void IsPlayerRideFunc(object sender, EventArgs e)
    {
        stopSpeed = 0.5f;
        movementSpeed = 4f;
        IsNearBusStop = false;
        frontdoorRight.SetBool("IsOpen", false);
        frontdoorLeft.SetBool("IsOpen", false);
    }

    void MoveBus()
    {
        //Speed Control
        if (!IsNearBusStop) // bus stop not arrived
        {
            Movement(this.gameObject);
        }
        else // bus stop arrived
        {
            if (stopSpeed >= movementSpeed)
            {
                Stop();
                movementSpeed = 0f;
            }
            else
            {
                stopSpeed += Time.deltaTime * stopSpeed;
                Movement(this.gameObject, stopSpeed);
            }
        }


    }

    public override void Movement(GameObject childobject)
    {
        // 목적지와 객체의 포지션이 일치하지 않으면, 
        if (childobject.transform.position != destination)
        {
            // 목적지로의 벡터를 구함
            Vector3 destinationDirection = destination - transform.position;
            destinationDirection.y = 0;

            // 목적지까지의 거리 변수
            float destinationDistance = destinationDirection.magnitude;
            if (destinationDistance >= stopDistance)
            {
                reachedDestination = false;
                // destinationDirectin으로 방향 회전하고, 이동
                Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);

                //horizontalInput
                childobject.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                childobject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);

            }
            else
            {
                reachedDestination = true;
            }

        }


    }


    void Slowdown(object sender, EventArgs e)
    {
        IsNearBusStop = true;
    }


    void Stop()
    {
        frontdoorRight.SetBool("IsOpen", true);
        frontdoorLeft.SetBool("IsOpen", true);
    }



}
