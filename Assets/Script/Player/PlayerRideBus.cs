using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRideBus : MonoBehaviour
{

    public BusCollider busCollider;


    //확인후 private으로 바꾸기 
    private Transform bus;
    private GameObject playerSeat;
    //public GameObject busRideUI;
    public bool IsClicked;
    public bool IsGetOff;
    public Transform Bus1_BusStopParent;
    private Transform[] BusStopSpawnPos;
    


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
        busCollider.ArrivedNearBusStopEvnt += new EventHandler(CheckGetOff);
        BusStopSpawnPos = new Transform[Bus1_BusStopParent.childCount];
       
        for (int i = 0; i < 9; i++)
        {
            BusStopSpawnPos[i] = Bus1_BusStopParent.GetChild(i).GetChild(0);
            
        }
    }

    private void Update()
    {

        if (IsClicked)
        {
            //playerPlacement
            gameObject.transform.SetParent(bus, false);
            gameObject.transform.position = playerSeat.transform.position;

        }

        
    }


    private void ShowUI(object sender, EventArgs e)
    {
        PlayerEnterBusEvntArgs arg = e as PlayerEnterBusEvntArgs;
        playerSeat = arg.bus.transform.parent.transform.Find("PlayerSeat").gameObject;
        bus = arg.bus.transform.parent;
        //show the Ui
        //busRideUI.SetActive(true);
        //Button yes = busRideUI.GetComponentInChildren<Button>();
        IsClicked = true;

        //yes.onClick.AddListener(delegate { PlayerPlacement(arg.bus.transform.parent.gameObject); });


    }

    public void PlayerPlacement(GameObject bus)
    {
        //playerPlacement
        GameObject playerSeat = bus.transform.Find("PlayerSeat").gameObject;
        gameObject.transform.SetParent(gameObject.transform.parent, true);
        gameObject.transform.position = playerSeat.transform.localPosition;
    }

    private void CheckGetOff(object sender, EventArgs e)
    {
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
        IsGetOff = false;
        IsClicked = false;

    }
}
