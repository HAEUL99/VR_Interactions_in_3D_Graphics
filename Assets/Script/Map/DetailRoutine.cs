using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DetailRoutine : MonoBehaviour
{
    // receive event
    public ListofRout listofRout;

    //send event
    public Navigation nav;

    public GameObject wholeCam;
    public Transform selfCam;
    private Button expandBtn;
    public bool IsExpand;


    public Button[] buttons;
    private Transform player;

    // 수정필수
    public Transform destPos;

    private Vector3 playerPos;
    private Vector3 departBusStopPos;
    private Vector3 arriveBusStopPos;

    // Start is called before the first frame update
    void Start()
    {
        IsExpand = false;

        player = GameObject.Find("Player").GetComponent<Transform>();
        listofRout.ClickedRecomBtnEvnt += new EventHandler(ShowUpUI);
        nav.reDrawLineEvt += new EventHandler(CalCameraPos);
        expandBtn = GameObject.Find("ExpandBtn").GetComponent<Button>();
        expandBtn.onClick.AddListener(MoveUpUI);
        buttons[0].onClick.AddListener(ToPlayerPos);
        buttons[1].onClick.AddListener(ToBusStop);
        buttons[2].onClick.AddListener(ToDest);

    }

    void ShowUpUI(object sender, EventArgs e)
    {

        //지도 전체 비추는 카메라로 전환
        wholeCam.SetActive(true);
        //세부 설명 ui 올라오기
        Vector2 Pos = new Vector2(0, -485);
        gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos, 0.5f);
    }

    void MoveUpUI()
    {
        if (IsExpand)
        {
            //세부 설명 ui 내리
            Vector2 Pos1 = new Vector2(0, -485);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
            IsExpand = !IsExpand;
        }
        else
        {
            //세부 설명 ui 올라오기
            Vector2 Pos = new Vector2(0, -47);
            gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos, 0.5f);
            IsExpand = !IsExpand;
        }
        
    }

    void CalCameraPos(object sender, EventArgs e)
    {
        reDrawLineEvtArgs arg = e as reDrawLineEvtArgs;
        playerPos = player.position;
        departBusStopPos = arg.nearBusStopPos;
        arriveBusStopPos = destPos.position ;



    }

    void ToPlayerPos()
    {
        wholeCam.SetActive(false);
        selfCam.position = new Vector3((playerPos.x + departBusStopPos.x) / 2, 30, (playerPos.z + departBusStopPos.z) / 2);
        selfCam.GetComponent<Camera>().orthographicSize = 88;
        Vector2 Pos1 = new Vector2(0, -485);
        gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
        IsExpand = !IsExpand;
    }

    void ToBusStop()
    {
        wholeCam.SetActive(false);

        selfCam.position = new Vector3((arriveBusStopPos.x + departBusStopPos.x) / 2, 30, (arriveBusStopPos.z + departBusStopPos.z) / 2);
        //수정필요
        selfCam.GetComponent<Camera>().orthographicSize = 230;
        Vector2 Pos1 = new Vector2(0, -485);
        gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
        IsExpand = !IsExpand;
    }

    void ToDest()
    {
        wholeCam.SetActive(false);

        selfCam.position = new Vector3((arriveBusStopPos.x), 30, (arriveBusStopPos.z));
        selfCam.GetComponent<Camera>().orthographicSize = 88;
        Vector2 Pos1 = new Vector2(0, -485);
        gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
        IsExpand = !IsExpand;
    }


}
