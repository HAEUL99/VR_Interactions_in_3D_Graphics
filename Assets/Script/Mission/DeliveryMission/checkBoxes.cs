using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumManager;

public class deliveryMissionComplArgs : EventArgs
{
    public MissionType _missionType;
}


public class checkBoxes : MonoBehaviour
{

    private struct Boxes
    {
        public Boxes(GameObject box, bool isIn)
        {
            Box = box;
            IsIn = isIn;
        }

        public GameObject Box { get;}
        public bool IsIn { get;}
    }
    [SerializeField]
    private GameObject[] boxObjs;

    private Boxes[] boxes;

    /*
    public bool IsChecked;
    public int count;
    */
    public event EventHandler deliveryMissionCompl;



    private void Start()
    {
        boxes = new Boxes[boxObjs.Length];
        for (int i = 0; i < boxObjs.Length; i++)
        {
            boxes[i] = new Boxes(boxObjs[i], false);
        }

    }

    

    /*
    private void OnTriggerEnter(Collider other)
    {
        //player
        if (other.gameObject.tag == "boxes")
        {
            for (int i = 0; i < boxObjs.Length; i++)
            {
                if (other.gameObject == boxes[i].Box)
                {

                    boxes[i] = new Boxes(boxObjs[i], true);
                    return;
                }
            }
        }

    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "boxes")
        {

            for (int i = 0; i < boxObjs.Length; i++)
            {
                if (other.gameObject == boxes[i].Box)
                {
                    boxes[i] = new Boxes(boxObjs[i], false);
                    return;
                }
            }
        }

    }
    
    private bool CheckIsAllInTruck()
    {
        count = 0;
        for (int i = 0; i < boxObjs.Length; i++)
        {
            if (boxes[i].IsIn)
                count++;
            
        }

        if (count == boxObjs.Length)
            return true;
        else
            return false;
    }

    private void Update()
    {
        if (CheckIsAllInTruck() && IsChecked == false)
        {
            IsChecked = true;
            CompletedeliveryMission();
            return;

        }

    }
    */

    public void SetUp(int numOfbox)
    {
        boxes[numOfbox] = new Boxes(boxObjs[numOfbox], true);
        int count = 0;

        for (int i = 0; i < boxObjs.Length; i++)
        {
            if (boxes[i].IsIn)
                count++;
        }

        if (count == boxObjs.Length)
        {
            CompletedeliveryMission();
        }
    }

    public void SetDown(int numOfbox)
    {
        boxes[numOfbox] = new Boxes(boxObjs[numOfbox], false);
    }


    private void CompletedeliveryMission()
    {
        Debug.Log("delivery");
        deliveryMissionComplArgs arg = new deliveryMissionComplArgs
        {
            _missionType = EnumManager.MissionType.boxes
        };
        this.deliveryMissionCompl(this, arg);

    }

}
