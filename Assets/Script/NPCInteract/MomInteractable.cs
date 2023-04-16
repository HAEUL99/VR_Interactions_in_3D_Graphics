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

    

    public void Start()
    {
        //Mom_after.SetActive(false);

        momNPC_Nav.WaitingPlayerFinishTutorialEvnt += new EventHandler(ChangeMomChar);
        //hoverStateVisuals.SetActive(false);
        //keyBoardInput.CompleteEnterNick += new EventHandler(CharMomDisable);


    }

    public void ActivatetheMomDialogue()
    {
        npcInteractable.isInput = true;
    }

    public void ChangeMomChar(object sender, EventArgs e)
    {
        
        

        GetComponent<Animator>().SetBool("IsWaiting", true);
        GetComponent<Animator>().SetBool("IsWalk", false);

    }



}
