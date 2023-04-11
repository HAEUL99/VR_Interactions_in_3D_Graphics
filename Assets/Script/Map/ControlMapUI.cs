using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System;
using TMPro;

public class ShowEnterInitEvntArgs : EventArgs
{
   
}

public class ControlMapUI : MonoBehaviour
{
    public GameObject InitMapobj, EnterInitDesobj, Keyboardobj, NormalMapobj, DetailRoutobj;
    private RectTransform InitMap, EnterInitDes, NormalMap, Keyboard, DetailRout;
    public TMP_InputField Init_InputField;
    public TMP_Text time;
    public event EventHandler ShowEnterInitEvnt;
    public GameObject wholeCam;
    public  Vector2 keyBoardPos;

    // Start is called before the first frame update
    void Start()
    {

        InitMap = InitMapobj.GetComponent<RectTransform>();
        EnterInitDes = EnterInitDesobj.GetComponent<RectTransform>();
        //NormalMap = NormalMapobj.GetComponent<RectTransform>();
        Keyboard = Keyboardobj.GetComponent<RectTransform>();
        DetailRout = DetailRoutobj.GetComponent<RectTransform>();
        keyBoardPos = Keyboard.position;
        wholeCam.SetActive(false);
        Keyboardobj.SetActive(false);

        EnterInitDesobj.SetActive(false);
        //NormalMapobj.SetActive(false);
        //InitMap.DOAnchorPos(Vector2.zero, 0.25f);
        //gameObject.SetActive(false);


    }

    // Update is called once per frame
    void Update()
    {

    
  
        if (Init_InputField.isFocused)
        {
            EnterInit_InputField();
        }

        TimeSetting();
    }

    void EnterInit_InputField()
    {
        //두번째 목적지 입력시, 이전 ui 리셋
        Vector2 Pos1 = new Vector2(0, -570);
        DetailRout.DOAnchorPos(Pos1, 0.2f);

        //InitMapobj.SetActive(false);
        EnterInitDesobj.SetActive(true);

        ShowEnterInitEvntArgs arg = new ShowEnterInitEvntArgs
        {
            
        };
        this.ShowEnterInitEvnt(this, arg);

        Keyboardobj.SetActive(true);
        Vector2 Pos = new Vector2(0, -208);
        Keyboard.DOAnchorPos(Pos, 0.5f); 


    }

    void TimeSetting()
    {
        //timeSetting
        time.text = DateTime.Now.ToString(("HH:mm"));
    }


}
