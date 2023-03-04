using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class EnterDestEvntArgs : EventArgs
{
    public string Currentword;
    public int CurrentIndex;
}

public class MiniMapKeyBoard : MonoBehaviour
{
    string word = null;
    int wordIndex = 0;
    public TMP_InputField inputfield;
    public event EventHandler EnterDestEvnt;

    public Transform buttonParent;
    private Button[] keybtn;

    private void Start()
    {
        keybtn = new Button[buttonParent.childCount];
        int i = 0;
        foreach (Transform btn in buttonParent)
        {
            keybtn[i] = btn.GetComponent<Button>();
            i++;
        }

        for (int j = 0; j < 26; j++)
        {
            string alpha = keybtn[j].GetComponentInChildren<TMP_Text>().text;
            keybtn[j].onClick.AddListener(delegate { InputFunc(alpha); });
        }

        //delete key
        keybtn[26].onClick.AddListener(DeleteFunc);

        //space key
        keybtn[27].onClick.AddListener(delegate { InputFunc(" "); });

        //search key
        keybtn[28].onClick.AddListener(SearchFunc);


    }


    public void InputFunc(string alpha)
    {
        wordIndex++;
        word = word + alpha;
        inputfield.text = word;

        EnterDestEvntArgs arg = new EnterDestEvntArgs
        {
            CurrentIndex = wordIndex,
            Currentword = word
        };

        this.EnterDestEvnt(this, arg);

    }



    public void DeleteFunc()
    {
        if (wordIndex > 0)
        {
            inputfield.text = inputfield.text.Substring(0, wordIndex - 1);
            word = inputfield.text;
            wordIndex--;
        }

        EnterDestEvntArgs arg = new EnterDestEvntArgs
        {
            CurrentIndex = wordIndex,
            Currentword = word
        };

        this.EnterDestEvnt(this, arg);
    }

    public void SpaceFunc()
    {
        wordIndex++;
        word = word + " ";
        inputfield.text = word;
    }




    public void SearchFunc()
    {
        Debug.Log($"word: {word}");
    }
}  
