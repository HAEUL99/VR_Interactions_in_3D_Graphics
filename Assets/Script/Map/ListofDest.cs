using System;
using DG.Tweening;
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

    public GameObject Keyboard;
    private RectTransform KeyboardRectTrans;
    private MiniMapKeyBoard minimapKeyboard;

    private string[] DestLists =
    {
        "Arethusa al Tavolo", "Barrique Venice", "Bones", "Cafe Provence", "chi SPACCA",
        "Del Posto", "Gabriel Kreuther", "Gramercy Tavern", "Heirloom Cafe", "Joe's Seafood",
        "Kokkari Estiatorio", "LArtusi", "Le Vallauris", "The Modern", "Oriole", "Polo Lounge",
        "Rasika", "RL Restaurant", "The Saddle River Inn", "Sotto", "Stonehous", "The Metro",
        "Uchi - Dallas", "Vetri Cucina", "Yvonne's", "Zahav", "Zero Restaurant", "Bavette's",
        "BONDST", "Charleston", "Coppa", "Double Knot", "Geronimo", "The Grill", "Hersh's",
        "JUNGSIK", "L'Auberge", "Le Bilboquet", "Linwoods", "Marea", "Momofuku Ko", "Parc",
        "Market Restaurant", "Mistral", "Neighborhood", "Marcus Market", "New Life Church", "Rachel Bookkeeping"
    };

    public GameObject[] destArrowImgs;
    private int Imgidx;

    private string[] DestListsLower;

    public GameObject buttonParent;
    public GameObject[] buttons;

    private int buttonNum;
    private Button backButton;
    public string alpha;

    private void Awake()
    {
        mapKeyboard.EnterDestEvnt += new EventHandler(SearchingName);
        controlMapUI.ShowEnterInitEvnt += new EventHandler(Init);
        backButton = gameObject.transform.Find("BackButton").GetComponent<Button>();
        KeyboardRectTrans = Keyboard.GetComponent<RectTransform>();
        minimapKeyboard = Keyboard.GetComponent<MiniMapKeyBoard>();
        backButton.onClick.AddListener(backButtonClicked);
        foreach (GameObject obj in destArrowImgs)
        {
            obj.SetActive(false);
        }

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
            alpha = DestFindValue[j];

            if (string.Equals($"{DestFindValue[j]}", "marcus market") ||
                string.Equals($"{DestFindValue[j]}", "new life church") ||
                string.Equals($"{DestFindValue[j]}", "rachel bookkeeping"))
            {
                buttons[j].GetComponent<Button>().onClick.AddListener(CorrectDestClick);
                switch (DestFindValue[j])
                {
                    case "marcus market":
                        Imgidx = 0;
                        break;
                    case "new life church":
                        Imgidx = 1;
                        break;
                    case "rachel bookkeeping":
                        Imgidx = 2;
                        break;
                }
            }
            else
                buttons[j].GetComponent<Button>().onClick.AddListener(WrongDestClick);
             
        }
    }

    void CorrectDestClick()
    {
        //destination 지도에 표시
        for(int i = 0; i< destArrowImgs.Length; i++)
        {
            if (i == Imgidx)
                destArrowImgs[i].SetActive(true);
            else
                destArrowImgs[i].SetActive(false);

        }
        

        ClickCorrectDestEvntArgs arg = new ClickCorrectDestEvntArgs
        {
            //Destname = "marcus market"
            Destname = alpha
            
        };

        this.ClickCorrectDestEvnt(this, arg);
    }



    void WrongDestClick() { Debug.Log($"wrong"); }


    void backButtonClicked()
    {
        gameObject.SetActive(false);
        Vector2 Pos = new Vector2(0, -486);
        KeyboardRectTrans.DOAnchorPos(Pos, 0.5f);
        minimapKeyboard.DeleteAllFunc();
    }


}
