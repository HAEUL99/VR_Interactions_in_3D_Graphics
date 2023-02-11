using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class ShowDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public MomNPC_Nav momnpc_Nav;
    public GameObject Okbtn;
    private string[] lines;
    private float textSpeed = 0.05f;
    private int index;
    public GameObject dialogueUi;

    private void Start()
    {
        lines = new string[2];
        dialogueUi.SetActive(false);
        momnpc_Nav.StartDialogueEvnt += new EventHandler(StartDialogueEvntSender);
        dialogueText.text = string.Empty;
        Okbtn.SetActive(false);

    }


    private void StartDialogueEvntSender(object sender, EventArgs e)
    {
        lines[0] = $"Good Morning, " + PlayerNick.Nickname;
        lines[1] = "Go to convenience store and buy 2 toothpaste, 1 toothbrush, and 1 toilet paper";
        dialogueUi.SetActive(true);
        index = 0;
        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        dialogueText.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        NextLine();
    }

    IEnumerator TypeLineSecond()
    {
        yield return new WaitForSeconds(3f);
        dialogueText.text = string.Empty;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        NextLine();

    }
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLineSecond());
        }
        else
        {
            Okbtn.SetActive(true);
            //gameObject.transform.parent.gameObject.SetActive(false);
        }

    }





}
