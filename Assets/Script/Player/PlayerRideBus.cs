using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRideBus : MonoBehaviour
{
    public GameObject playerRideEffect;
    public BusCollider busCollider;


    //확인후 private으로 바꾸기 
    private Transform bus;
    private GameObject playerSeat;
    public GameObject busRideUI;
    public bool IsClicked;
    public bool IsGetOff;

    // Start is called before the first frame update
    void Start()
    {
        IsClicked = false;
        busRideUI.SetActive(false);
        busCollider.PlayerEnterBusEvnt += new EventHandler(ShowUI);

    }

    private void Update()
    {

        if (IsClicked)
        {
            //playerPlacement
            gameObject.transform.SetParent(bus, false);
            gameObject.transform.position = playerSeat.transform.position;

        }

        if (IsGetOff)
        {
            gameObject.transform.SetParent(null);
            gameObject.transform.position = bus.position + new Vector3(5.0f, 0.0f, 0.0f);
        }
    }


    private void ShowUI(object sender, EventArgs e)
    {
        PlayerEnterBusEvntArgs arg = e as PlayerEnterBusEvntArgs;
        playerSeat = arg.bus.transform.parent.transform.Find("PlayerSeat").gameObject;
        bus = arg.bus.transform.parent;
        //show the Ui
        busRideUI.SetActive(true);
        Button yes = busRideUI.GetComponentInChildren<Button>();
        IsClicked = true;

        //yes.onClick.AddListener(delegate { PlayerPlacement(arg.bus.transform.parent.gameObject); });


    }

    public void PlayerPlacement(GameObject bus)
    {
        //playerPlacement
        GameObject playerSeat = bus.transform.Find("PlayerSeat").gameObject;
        //gameObject.transform.parent = gameObject.transform.parent;
        gameObject.transform.SetParent(gameObject.transform.parent, true);
        gameObject.transform.position = playerSeat.transform.localPosition;
    }
}
