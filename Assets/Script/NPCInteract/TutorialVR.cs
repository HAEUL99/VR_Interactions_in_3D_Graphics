using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialVR : MonoBehaviour
{
    public ShowDialogue showDialogue;
    public GameObject dialogueImg;
    public TextMeshProUGUI dialogueText;

    // Start is called before the first frame update
    void Start()
    {
        showDialogue.VRInteractionTutorialEvnt += new EventHandler(vrInteractionTutorial);
    }

    private void vrInteractionTutorial(object sender, EventArgs e)
    {

        dialogueImg.SetActive(false);
        dialogueText.text = string.Empty;


    }
}
