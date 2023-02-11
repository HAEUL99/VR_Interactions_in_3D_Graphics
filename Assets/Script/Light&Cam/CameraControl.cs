using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour
{
    public GameObject[] cameras;
    public KeyBoardInput keyBoardinput;
    public ErrandOkbtn errandOkbtn;
    
    public void Start()
    {
        cameras[0].SetActive(true);
        cameras[1].SetActive(false);
        cameras[2].SetActive(false);

        keyBoardinput.CompleteEnterNick += new EventHandler(ChangeScene);
        errandOkbtn.DialogueOkEvnt += new EventHandler(ChangeSecene1);
    }

    private void ChangeScene(object sender, EventArgs e)
    {
        cameras[2].SetActive(false);
        cameras[1].SetActive(true);
        cameras[0].SetActive(false);
    }

    private void ChangeSecene1(object sender, EventArgs e)
    {
        cameras[2].SetActive(true);
        cameras[1].SetActive(false);
        cameras[0].SetActive(false);
    }
}
