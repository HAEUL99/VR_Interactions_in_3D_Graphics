using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpendoorAnim : MonoBehaviour
{
    public ErrandOkbtn errandOkbtn;
    private Animator animator;

    private void Start()
    {
        errandOkbtn.DialogueOkEvnt += new EventHandler(StartAnim);
        animator = gameObject.GetComponent<Animator>();
    }

    private void StartAnim(object sender, EventArgs e)
    {
        animator.SetBool("IsCheck", true);
    }


}
