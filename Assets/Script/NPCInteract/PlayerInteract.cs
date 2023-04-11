using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteract : MonoBehaviour
{
    private float interactRange = 2f;
    private bool isClicked;

    public ShowDialogue showdialogue;
    public bool isWaiting;

    private void Start()
    {
        showdialogue.VRInteractionTutorialEvnt += new EventHandler(ShowInteractUI);
    }

   

    private void ShowInteractUI(object sender, EventArgs e)
    {
        isWaiting = true;
    }
}
