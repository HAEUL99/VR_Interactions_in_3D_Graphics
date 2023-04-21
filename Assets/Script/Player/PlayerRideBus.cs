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

public class PlayerGetOffBusEvntArgs : EventArgs
{

}



public class PlayerRideBus : MonoBehaviour
{

    public BusCollider[] busCollider;
    public GameObject[] pushButton;

    private Transform bus;
    private GameObject busColliderobj;
    private GameObject playerSeat;
    public bool IsClicked;
    public bool IsGetOff;
    public Transform Bus1_BusStopParent;
    public Transform Bus2_BusStopParent;
    public Transform[] BusStopSpawnPos;
    public Transform[] BusStopSpawnPos1;
    public Transform tutorialgetoffPosition;
    public EventHandler PlayerGetOffBusEvnt;

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
        Tennyson,

    }
    
    // Start is called before the first frame update
    void Start()
    {
        IsClicked = false;



        if (IsTutorial)
        {
            Mom1.SetActive(false);
            Mom2.SetActive(false);
        }
        else
        {
            BusStopSpawnPos = new Transform[Bus1_BusStopParent.childCount];
            BusStopSpawnPos1 = new Transform[Bus2_BusStopParent.childCount];
            for (int i = 0; i < 9; i++)
            {
                BusStopSpawnPos[i] = Bus1_BusStopParent.GetChild(i).GetChild(0);

            }

            for (int i = 0; i < 9; i++)
            {
                BusStopSpawnPos1[i] = Bus2_BusStopParent.GetChild(i).GetChild(0);
            }
        }

        for (int i = 0; i < busCollider.Length; i++)
        {
            busCollider[i].PlayerEnterBusEvnt += new EventHandler(ShowUI);

            busCollider[i].PlayerEnterBusEvnt += new EventHandler(GameObjectSetting);
            busCollider[i].ArrivedNearBusStopEvnt += new EventHandler(CheckGetOff);

        }


        for (int i = 0; i < pushButton.Length; i++)
        {
            //button pushed event
            UnityEvent pressEvnt = pushButton[i].GetComponent<XRPushButton>().m_OnPress;
            pressEvnt.AddListener(GetOffBus);
        }
        
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
        busColliderobj = arg.bus;
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
        GameObject eventBus = arg.bus;
        if (eventBus != busColliderobj)
            return;

        string currentBusStop = arg.currentBusStopName;
        int busdirection = arg.busdirection; // 0: forward, 1:back

        BusStopName currentBusStopName = (BusStopName)Enum.Parse(typeof(BusStopName), $"{currentBusStop}");
        int currentBusStopnInt = (int)currentBusStopName;

        gameObject.transform.SetParent(null);

        PlayerGetOffBusEvntArgs arg1 = new PlayerGetOffBusEvntArgs { };
        this.PlayerGetOffBusEvnt(this, arg1);

        if (!IsTutorial)
        {
            if (busdirection == 0)
            {
                gameObject.transform.position = BusStopSpawnPos[currentBusStopnInt].position;
            }
            else
            {
                gameObject.transform.position = BusStopSpawnPos1[currentBusStopnInt].position;
            }
        }
        else
        {
            gameObject.transform.position = tutorialgetoffPosition.position;
        }
        
        
        
        gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
        IsGetOff = false;
        IsClicked = false;
        IsPlayerPushBtn = false;
        isonce = false;
        if (IsTutorial)
        {
            Mom2.SetActive(false);
        }

    }
}
