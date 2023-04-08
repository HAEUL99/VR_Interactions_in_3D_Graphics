using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraControl : MonoBehaviour
{
    public GameObject camera;

    public GameObject[] positions;
    public KeyBoardInput keyBoardinput;
    public NPCInteractable npcInteractable;

    public void Start()
    {

        keyBoardinput.CompleteEnterNick += new EventHandler(ChangeScene);
        npcInteractable.FinishTutorialEvnt += new EventHandler(ChangeSecene1);
    }

    /*
    private void ChangeScene(object sender, EventArgs e)
    {
        camera.SetActive(false);
        camera1.SetActive(true);
    }

    private void ChangeSecene1(object sender, EventArgs e)
    {
        camera1.SetActive(false);
        camera2.SetActive(true);
    }
    */
    
    private void ChangeScene(object sender, EventArgs e)
    {
        camera.transform.position = positions[0].transform.position;
        camera.transform.rotation = Quaternion.Euler(0, 180, 0);
    }

    private void ChangeSecene1(object sender, EventArgs e)
    {

        camera.transform.position = positions[1].transform.position;
        camera.transform.rotation = Quaternion.Euler(0, 9, 0);
        //camera.transform.rotation = Quaternion.Euler(positions[1].transform.rotation.x, positions[1].transform.rotation.y, positions[1].transform.rotation.z);
    }
    
}
