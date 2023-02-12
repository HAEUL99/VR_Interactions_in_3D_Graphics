using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour
{
    public GameObject camera;
    public GameObject[] positions;
    public KeyBoardInput keyBoardinput;
    public ErrandOkbtn errandOkbtn;
    
    public void Start()
    {
    

        keyBoardinput.CompleteEnterNick += new EventHandler(ChangeScene);
        errandOkbtn.DialogueOkEvnt += new EventHandler(ChangeSecene1);
    }

    private void ChangeScene(object sender, EventArgs e)
    {
        camera.transform.position = positions[0].transform.position;
        camera.transform.rotation = Quaternion.Euler(positions[0].transform.rotation.x, positions[0].transform.rotation.y, positions[0].transform.rotation.z);
    }

    private void ChangeSecene1(object sender, EventArgs e)
    {
        camera.transform.position = positions[1].transform.position;
        camera.transform.rotation = Quaternion.Euler(positions[1].transform.rotation.x, positions[1].transform.rotation.y, positions[1].transform.rotation.z);
    }
}
