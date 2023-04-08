using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpendoorAnim : MonoBehaviour
{
    public NPCInteractable npcInteractable;
    private Animator animator;

    private void Start()
    {
        npcInteractable.FinishTutorialEvnt += new EventHandler(StartAnim);
        animator = gameObject.GetComponent<Animator>();
    }

    private void StartAnim(object sender, EventArgs e)
    {
        animator.SetBool("IsCheck", true);
    }


}
