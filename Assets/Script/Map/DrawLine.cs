using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.ComponentModel;

public class DrawLine : MonoBehaviour
{
    //event receive
    public ListofDest listofDest;

    //Line
    public LineRenderer lineRenderer;

    //정방향 
    [SerializeField]
    private Transform pointParent;
    public Transform[] points;
    //정방향 총거리
    public float forwardDist;

    //역방향 
    private Transform[] repoints;
    //역방향 총거리
    public float reverseDist;
    private bool IsForward;


    public Transform[] busstops;


    public Navigation nav;
    private Transform player;


    //marcus market destIndex
    int marcusIndex;
    int destIndex;
    string destname;

    //이벤트변수
    Vector3 playerPos;
    int pointIndex;
    int busStopIndex;
    Vector3 pointPos;
    Vector3 busPos;
    int busStopIndexinIndex;

    //distance
    public float disFromPlayertoPoint;
    public float disFromBustoDest;
    public float disFromBusStoptoDest;

    public Transform[] destPositions;
    private int destNum;
    Transform destTransform = null;

    //event receive
    public ListofRout listofRout;

    //"Marcus Market", "New Life Church", "Rachel Bookkeeping"
    enum DestIndex
    {
        [Description("marcus market")] //26
        marcus_market = 26,
        [Description("new life church")] //4
        new_life_church = 4,
        [Description("rachel bookkeeping")] //18
        rachel_bookkeeping = 18

    }


    
    void Start()
    {

        //point 
        points = new Transform[pointParent.childCount];
        repoints = new Transform[pointParent.childCount];
        int i = 0;
        foreach (Transform child in pointParent)
        {
            points[i] = child;
            i++;
        }

        //repoint
        i = 0;
        for (int j = pointParent.childCount - 1; j >= 0; j--)
        {
            repoints[j] = points[i++];
        }


        //nav = FindObjectOfType<Navigation>();     
        player = GameObject.FindWithTag("Player").GetComponent<Transform>();

        nav.reDrawLineEvt += new EventHandler(Init);
        nav.reDrawLineEvt += new EventHandler(ComparedtwoWays);


        
        nav.reDrawLineEvt += new EventHandler(CalculateDottedDistance);
        nav.reDrawLineEvt += new EventHandler(CalculateLineDistance);

        listofRout.ClickedRecomBtnEvnt += new EventHandler(DrawDottedLine);
        listofRout.ClickedRecomBtnEvnt += new EventHandler(DrawNavLine);

    }
    

    public void Init(object sender, EventArgs e)
    {

        


        reDrawLineEvtArgs args = e as reDrawLineEvtArgs;
        playerPos = player.position;
        pointIndex = args.nearIndex;
        busStopIndex = args.nearBusStopIndex;
        pointPos = args.nearPointPos;
        busPos = args.nearBusStopPos;
        busStopIndexinIndex = args.busStopIndexinIndex;

        destname = listofDest.alpha;
        switch (destname)
        {
            case "marcus market":
                destTransform = destPositions[0];
                marcusIndex = 26;
                break;
            case "new life church":
                destTransform = destPositions[1];
                marcusIndex = 4;
                break;
            case "rachel bookkeeping":
                destTransform = destPositions[2];
                marcusIndex = 18;
                break;
        }

    }

