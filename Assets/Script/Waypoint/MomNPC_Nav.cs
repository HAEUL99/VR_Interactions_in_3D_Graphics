using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MomNPC_Nav : CharacterNavigatorController
{
    public KeyBoardInput keyBoardinput;
    public WaypointNavigator waypointNavigator;
    private bool IsCheck = false;
    private bool IsEvntSend = false;

    public event EventHandler StartDialogueEvnt;
    StartDialogueArgs arg;

    public override void Init()
    {
        movementSpeed = 2f;
        rotationSpeed = 60;
        stopDistance = 1f;
    }

    public void Start()
    {
        keyBoardinput.CompleteEnterNick += new EventHandler(StartAnimation);
        this.gameObject.GetComponent<WaypointNavigator>().CurrentWaypointNull += new EventHandler(StopAnimation);
        animator = GetComponent<Animator>();
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
