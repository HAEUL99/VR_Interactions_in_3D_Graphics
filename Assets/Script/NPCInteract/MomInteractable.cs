using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Content.Rendering;


public class MomInteractable : MonoBehaviour
{
    public NPCInteractable npcInteractable;

    //Event receiver
    public MomNPC_Nav momNPC_Nav;

    public GameObject Mom_before;
    public GameObject Mom_after;

    //ui
    public GameObject InteractUI;

    public bool TestCodeforSceneFadeOut;
    public bool isChecked;


    public void Start()
    {
        //Mom_after.SetActive(false);
        InteractUI.SetActive(false);
        momNPC_Nav.WaitingPlayerFinishTutorialEvnt += new EventHandler(ChangeMomChar);
        //hoverStateVisuals.SetActive(false);
        //keyBoardInput.CompleteEnterNick += new EventHandler(CharMomDisable);


    }

    public void ActivatetheMomDialogue()
    {
        npcInteractable.isInput = true;
    }

    public void ActivatetheUI()
    {
        InteractUI.SetActive(true);
    }

    public void deativatetheUI()
    {
        InteractUI.SetActive(false);
    }



    public void ChangeMomChar(object sender, EventArgs e)
    {
        
        

        GetComponent<Animator>().SetBool("IsWaiting", true);
        GetComponent<Animator>().SetBool("IsWalk", false);

    }



}
