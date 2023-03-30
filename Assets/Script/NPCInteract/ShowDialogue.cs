using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class BusTutorialStartEvntArgs : EventArgs
{

}

public class ShowDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public MomNPC_Nav momnpc_Nav;
    public string[] lines;
    private float textSpeed = 0.05f;
    public static int index;
    public GameObject dialogueUi;
    public bool IsSentenseFinished;

    //vr interaction
    public InputAction leftThum, rightThum, leftPri, rightPri;
    public int VrInput;

    //bus tutorial
    public event EventHandler BusTutorialStartEvnt;
    private bool Ischeck;


    private void Start()
    {
        lines = new string[13];
        IsSentenseFinished = false;
        dialogueUi.SetActive(false);
        momnpc_Nav.StartDialogueEvnt += new EventHandler(StartDialogueEvntSender);
        dialogueText.text = string.Empty;

    }


    private void StartDialogueEvntSender(object sender, EventArgs e)
    {
        lines[0] = PlayerNick.Nickname + ", Welcome to the vr world!" ;
        lines[1] = "You can take a bus from here to other places,";
        lines[2] = "and do vr interaction with a few objects";
        lines[3] = "I'll let you know how to operate the controller now";
        //Check if the player try or not 
        lines[4] = "You can rotate your view with the left thumbstick. Try";
        lines[5] = "you can move with the right thumbstick.";
        lines[6] = "And push the left x button ";
        lines[7] = "Push the right a button";
        lines[8] = "We are going to try getting on and off the bus";
        lines[9] = "";
        lines[10] = "Finally, there are things in the house that you can interact with.";
        lines[11] = "Give it a shot";
        lines[12] = "When you're ready, talk to me again!";
        dialogueUi.SetActive(true);
        index = 0;
        StartCoroutine(TypeLineFirst());
    }

    IEnumerator TypeLineFirst()
    {
        dialogueText.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        NextLine();
    }

    IEnumerator TypeLineAfterFirst()
    {
        IsSentenseFinished = false;
        yield return new WaitForSeconds(3f);
        dialogueText.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        IsSentenseFinished = true;
 
    }

    public void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLineAfterFirst());
            
        }
    }

    private void Update()
    {
 
        if (IsSentenseFinished)
        {
            VRInputCheck();
            VRInputAndIndexCheck();
            
        }
        
    }

    void VRInputCheck()
    {
        if (leftThum.triggered || Input.GetKeyDown(KeyCode.A))
            VrInput = 1;
        if (rightThum.triggered || Input.GetKeyDown(KeyCode.B))
            VrInput = 2;
        if (leftPri.triggered || Input.GetKeyDown(KeyCode.C))
            VrInput = 3;
        if (rightPri.triggered || Input.GetKeyDown(KeyCode.D))
            VrInput = 4;
    }

    void VRInputAndIndexCheck()
    {
        if (index == 4)
        {
            if (VrInput == 1)
                NextLine();
        }
        else if (index == 5)
        {
            if (VrInput == 2)
                NextLine();
        }
        else if (index == 6)
        {
            if (VrInput == 3)
                NextLine();
        }
        else if (index == 7)
        {
            if (VrInput == 4)
                NextLine();
        }
        //bus getting on off tutorial
        else if (index == 9)
        {
            if (Ischeck == false)
            {

                BusTutorialStartEvntArgs arg = new BusTutorialStartEvntArgs
                {

                };

                this.BusTutorialStartEvnt(this, arg);
                Ischeck = true;
            }


        }
        else
        {
        
            NextLine();
        }
                  
            
    }



}
