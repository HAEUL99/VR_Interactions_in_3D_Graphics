using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRideBus : MonoBehaviour
{
    public GameObject playerRideEffect;
    public BusCollider busCollider;


    //확인후 private으로 바꾸기 
    private GameObject bus;
    public GameObject playerSeat;

    // Start is called before the first frame update
    void Start()
    {
        busCollider.PlayerEnterBusEvnt += new EventHandler(PlayerPlacement);

    }


    private void PlayerPlacement(object sender, EventArgs e)
    {
        PlayerEnterBusEvntArgs arg = e as PlayerEnterBusEvntArgs;
        // effect 
        //GameObject effectPlayer = (GameObject)Instantiate(playerRideEffect);
        //effectPlayer.transform.position = transform.position;
        //Destroy(effectPlayer);

        //playerPlacement
        bus = arg.bus.transform.parent.gameObject;
        playerSeat = arg.bus.transform.parent.Find("PlayerSeat").gameObject;
        
        gameObject.transform.SetParent(bus.transform, true);
        //Debug.Log($"playerSeat.transform.localPosition.x: {playerSeat.transform.localPosition.x}");
        gameObject.transform.localPosition = playerSeat.transform.localPosition;


        //Debug.Log($"playerSeat.transform.localPosition: {playerSeat.transform.localPosition}");
        //Debug.Log($"gameObject.transform.position: {gameObject.transform.localPosition}");

    }
}
