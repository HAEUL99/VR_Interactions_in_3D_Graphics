using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClickCorrectDestEvntArgs : EventArgs
{
    public string Destname;
}


public class ListofDest : MonoBehaviour
{
    //event receive
    public MiniMapKeyBoard mapKeyboard;
    public ControlMapUI controlMapUI;

    //event send
    public event EventHandler ClickCorrectDestEvnt;


    private string[] DestLists =
    {
        "Arethusa al Tavolo", "Barrique Venice", "Bones", "Cafe Provence", "chi SPACCA",
        "Del Posto", "Gabriel Kreuther", "Gramercy Tavern", "Heirloom Cafe", "Joe's Seafood",
        "Kokkari Estiatorio", "LArtusi", "Le Vallauris", "The Modern", "Oriole", "Polo Lounge",
        "Rasika", "RL Restaurant", "The Saddle River Inn", "Sotto", "Stonehous", "The Metro",
        "Uchi - Dallas", "Vetri Cucina", "Yvonne's", "Zahav", "Zero Restaurant", "Bavette's",
        "BONDST", "Charleston", "Coppa", "Double Knot", "Geronimo", "The Grill", "Hersh's",
        "JUNGSIK", "L'Auberge", "Le Bilboquet", "Linwoods", "Marea", "Momofuku Ko", "Parc",
        "Market Restaurant", "Mistral", "Neighborhood", "Marcus Market"
    };

    private string[] DestListsLower;

    public GameObject buttonParent;
    public GameObject[] buttons;

    private int buttonNum;


    private void Start()
    {
        

        mapKeyboard.EnterDestEvnt += new EventHandler(SearchingName);
        controlMapUI.ShowEnterInitEvnt += new EventHandler(Init);
    }

    void Init(object sender, EventArgs e)
    {

       
        buttonNum = buttonParent.GetComponent<Transform>().childCount;

        for (int j = 0; j < buttonNum; j++)
        {
            buttons[j].SetActive(false);

        }
        DestListsLower = new string[DestLists.Length];
        int i = 0;
        foreach (string str in DestLists)
        {
            DestListsLower[i++] = str.ToLower();
        }

        
    }

    void SearchingName(object sender, EventArgs e)
    {
        for (int i = 0; i < buttonNum; i++)
        {
            buttons[i].SetActive(false);
            buttons[i].GetComponent<Button>().onClick.RemoveListener(CorrectDestClick);
            buttons[i].GetComponent<Button>().onClick.RemoveListener(WrongDestClick);
        }
        EnterDestEvntArgs args = e as EnterDestEvntArgs;
        string currentWord = args.Currentword;
        int currentIndex = args.CurrentIndex;

        //search
        string[] DestFindValue = Array.FindAll(DestListsLower, element => element.Substring(0, (element.Length > currentIndex)? currentIndex: element.Length) == $"{currentWord}");

      
        int numOfDest = (DestFindValue.Length > 5) ? 5 : DestFindValue.Length;
        for (int j = 0; j < numOfDest; j++)
        {
            buttons[j].SetActive(true);
            buttons[j].GetComponentInChildren<TMP_Text>().text = DestFindValue[j];
            string alpha = DestFindValue[j];

            if (string.Equals($"{DestFindValue[j]}", "marcus market"))
                buttons[j].GetComponent<Button>().onClick.AddListener(CorrectDestClick);
            else
                buttons[j].GetComponent<Button>().onClick.AddListener(WrongDestClick);

        }
    }

    void CorrectDestClick()
    {
        ClickCorrectDestEvntArgs arg = new ClickCorrectDestEvntArgs
        {
            Destname = "marcus market"
        };
        { Debug.Log($"correct"); }
        this.ClickCorrectDestEvnt(this, arg);
    }



    void WrongDestClick() { Debug.Log($"wrong"); }





}
