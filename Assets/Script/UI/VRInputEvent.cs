using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class VRInputEvent : MonoBehaviour
{
    public ShowDialogue showdialogue;
    public GameObject listUI;
    public GameObject mapUI;

    private bool IsOpenListUI;
    private bool IsOpenMapUI;
    void Start() 
    {
        showdialogue.VRInteractionListEvnt += new EventHandler(ControlListUI);
        showdialogue.VRInteractionMinimapEvnt += new EventHandler(ControlMapUI);

        IsOpenListUI = false;
        IsOpenMapUI = false;
        listUI.SetActive(false);
        mapUI.SetActive(false);
        
    }

    private void ControlListUI(object sender, EventArgs e)
    {
        if (IsOpenListUI == false) 
        {
            IsOpenListUI = true;
            listUI.SetActive(true);
        }
        else
        {
            IsOpenListUI = false;
            listUI.SetActive(false);
        }

    }


    private void ControlMapUI(object sender, EventArgs e)
    {
        if (IsOpenMapUI == false)
        {
            IsOpenMapUI = true;
            mapUI.SetActive(true);
        }
        else
        {
            IsOpenMapUI = false;
            mapUI.SetActive(false);
        }

    }

}
