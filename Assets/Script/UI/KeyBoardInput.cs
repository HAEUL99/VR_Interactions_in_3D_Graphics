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

    public Transform buttonParent;
    private Button[] keybtn;

    public bool nextButton;

    private void Start()
    {
        keybtn = new Button[buttonParent.childCount];
        nextButton = false;
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

        //check key
        keybtn[27].onClick.AddListener(CheckFunc);


    }

    private void Update()
    {
        if (nextButton == true)
        {
            CheckTest();
        }
    }


    public void InputFunc(string alpha)
    {
        wordIndex++;
        word = word + alpha;
        inputfield.text = word;
        Debug.Log($"alpha: {alpha}");
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

    public void CheckTest()
    {
        PlayerNick.Nickname = "haeul";


        CompleteEnterNickArgs arg = new CompleteEnterNickArgs
        {
            Nickname = word
        };

        this.CompleteEnterNick(this, arg);
        gameObject.SetActive(false);
    }

}
