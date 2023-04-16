using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WaitingPlayerFinishTutorialArgs : EventArgs { }
public class MomNPC_Nav : CharacterNavigatorController
{
    public KeyBoardInput keyBoardinput;
    public WaypointNavigator waypointNavigator;
    private bool IsCheck = false;
    private bool IsEvntSend = false;

    public event EventHandler StartDialogueEvnt;
    public event EventHandler WaitingPlayerFinishTutorialEvnt;
    StartDialogueArgs arg;

    public ShowDialogue showDialogue;
    public GameObject Mom;



    public override void Init()
    {
        movementSpeed = 2f;
        rotationSpeed = 60;
        stopDistance = 1f;
    }

    public void Start()
    {
        keyBoardinput.CompleteEnterNick += new EventHandler(StartAnimation);
        waypointNavigator.CurrentWaypointNull += new EventHandler(StopAnimation);
        animator = GetComponent<Animator>();
   
        waypointNavigator.CurrentWaypointNull1 += new EventHandler(WaitingAnimation);
        showDialogue.VRInteractionTutorialEvnt += new EventHandler(WalkingAnimation);


    }

    private void StartAnimation(object sender, EventArgs e)
    {
        Init();
        animator.Play("walking");
        IsCheck = true;
    }

    private void StopAnimation(object sender, EventArgs e)
    {
        IsCheck = false;
        animator.SetBool("IsStop", true);
    }

    public class StartDialogueArgs : EventArgs
    {

    }

    public void WalkingAnimation(object sender, EventArgs e)
    {
        animator.SetBool("IsStop", false);
        animator.SetBool("IsTalk", false);
        animator.SetBool("IsWalk", true);
        IsCheck = true;
    }

    public void WaitingAnimation(object sender, EventArgs e)
    {
        animator.SetBool("IsWaiting", true);
        animator.SetBool("IsWalk", false);
        IsCheck = false;
        Mom.transform.Rotate(0.0f, 180.0f, 0.0f);
        WaitingPlayerFinishTutorialArgs arg = new WaitingPlayerFinishTutorialArgs { };
        this.WaitingPlayerFinishTutorialEvnt(this, arg);




    }

    public void Update()
    {
        if (IsCheck == true)
        {
            Movement(this.gameObject);
        }
   
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Stopwalking") &&
   animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && IsEvntSend == false)
        {
            animator.SetBool("IsTalk", true);
            this.StartDialogueEvnt(this, arg);
            IsEvntSend = true;
        }

        

    }
}