    public void ComparedtwoWays(object sender, EventArgs e)
    {
        //초기화
        forwardDist = 0.0f;
        reverseDist = 0.0f;
        //정뱡향 계산
        //점선
        //player -> nearPoint
        forwardDist += Vector3.Distance(playerPos, pointPos);
        //nearPoint -> nearPoint -> ... -> Bus Stop
        int next = 0;
        if (pointIndex <= busStopIndexinIndex)
        {
            for (int i = pointIndex; i < busStopIndexinIndex; i++)
            {
                next = i + 1;
                forwardDist += Vector3.Distance(points[i].position, points[next].position);
            }
        }
        else
        {
            for (int i = busStopIndexinIndex; i < pointIndex; i++)
            {
                next = i + 1;
                forwardDist += Vector3.Distance(points[i].position, points[next].position);
            }
        }
        //Bus Stop -> Dest
        forwardDist += Vector3.Distance(points[marcusIndex].position, destTransform.position);

        //실선
        //set the position
        next = 0;
        if (busStopIndexinIndex <= marcusIndex)
        {
            for (int i = busStopIndexinIndex; i < marcusIndex + 1; i++)
            {
                if (i != marcusIndex)
                {
                    next = i + 1;
                    forwardDist += Vector3.Distance(points[i].position, points[next].position);
                }
                    
            }
        }
        else //지나쳤을 경우
        {
            for (int i = busStopIndexinIndex; i < points.Length; i++)
            {
                if (i != points.Length - 1)
                {
                    next = i + 1;
                    forwardDist += Vector3.Distance(points[i].position, points[next].position);
                }
                    
            }
            for (int i = 0; i < marcusIndex + 1; i++)
            {
                if (i != marcusIndex)
                {
                    next = i + 1;
                    forwardDist += Vector3.Distance(points[i].position, points[next].position);
                }
                    
            }
        }


        //역방향 계산
        //점선
        //player -> nearPoint
        reverseDist += Vector3.Distance(playerPos, pointPos);
        //nearPoint -> nearPoint -> ... -> Bus Stop
        int next1 = 0;
        if (pointIndex <= busStopIndexinIndex)
        {
            for (int i = pointIndex; i < busStopIndexinIndex; i++)
            {
                next1 = i + 1;
                reverseDist += Vector3.Distance(points[i].position, points[next1].position);
            }
        }
        else
        {
            for (int i = busStopIndexinIndex; i < pointIndex; i++)
            {
                next1 = i + 1;
                reverseDist += Vector3.Distance(points[i].position, points[next1].position);
            }
        }
        //Bus Stop -> Dest
        reverseDist += Vector3.Distance(points[marcusIndex].position, destTransform.position);

        //실선
        //set the position
        next1 = 0;
        if (busStopIndexinIndex <= marcusIndex)
        {

            for (int i = busStopIndexinIndex; i > 0; i--)
            {
                next1 = i - 1;
                reverseDist += Vector3.Distance(points[i].position, points[next1].position);
            }
            for (int i = points.Length - 1; i > marcusIndex; i--)
            {
                next1 = i - 1;
                reverseDist += Vector3.Distance(points[i].position, points[next1].position);
            }
        }
        else 
        {
            for (int i = busStopIndexinIndex; i > marcusIndex; i--)
            {
                next1 = i - 1;
                reverseDist += Vector3.Distance(points[i].position, points[next1].position);
            }
        }

        IsForward = (forwardDist <= reverseDist) ? true : false;

    }


    public void CalculateDottedDistance(object sender, EventArgs e)
    {
        disFromPlayertoPoint = 0.0f;
        //player -> near Point
        disFromPlayertoPoint = DottedLine.DottedLine.Instance.DrawDottedLineFromPlayer(playerPos, pointPos);



        //near Point -> next Point -> ... -> bus stop (정방향)
        if (pointIndex <= busStopIndexinIndex)
        {
            for (int i = pointIndex; i < busStopIndexinIndex; i++)
            {
                int next = i + 1;
                disFromPlayertoPoint +=
                    DottedLine.DottedLine.Instance.DrawDottedLineFromObj(points[i].position, points[next].position);

            }
        }
        else
        {
            for (int i = busStopIndexinIndex; i < pointIndex; i++)
            {
                int next = i + 1;
                disFromPlayertoPoint +=
                    DottedLine.DottedLine.Instance.DrawDottedLineFromObj(points[i].position, points[next].position);

            }
        }

        //bus stop -> dest
        //draw the line from busStop to destination
        disFromBusStoptoDest = DottedLine.DottedLine.Instance.DrawDottedLineFromObj(points[marcusIndex].position, destTransform.position);

    }
    public void DrawDottedLine(object sender, EventArgs e)
    {
        DottedLine.DottedLine.Instance.Render();
    }

