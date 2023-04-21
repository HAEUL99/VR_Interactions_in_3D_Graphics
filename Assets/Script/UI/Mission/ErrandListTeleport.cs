using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ErrandListTeleport : MonoBehaviour
{
    public Button churchMission;
    public Button deliveryMission;
    public Button marketMission;

    public GameObject[] pos;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        churchMission.onClick.AddListener(MovetoChurch);
        deliveryMission.onClick.AddListener(MovetoDelivery);
        marketMission.onClick.AddListener(MovetoMarket);
        player = GameObject.FindWithTag("Player");
    }

    void MovetoChurch()
    {
        player.transform.position = pos[0].transform.position;
        player.transform.rotation = Quaternion.Euler(0, -90, 0);

    }

    void MovetoDelivery()
    {
        player.transform.position = pos[1].transform.position;
        player.transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    void MovetoMarket()
    {
        player.transform.position = pos[2].transform.position;
        player.transform.rotation = Quaternion.Euler(0, -90, 0);
    }


}
