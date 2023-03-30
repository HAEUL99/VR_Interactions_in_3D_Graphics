using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class ShowDialogueBus : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    private float textSpeed = 0.05f;
    public string[] lines;
    private int index;

    private void Start()
    {
        //lines = new string[2];
        dialogueText = gameObject.GetComponent<TextMeshProUGUI>();
        //lines[0] = "If you get close to the bus, you can get on the bus!";
        //lines[1] = "If you want to get off, push the button in the bus";/
        dialogueText.text = string.Empty;
        index = 0;
        StartCoroutine(ShowtheDialogue());
        
    }


    IEnumerator ShowtheDialogue()
    {

        while (true)
        {
            dialogueText.text = "";
            //yield return null;
            foreach (char c in lines[index].ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }

            index = (index + 1) % 2;

            yield return new WaitForSeconds(4f);
        }

    }

}
