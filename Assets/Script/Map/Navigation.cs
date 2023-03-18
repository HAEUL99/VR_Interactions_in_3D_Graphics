using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class reDrawLineEvtArgs : EventArgs
{
    public int nearIndex;
    public Vector3 nearPointPos;
    public int nearBusStopIndex;
    public Vector3 nearBusStopPos;
    public int busStopIndexinIndex;

}


public class Navigation : MonoBehaviour
{
    [SerializeField]
    private Transform player;


    //research button
    public Button reSear;


    //Points
    [SerializeField]
    private Transform pointParent;
    private Transform[] points;
    


    public Transform[] busstops;



    //dotted line
    public event EventHandler reDrawLineEvt;

    private void Start()
    {


        reSear.onClick.AddListener(FindNearPoint);

        points = new Transform[pointParent.childCount];
        int i = 0;
        foreach (Transform child in pointParent)
        {
            points[i] = child;
            i++;
        }


    }

    public void FindNearPoint(object sender, EventArgs e)
    {

        //첫번째 가까운거
        float mindistance = Vector3.Distance(player.position, points[0].position);
        int minIndex = 0;
        Vector3 minPos = points[0].position;


        for (int i = 1; i < points.Length; i++)
        {
            if (mindistance > Vector3.Distance(player.position, points[i].position))
            {
                mindistance = Vector3.Distance(player.position, points[i].position);
                minIndex = i;
                minPos = points[i].position;
            }
        }

        //두번째 가까운거
        float min2distance = Vector3.Distance(player.position, points[0].position);
        int min2Index = 0;

        for (int i = 1; i < points.Length; i++)
        {
            if (i == minIndex)
            {
                continue;
            }
            if (min2distance > Vector3.Distance(player.position, points[i].position))
            {
                min2distance = Vector3.Distance(player.position, points[i].position);
                min2Index = i;
            }
        }

        

        //busttop
        float mindistanceDest = Vector3.Distance(player.position, busstops[0].position);
        int minIndexBusStop = 0;    
        Vector3 minPosBusStop = busstops[0].position;


        for (int i = 1; i < busstops.Length; i++)
        {

            if (mindistanceDest > Vector3.Distance(player.position, busstops[i].position))
            {
                mindistanceDest = Vector3.Distance(player.position, busstops[i].position);
                minIndexBusStop = i;
                
                minPosBusStop = busstops[i].position;
            }
        }

        int idx = 0;
        int busStopIndexinWhole = 0;
        foreach (Transform child in pointParent)
        {
            if (child.name == busstops[minIndexBusStop].name)
            {
                busStopIndexinWhole = idx;
            }
            idx++;

        }


        // 2번째 가까운게 버스정류장이랑 가까우면 두번째 가까운것을 minIndex로
        float distfromMin2 = Vector3.Distance(points[min2Index].position, points[busStopIndexinWhole].position);
        float distFromMin = Vector3.Distance(points[minIndex].position, points[busStopIndexinWhole].position);

        if (distfromMin2 < distFromMin)
        {
            minIndex = min2Index;
            minPos = points[minIndex].position;
        }

        reDrawLineEvtArgs arg = new reDrawLineEvtArgs
        {
            nearIndex = minIndex,
            nearPointPos = minPos,
            nearBusStopPos = minPosBusStop,
            nearBusStopIndex = minIndexBusStop,
            busStopIndexinIndex = busStopIndexinWhole
        };

        reDrawLineEvt(this, arg);


    }

    public void FindNearPoint()
    {

        //첫번째 가까운거
        float mindistance = Vector3.Distance(player.position, points[0].position);
        int minIndex = 0;
        Vector3 minPos = points[0].position;


        for (int i = 1; i < points.Length; i++)
        {
            if (mindistance > Vector3.Distance(player.position, points[i].position))
            {
                mindistance = Vector3.Distance(player.position, points[i].position);
                minIndex = i;
                minPos = points[i].position;
            }
        }

        //두번째 가까운거
        float min2distance = Vector3.Distance(player.position, points[0].position);
        int min2Index = 0;

        for (int i = 1; i < points.Length; i++)
        {
            if (i == minIndex)
            {
                continue;
            }
            if (min2distance > Vector3.Distance(player.position, points[i].position))
            {
                min2distance = Vector3.Distance(player.position, points[i].position);
                min2Index = i;
            }
        }



        //busttop
        float mindistanceDest = Vector3.Distance(player.position, busstops[0].position);
        int minIndexBusStop = 0;
        Vector3 minPosBusStop = busstops[0].position;


        for (int i = 1; i < busstops.Length; i++)
        {

            if (mindistanceDest > Vector3.Distance(player.position, busstops[i].position))
            {
                mindistanceDest = Vector3.Distance(player.position, busstops[i].position);
                minIndexBusStop = i;

                minPosBusStop = busstops[i].position;
            }
        }

        int idx = 0;
        int busStopIndexinWhole = 0;
        foreach (Transform child in pointParent)
        {
            if (child.name == busstops[minIndexBusStop].name)
            {
                busStopIndexinWhole = idx;
            }
            idx++;

        }


        // 2번째 가까운게 버스정류장이랑 가까우면 두번째 가까운것을 minIndex로
        float distfromMin2 = Vector3.Distance(points[min2Index].position, points[busStopIndexinWhole].position);
        float distFromMin = Vector3.Distance(points[minIndex].position, points[busStopIndexinWhole].position);

        if (distfromMin2 < distFromMin)
        {
            minIndex = min2Index;
            minPos = points[minIndex].position;
        }

        reDrawLineEvtArgs arg = new reDrawLineEvtArgs
        {
            nearIndex = minIndex,
            nearPointPos = minPos,
            nearBusStopPos = minPosBusStop,
            nearBusStopIndex = minIndexBusStop,
            busStopIndexinIndex = busStopIndexinWhole
        };

        reDrawLineEvt(this, arg);

    }


}