    public void CalculateLineDistance(object sender, EventArgs e)
    {
        disFromBustoDest = 0.0f;

        if (IsForward) //정방향
        {
            int num = 0;
            //set the position
            if (busStopIndexinIndex <= marcusIndex)
            {
                //lineRenderer.positionCount = marcusIndex - busStopIndexinIndex + 1;

                for (int i = busStopIndexinIndex; i < marcusIndex + 1; i++)
                {
                    //lineRenderer.SetPosition(num++, points[i].position);
                    int next = i + 1;
                    if (i != marcusIndex)
                    {
                        disFromBustoDest += Vector3.Distance(points[i].position, points[next].position);
                    }


                }
            }
            else //지나쳤을 경우
            {
                //lineRenderer.positionCount = marcusIndex + (points.Length - busStopIndexinIndex) + 1;

                for (int i = busStopIndexinIndex; i < points.Length; i++)
                {
                    //lineRenderer.SetPosition(num++, points[i].position);
                    int next = i + 1;
                    if (i != points.Length - 1)
                    {
                        disFromBustoDest += Vector3.Distance(points[i].position, points[next].position);
                    }

                }

                for (int i = 0; i < marcusIndex + 1; i++)
                {
                    //lineRenderer.SetPosition(num++, points[i].position);
                    int next = i + 1;
                    if (i != marcusIndex)
                    {
                        disFromBustoDest += Vector3.Distance(points[i].position, points[next].position);
                    }

                }

            }
        }
        else // 역방
        {
            int num = 0;
            //set the position
            if (busStopIndexinIndex <= marcusIndex)
            {

                //lineRenderer.positionCount = busStopIndexinIndex + 1 + (points.Length - marcusIndex);


                int next = 0;
                for (int i = busStopIndexinIndex; i > 0; i--)
                {
                    //lineRenderer.SetPosition(num++, points[i].position);
                    next = i - 1;
                    disFromBustoDest += Vector3.Distance(points[i].position, points[next].position);


                }
                //lineRenderer.SetPosition(num++, points[0].position);
                disFromBustoDest += Vector3.Distance(points[0].position, points[points.Length - 1].position);
                for (int i = points.Length - 1; i >= marcusIndex; i--)
                {
                    //lineRenderer.SetPosition(num++, points[i].position);
                    next = i - 1;
                    disFromBustoDest += Vector3.Distance(points[i].position, points[next].position);

                }

            }
            else
            {
                //lineRenderer.positionCount = busStopIndexinIndex - marcusIndex + 1;

                int next = 0;
                for (int i = busStopIndexinIndex; i >= marcusIndex; i--)
                {
                    //lineRenderer.SetPosition(num++, points[i].position);
                    next = i - 1;

                    disFromBustoDest += Vector3.Distance(points[i].position, points[next].position);
                }

            }
        }



    }


    public void DrawNavLine(object sender, EventArgs e)
    {
        //disFromBustoDest = 0.0f;

        lineRenderer.material.SetColor("_Color", Color.red);
        lineRenderer.startWidth = 5f;
        lineRenderer.endWidth = 5f;


        if (IsForward) //정방향
        {
            int num = 0;
            //set the position
            if (busStopIndexinIndex <= marcusIndex)
            {
                lineRenderer.positionCount = marcusIndex - busStopIndexinIndex + 1;

                for (int i = busStopIndexinIndex; i < marcusIndex + 1; i++)
                {
                    lineRenderer.SetPosition(num++, points[i].position);

                }
            }
            else //지나쳤을 경우
            {
                lineRenderer.positionCount = marcusIndex + (points.Length - busStopIndexinIndex) + 1;

                for (int i = busStopIndexinIndex; i < points.Length; i++)
                {
                    lineRenderer.SetPosition(num++, points[i].position);
                    
                }

                for (int i = 0; i < marcusIndex + 1; i++)
                {
                    lineRenderer.SetPosition(num++, points[i].position);

                }

            }
        }
        else // 역방
        {
            int num = 0;
            //set the position
            if (busStopIndexinIndex <= marcusIndex) 
            {
                lineRenderer.positionCount = busStopIndexinIndex + 1 + (points.Length - marcusIndex);
                for (int i = busStopIndexinIndex; i > 0; i--)
                {
                    lineRenderer.SetPosition(num++, points[i].position);       
                }
                lineRenderer.SetPosition(num++, points[0].position);

                for (int i = points.Length - 1; i >= marcusIndex; i--)
                {
                    lineRenderer.SetPosition(num++, points[i].position);
                }
 
            }
            else 
            {
                lineRenderer.positionCount = busStopIndexinIndex - marcusIndex + 1;
                for (int i = busStopIndexinIndex; i >= marcusIndex; i--)
                {
                    lineRenderer.SetPosition(num++, points[i].position);
                }

            }
        }


    }

    
}
