using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.XR.Content.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class BusGetOffEvntArgs : EventArgs
{
    public bool IsPlayerPushBtn;
}


public class PlayerRideBus : MonoBehaviour
{

    public BusCollider busCollider;
    public GameObject pushButton;

    private Transform bus;
    private GameObject playerSeat;
    //public GameObject busRideUI;
    public bool IsClicked;
    public bool IsGetOff;
    public Transform Bus1_BusStopParent;
    private Transform[] BusStopSpawnPos;

    //tutorial
    public bool IsPlayerPushBtn;
    public bool IsTutorial;
    public event EventHandler BusGetOffEvnt;
    public GameObject Mom1;
    public GameObject Mom2;
    public GameObject Mom2Text;

    private bool isonce;

    enum BusStopName
    {
        Forest,
        Harmon,
        Sears,
        Savannah,
        Hollywood,
        Winona,
        Custer,
        Boston,
        Tennyson

    }


    // Start is called before the first frame update
    void Start()
    {
        IsClicked = false;
 
        //busRideUI.SetActive(false);
        busCollider.PlayerEnterBusEvnt += new EventHandler(ShowUI);
        if (IsTutorial)
        {
            //busCollider.PlayerEnterBusEvnt += new EventHandler(GameObjectSetting);
            Mom1.SetActive(false);
            Mom2.SetActive(false);
        }
        busCollider.PlayerEnterBusEvnt += new EventHandler(GameObjectSetting);
        busCollider.ArrivedNearBusStopEvnt += new EventHandler(CheckGetOff);
        BusStopSpawnPos = new Transform[Bus1_BusStopParent.childCount];


        for (int i = 0; i < 9; i++)
        {
            BusStopSpawnPos[i] = Bus1_BusStopParent.GetChild(i).GetChild(0);
            
        }

        //button pushed event
        UnityEvent pressEvnt = pushButton.GetComponent<XRPushButton>().m_OnPress;
        pressEvnt.AddListener(GetOffBus);
    }

    private void Update()
    {

        if (IsClicked)
        {
            //playerPlacement
            gameObject.transform.SetParent(bus, false);
            gameObject.transform.position = playerSeat.transform.position;
            if (isonce == false)
            {
                isonce = true;
                gameObject.transform.rotation = Quaternion.Euler(0, 90, 0);
            }


        }

        
    }

    void GetOffBus()
    {
        IsGetOff = true;
        IsPlayerPushBtn = true;
    }

    private void GameObjectSetting(object sender, EventArgs e)
    {
        if (!IsTutorial)
            return;
        Mom1.SetActive(false);
        Mom2.SetActive(true);
        Mom2Text.GetComponent<ShowDialogueBus>().Init();
    }

    private void ShowUI(object sender, EventArgs e)
    {
        PlayerEnterBusEvntArgs arg = e as PlayerEnterBusEvntArgs;
        playerSeat = arg.bus.transform.parent.transform.Find("PlayerSeat").gameObject;
        bus = arg.bus.transform.parent;
        IsClicked = true;
        

    }


    private void CheckGetOff(object sender, EventArgs e)
    {

        if (IsTutorial && IsClicked)
        {
            
            BusGetOffEvntArgs args = new BusGetOffEvntArgs
            {
                //Destname = "marcus market"
                IsPlayerPushBtn = IsPlayerPushBtn

            };

            this.BusGetOffEvnt(this, args);
            IsGetOff = true;

        }

        if (!IsGetOff)
        {
            return;
        }


        ArrivedNearBusStopEvntArgs arg = e as ArrivedNearBusStopEvntArgs;
        string currentBusStop = arg.currentBusStopName;
        BusStopName currentBusStopName = (BusStopName)Enum.Parse(typeof(BusStopName), $"{currentBusStop}");
        int currentBusStopnInt = (int)currentBusStopName;

        gameObject.transform.SetParent(null);
        gameObject.transform.position = BusStopSpawnPos[currentBusStopnInt].position;
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        IsGetOff = false;
        IsClicked = false;
        Mom2.SetActive(false);






    }
}
