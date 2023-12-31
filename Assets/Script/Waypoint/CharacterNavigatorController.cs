using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterNavigatorController : MonoBehaviour
{
    
    public float movementSpeed;
    public float rotationSpeed;
    public float stopDistance;

    public Animator animator;
    public bool reachedDestination;

    public Vector3 destination;
    public Vector3 lastPosition;
    public Vector3 velocity;



    public virtual void Init()
    {
        movementSpeed = 1;
        rotationSpeed = 120;
        stopDistance = 0.5f;
    }



    public virtual void Movement(GameObject childobject)
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
                childobject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }

        }

    }



    public void Movement(GameObject childobject, float stopSpeed)
    {
        float stopspeed = stopSpeed;
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
                childobject.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                childobject.transform.Translate(Vector3.forward * (movementSpeed - stopspeed) * Time.deltaTime);
            }
            else
            {
                reachedDestination = true;
            }

        }


        lastPosition = childobject.transform.position;


    }

    public void Movement(GameObject childobject, bool isTruck, bool isFarward)
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
                if (isFarward == false)
                {
                    Vector3 destinationDirection_Back = new Vector3(destinationDirection.x * (-1), destinationDirection.y, destinationDirection.z * (-1));
                    // destinationDirectin으로 방향 회전하고, 이동
                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection_Back);

                    //horizontalInput
                    childobject.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                    //verticalInput
                    childobject.transform.Translate(Vector3.back * movementSpeed * Time.deltaTime);
                }
                if(isFarward == true)
                {

                    //reachedDestination = false;
                    // destinationDirectin으로 방향 회전하고, 이동
                    Quaternion targetRotation = Quaternion.LookRotation(destinationDirection);

                    //horizontalInput
                    childobject.transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);


           
                    childobject.transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
                }
                
            }
            else
            {
                reachedDestination = true;
            }

        }


    }




    public void SetDestination(Vector3 destination)
    {
        this.destination = destination;
        reachedDestination = false;
    }

}
