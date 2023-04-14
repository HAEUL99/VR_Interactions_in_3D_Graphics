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

    public bool IsNearBusStop = false;
    public float stopSpeed = 1f;

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

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    private Rigidbody rb;
    private GameObject centerofMass;




    public override void Init()
    {

        movementSpeed = 7;
        rotationSpeed = 120;
        stopDistance = 0.5f;


        //bus stop at each station
        busCollider = transform.Find("EntranceTrigger").GetComponent<BusCollider>();
        busCollider.ArrivedNearBusStopEvnt += new EventHandler(Slowdown);
        frontdoorRight = transform.Find("FrontDoorRight").GetComponent<Animator>();
        frontdoorLeft = transform.Find("FrontDoorLeft").GetComponent<Animator>();

        //rb = GetComponent<Rigidbody>();
        //centerofMass = GameObject.Find("mass"); // 차마다 mass 오브젝트 추가하고 낮게 설정해준다. 
        //rb.centerOfMass = centerofMass.transform.localPosition;
    }



    public void Start()
    {
        Init();


    }

    public void Update()
    {

        MoveBus();


    }


    void MoveBus()
    {
        //Speed Control
        if (IsNearBusStop == false) // bus stop not arrived
        {
            //Debug.Log($"yValue: {yValue}");
            Movement(this.gameObject);


        }
        else // bus stop arrived
        {
            if (stopSpeed >= movementSpeed)
            {
                StartCoroutine(Stop());
                movementSpeed = 0f;
                Debug.Log($"bus name1: {gameObject.name}");

            }
            else
            {
                stopSpeed += Time.deltaTime * stopSpeed;
                Movement(this.gameObject, stopSpeed);
                Debug.Log($"bus name2: {gameObject.name}");

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

                //verticalInput
                //verticalInput = movementSpeed * Time.deltaTime;
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
        Debug.Log($"bus name: {IsNearBusStop}");
    }


    IEnumerator Stop()
    {
        frontdoorRight.SetBool("IsOpen", true);
        frontdoorLeft.SetBool("IsOpen", true);

        for (int i = 0; i < 10; i++)
        {

            yield return new WaitForSeconds(1f);

        }
        stopSpeed = 1f;
        movementSpeed = 7f;
        IsNearBusStop = false;
        frontdoorRight.SetBool("IsOpen", false);
        frontdoorLeft.SetBool("IsOpen", false);

    }

}