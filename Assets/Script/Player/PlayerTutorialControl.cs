using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR.Interaction.Toolkit;

public class PlayerTutorialControl : MonoBehaviour
{

    public ShowDialogue showDialogue; //
    public GameObject listUI;
    public GameObject mapUI;

    private bool IsOpenListUI;
    private bool IsOpenMapUI;
    private bool IsEnableUI;

    public enum PlayerStatus
    {
        CantMoveRotate,
        LimitedMove, // 움직일 수 있음 
        LimitedRotate, // 회전할 수 있음
        CanMoveRotate

    }

   

    public PlayerStatus playerStatus;
    ActionBasedContinuousMoveProvider continuousMoveProvider;
    ActionBasedContinuousTurnProvider continuousTurnProvider;



    private void Start()
    {
        continuousMoveProvider = gameObject.GetComponent<ActionBasedContinuousMoveProvider>();
        continuousTurnProvider = gameObject.GetComponent<ActionBasedContinuousTurnProvider>();
        playerStatus = PlayerStatus.CantMoveRotate;
        showDialogue.VRInteractionMoveEvnt += new EventHandler(ControlMove);
        showDialogue.VRInteractionTurnEvnt += new EventHandler(ControlTurn);
        showDialogue.VRInteractionListEvnt += new EventHandler(ControlListUI);
        showDialogue.VRInteractionMinimapEvnt += new EventHandler(ControlMapUI);
        showDialogue.BusTutorialStartEvnt += new EventHandler(EnableMoveandTurn);
        showDialogue.BusTutorialStartEvnt += new EventHandler(EnableUI);
        showDialogue.ComebacktoHomeAfterBusEvnt += new EventHandler(DisableMoveandTurn);
        showDialogue.ComebacktoHomeAfterBusEvnt += new EventHandler(DisableUI);
        showDialogue.VRInteractionTutorialEvnt += new EventHandler(EnableMoveandTurn);
        showDialogue.VRInteractionTutorialEvnt += new EventHandler(EnableUI);

        continuousMoveProvider.enabled = false;
        continuousTurnProvider.enabled = false;

        IsOpenListUI = false;
        IsOpenMapUI = false;
        IsEnableUI = false;
        
        listUI.SetActive(false);
        mapUI.SetActive(false);

    }

    private void ControlMove(object sender, EventArgs e)
    {
        VRInteractionMoveEvntArgs arg = e as VRInteractionMoveEvntArgs;

        if (!arg.isBefore2)
        {
           
            continuousMoveProvider.enabled = false;
        }
        else
        {
            continuousMoveProvider.enabled = true;
        }


    }

    private void ControlTurn(object sender, EventArgs e)
    {
        VRInteractionTurnEvntArgs arg = e as VRInteractionTurnEvntArgs;
        if (!arg.isBefore3)
        {
            continuousTurnProvider.enabled = false;
        }
        else
        {
            continuousTurnProvider.enabled = true;
        }

    }



    private void ControlListUI(object sender, EventArgs e)
    {
        VRInteractionListEvntArgs arg = e as VRInteractionListEvntArgs;
        if (!arg.isBefore1)
        {
            listUI.SetActive(false);
        }
        else
        {
            listUI.SetActive(true);
        }

    }


    private void ControlMapUI(object sender, EventArgs e)
    {
        VRInteractionMinimapEvntArgs arg = e as VRInteractionMinimapEvntArgs;
        if (!arg.isBefore)
        {
            mapUI.SetActive(false);
        }
        else
        {

            mapUI.SetActive(true);
        }
    }

    private void EnableMoveandTurn(object sender, EventArgs e)
    {
        continuousMoveProvider.enabled = true;
        continuousTurnProvider.enabled = true;
    }

    private void DisableMoveandTurn(object sender, EventArgs e)
    {
        continuousMoveProvider.enabled = false;
        continuousTurnProvider.enabled = false;
    }

    private void EnableUI(object sender, EventArgs e)
    {
        IsEnableUI = true;
    }

    private void DisableUI(object sender, EventArgs e)
    {
        IsEnableUI = false;
    }

    public void Update()
    {
        if (IsEnableUI)
        {
            if (showDialogue.leftPri.triggered || Input.GetKeyDown(KeyCode.C) )
            {
                IsOpenListUI = !IsOpenListUI;
                listUI.SetActive(IsOpenListUI);
            }
            if (showDialogue.rightPri.triggered || Input.GetKeyDown(KeyCode.D))
            {
                IsOpenMapUI = !IsOpenMapUI;
                mapUI.SetActive(IsOpenMapUI);

            }
        }



         
    }



}
