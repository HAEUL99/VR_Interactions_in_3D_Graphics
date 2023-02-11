using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyBoardInput : MonoBehaviour
{
    string word = null;
    int wordIndex = 0;
    public TMP_InputField inputfield;
    public event EventHandler CompleteEnterNick;


    public void InputFunc(string alpha)
    {
        wordIndex++;
        word = word + alpha;
        inputfield.text = word;
    }

    public void DeleteFunc()
    {
        if (wordIndex > 0)
        {
            inputfield.text = inputfield.text.Substring(0, wordIndex - 1);
            word = inputfield.text;
            wordIndex--;
        }
    }

    public class CompleteEnterNickArgs : EventArgs
    {
        public string Nickname;
    }

    public void CheckFunc()
    {
        if (wordIndex == 0)
            return;
        PlayerNick.Nickname = word;


        CompleteEnterNickArgs arg = new CompleteEnterNickArgs
        {
            Nickname = word
        };

        this.CompleteEnterNick(this, arg);
        gameObject.SetActive(false);

    }


}
