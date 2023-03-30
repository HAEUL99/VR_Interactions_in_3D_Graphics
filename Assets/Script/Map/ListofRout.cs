using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class ClickedRecBtnEvntArgs : EventArgs
{
    public int idx;
}


public class ListofRout : MonoBehaviour
{
    //event receive
    public ListofDest listofDest;

    //receive value
    public DrawLine drawLine;
    public Navigation nav;

    private Transform myPos;
    private Transform destPos;

    //UI
    public GameObject keyboard;
    private RectTransform keyboardRect;
    public GameObject EnterInit;
    private GameObject InitMap;
    public GameObject TransRec;

    [SerializeField]
    private Button backBtn;

    [SerializeField]
    private TMP_Text firstDist;
    [SerializeField]
    private TMP_Text secondDist;
    [SerializeField]
    private TMP_Text detailfirstDist;
    [SerializeField]
    private TMP_Text detailsecondDist;
    [SerializeField]
    private GameObject busImg;
    [SerializeField]
    private GameObject arrowImg;
    [SerializeField]
    private GameObject arrowImg1;
    [SerializeField]
    private GameObject walkingImg1;
    [SerializeField]
    private GameObject detailbusImg;
    [SerializeField]
    private GameObject detwalkingImg1;
    [SerializeField]
    private GameObject detarrowImg;
    [SerializeField]
    private GameObject detarrowImg1;
    [SerializeField]
    private Button recommendedBtn;
    [SerializeField]
    private GameObject destNameObj;




    public event EventHandler ClickedRecomBtnEvnt;

    void Start()
    {

        keyboardRect = keyboard.GetComponent<RectTransform>();


        InitMap = GameObject.Find("InitMap");
        
        listofDest.ClickCorrectDestEvnt += new EventHandler(ShowUpUI);
        listofDest.ClickCorrectDestEvnt += new EventHandler(nav.FindNearPoint);
        listofDest.ClickCorrectDestEvnt += new EventHandler(ShowUpButton);
        backBtn.onClick.AddListener(ClickedBackBtn);

      
        recommendedBtn.onClick.AddListener(ClickedRecommededBtn);
    }

    [Obsolete]
    void ShowUpUI(object sender, EventArgs e)
    {
        //키보드 원위치
        Vector2 Pos1 = new Vector2(0, -486);
        keyboardRect.DOAnchorPos(Pos1, 0.1f);
        //키보드 끄기
     
        //EnterInit 끄고, InitMap 켜기
        EnterInit.SetActive(false);
        InitMap.SetActive(true);

        
        ClickCorrectDestEvntArgs args = e as ClickCorrectDestEvntArgs;
        string destname = args.Destname;

        InitMap.GetComponentInChildren<TMP_InputField>().text = destname;
        //ui show up
        Vector2 Pos = new Vector2(0, 0);
        gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos, 0.5f);

        //set my pos and destination 
        myPos = gameObject.GetComponent<Transform>().FindChild("MyPos");
        myPos.GetComponent<TMP_InputField>().text = "Your location";

        destPos = gameObject.GetComponent<Transform>().FindChild("DestPos");
        destPos.GetComponent<TMP_InputField>().text = $"{destname}";

        //destNameObj.GetComponent<TMP_Text>().text = $"{destname}";


    }

    void ShowUpButton(object sender, EventArgs e)
    {
        if (drawLine.disFromBustoDest < 1f) 
        {
            busImg.SetActive(false);
            arrowImg.SetActive(false);
            arrowImg1.SetActive(false);
            walkingImg1.SetActive(false);
            //detail
            detailbusImg.SetActive(false);
            detwalkingImg1.SetActive(false);
            detarrowImg.SetActive(false);
            detarrowImg1.SetActive(false);
        }


        // 나누기 16해서 몫만
        if ((drawLine.disFromPlayertoPoint / 16) == 0)
        {
            firstDist.text = "1";
            detailfirstDist.text = "1";
        }
        else
        {
            firstDist.text = ((int)drawLine.disFromPlayertoPoint / 16).ToString();
            detailfirstDist.text = ((int)drawLine.disFromPlayertoPoint / 16).ToString();
        }
            
        secondDist.text = ((int)drawLine.disFromBusStoptoDest / 16).ToString();
        detailsecondDist.text = ((int)drawLine.disFromBusStoptoDest / 16).ToString();

    }

    void ClickedRecommededBtn()
    {
        Vector2 Pos1 = new Vector2(514, 0);

        TransRec.GetComponent<RectTransform>().DOAnchorPos(Pos1, 0.5f);
        ClickedRecBtnEvntArgs arg = new ClickedRecBtnEvntArgs
        {
            idx = 0
        };
 
        ClickedRecomBtnEvnt(this, arg);
        //nav.FindNearPoint();

    }

    void ClickedBackBtn()
    {
        EnterInit.SetActive(true);
        Vector2 Pos = new Vector2(514, 0);
        gameObject.GetComponent<RectTransform>().DOAnchorPos(Pos, 0.5f);
        Vector2 Pos1 = new Vector2(0, -208);
        keyboardRect.DOAnchorPos(Pos1, 0.5f);


    }
}
