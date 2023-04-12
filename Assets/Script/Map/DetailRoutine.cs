using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DetailRoutine : MonoBehaviour
{
    // receive event
    public ListofRout listofRout;
    public ListofDest listofDest;

    //send event
    public Navigation nav;

    public GameObject wholeCam;
    public Transform selfCam;
    private Button expandBtn;
    public bool IsExpand;
    public bool IsExpand1;
    private Button expandBtn_walk;


    public Button[] buttons;
    private Transform player;

    // 수정필수
    public Transform[] destPos;

    private Vector3 playerPos;
    private Vector3 departBusStopPos;
    private Vector3 arriveBusStopPos;

    //거리 text
    public DrawLine drawLine;
    public TMP_Text firstDestTxt;
    public TMP_Text secondDestTxt;
    public TMP_Text ThirdDestTxt;
    public TMP_Text destNameTxt;
    public String destname;

    //only walk
    public GameObject DetailRout_walk;
    public TMP_Text firstDestTxt_walk;
    public TMP_Text destNameTxt_walk;


    // Start is called before the first frame update
    void Start()
    {
        IsExpand = false;
        IsExpand1 = false;

        player = GameObject.Find("Player").GetComponent<Transform>();
        listofRout.ClickedRecomBtnEvnt += new EventHandler(ShowUpUI);
        listofDest.ClickCorrectDestEvnt += new EventHandler(SetDestinationName);
        nav.reDrawLineEvt += new EventHandler(CalCameraPos);
        expandBtn = GameObject.Find("ExpandBtn").GetComponent<Button>();
        expandBtn_walk = GameObject.Find("ExpandBtn_walk").GetComponent<Button>();
        expandBtn.onClick.AddListener(MoveUpUI);
        expandBtn_walk.onClick.AddListener(MoveUpUI);
        buttons[0].onClick.AddListener(ToPlayerPos);
        buttons[1].onClick.AddListener(ToBusStop);
        buttons[2].onClick.AddListener(ToDest);

        


    }

    void SetDestinationName(object sender, EventArgs e)
    {
        ClickCorrectDestEvntArgs arg = e as ClickCorrectDestEvntArgs;
        destname = arg.Destname;


    }

    void ShowUpUI(object sender, EventArgs e)
    {

        //지도 전체 비추는 카메라로 전환
        wholeCam.SetActive(true);
        //세부 설명 ui 올라오기

        if ((int)drawLine.disFromBustoDest < 1)
        {
            Vector2 Pos = new Vector2(0, -555);
            DetailRout_walk.GetComponent<RectTransform>().DOAnchorPos(Pos, 0.5f);

            if (((int)drawLine.disFromPlayertoPoint / 16) == 0)
                firstDestTxt_walk.text = "1 min";
            else
                firstDestTxt_walk.text = $"{((int)drawLine.disFromPlayertoPoint / 16).ToString()} min";

            destNameTxt_walk.text = $"{destname}";

        }
        else
        {
            Vector2 Pos = new Vector2(0, -555);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos, 0.5f);
            //set the distance
            if(((int)drawLine.disFromPlayertoPoint / 16) == 0)
                firstDestTxt.text = "1 min";
            else
                firstDestTxt.text = $"{((int)drawLine.disFromPlayertoPoint / 16).ToString()} min";

            if (((int)drawLine.disFromBustoDest / 60) == 0)
                secondDestTxt.text = "1 min";
            else
                secondDestTxt.text = $"{((int)drawLine.disFromBustoDest / 60).ToString()} min";

            if (((int)drawLine.disFromBusStoptoDest / 16) == 0)
                ThirdDestTxt.text = "1 min";
            else
                ThirdDestTxt.text = $"{((int)drawLine.disFromBusStoptoDest / 16).ToString()} min";

            destNameTxt.text = $"{destname}";

        }

        


    }

    void MoveUpUI()
    {
        if ((int)drawLine.disFromBustoDest < 1)
        {
            if (IsExpand1)
            {
                //세부 설명 ui 내리
                Vector2 Pos1 = new Vector2(0, -555);
                DetailRout_walk.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
                IsExpand1 = !IsExpand1;
            }
            else
            {
                //세부 설명 ui 올라오기
                Vector2 Pos = new Vector2(0, -14);
                DetailRout_walk.GetComponent<RectTransform>().DOAnchorPos(Pos, 0.5f);
                IsExpand1 = !IsExpand1;
            }
        }
        else
        {
            if (IsExpand)
            {
                //세부 설명 ui 내리
                Vector2 Pos1 = new Vector2(0, -555);
                gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
                IsExpand = !IsExpand;
            }
            else
            {
                //세부 설명 ui 올라오기
                Vector2 Pos = new Vector2(0, -14);
                gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos, 0.5f);
                IsExpand = !IsExpand;
            }
        }
        
        
    }

    void CalCameraPos(object sender, EventArgs e)
    {
        reDrawLineEvtArgs arg = e as reDrawLineEvtArgs;
        playerPos = player.position;
        departBusStopPos = arg.nearBusStopPos;
        if (string.Equals($"{destname}", "marcus market"))
            arriveBusStopPos = destPos[0].position;
        if(string.Equals($"{destname}", "new life church"))
            arriveBusStopPos = destPos[1].position;
        if(string.Equals($"{destname}", "rachel bookkeeping"))
            arriveBusStopPos = destPos[2].position;

    }

    void ToPlayerPos()
    {
       
        wholeCam.SetActive(false);
        selfCam.position = new Vector3((playerPos.x + departBusStopPos.x) / 2, 30, (playerPos.z + departBusStopPos.z) / 2);
        selfCam.GetComponent<Camera>().orthographicSize = 88;
        if ((int)drawLine.disFromBustoDest < 1)
        {
            Vector2 Pos1 = new Vector2(0, -555);
            DetailRout_walk.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
            IsExpand = !IsExpand;
        }
        else
        {

            Vector2 Pos1 = new Vector2(0, -555);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
            IsExpand = !IsExpand;
        }
        
    }

    void ToBusStop()
    {
        wholeCam.SetActive(false);

        selfCam.position = new Vector3((arriveBusStopPos.x + departBusStopPos.x) / 2, 30, (arriveBusStopPos.z + departBusStopPos.z) / 2);
        //수정필요
        selfCam.GetComponent<Camera>().orthographicSize = 230;
        if ((int)drawLine.disFromBustoDest < 1)
        {
            Vector2 Pos1 = new Vector2(0, -555);
            DetailRout_walk.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
            IsExpand = !IsExpand;
        }
        else
        {
            Vector2 Pos1 = new Vector2(0, -555);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
            IsExpand = !IsExpand;
        }
        
    }

    void ToDest()
    {
        wholeCam.SetActive(false);

        selfCam.position = new Vector3((arriveBusStopPos.x), 30, (arriveBusStopPos.z));
        selfCam.GetComponent<Camera>().orthographicSize = 88;
        if ((int)drawLine.disFromBustoDest < 1)
        {
            Vector2 Pos1 = new Vector2(0, -555);
            DetailRout_walk.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
            IsExpand = !IsExpand;
        }
        else
        {
            Vector2 Pos1 = new Vector2(0, -555);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
            IsExpand = !IsExpand;
        }
        
    }


}
