using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrandOkbtn : MonoBehaviour
{
    public event EventHandler DialogueOkEvnt;


    public class DialogueOkEvntArgs : EventArgs
    {
       
    }

    private void Start()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(PressOkbtn);
    }

    public void PressOkbtn()
    {

        DialogueOkEvntArgs arg = new DialogueOkEvntArgs { };
        this.DialogueOkEvnt(this, arg);
        gameObject.transform.parent.gameObject.SetActive(false);
    }


}
