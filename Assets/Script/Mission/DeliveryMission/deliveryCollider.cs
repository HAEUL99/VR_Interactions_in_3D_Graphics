using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllBoxesInTruckArgs : EventArgs { }

public class deliveryCollider : MonoBehaviour
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
    private bool IsChecked;

    public event EventHandler AllBoxesInTruck;

    private void Start()
    {
        boxes = new Boxes[boxObjs.Length];
        for (int i = 0; i < boxObjs.Length; i++)
        {
            boxes[i] = new Boxes(boxObjs[i], false);
        }

    }


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
        int count = 0;
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
            AllBoxesInTruckArgs arg = new AllBoxesInTruckArgs();
            this.AllBoxesInTruck(this, arg);

        }

    }

}
