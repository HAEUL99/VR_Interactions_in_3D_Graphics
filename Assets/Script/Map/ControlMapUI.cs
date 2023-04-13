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
    public GameObject InitMapobj, EnterInitDesobj, Keyboardobj, NormalMapobj, DetailRoutobj, DetailRoutobjwalk;
    private RectTransform InitMap, EnterInitDes, NormalMap, Keyboard, DetailRout, DetailRoutwalk;
    public TMP_InputField Init_InputField;
    public TMP_Text time;
    public event EventHandler ShowEnterInitEvnt;
    public GameObject wholeCam;
    public  Vector2 keyBoardPos;
    public bool IsCheck;

    // Start is called before the first frame update
    void Awake()
    {

        InitMap = InitMapobj.GetComponent<RectTransform>();
        EnterInitDes = EnterInitDesobj.GetComponent<RectTransform>();
        Keyboard = Keyboardobj.GetComponent<RectTransform>();
        DetailRout = DetailRoutobj.GetComponent<RectTransform>();
        DetailRoutwalk = DetailRoutobjwalk.GetComponent<RectTransform>();
        keyBoardPos = Keyboard.position;
        wholeCam.SetActive(false);
        Keyboardobj.SetActive(false);

        EnterInitDesobj.SetActive(false);



    }

    // Update is called once per frame
    void Update()
    {
        if (Init_InputField.isFocused)
        {
            //IsCheck = true;
            EnterInit_InputField();
            
        }

        TimeSetting();
    }

    void EnterInit_InputField()
    {
        //두번째 목적지 입력시, 이전 ui 리셋
        Vector2 Pos1 = new Vector2(0, -610);
        DetailRout.DOAnchorPos(Pos1, 0.2f);
        DetailRoutwalk.DOAnchorPos(Pos1, 0.2f);

        EnterInitDesobj.SetActive(true);

        
        ShowEnterInitEvntArgs arg1 = new ShowEnterInitEvntArgs
        {
            
        };
        this.ShowEnterInitEvnt(this, arg1);
        
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
