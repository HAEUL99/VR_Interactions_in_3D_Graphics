using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using TMPro;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    public checkBoxes _checkBoxes;
    public checkCandles _checkCandles;
    public checkPlants _checkPlants;
    public checkPrizes _checkPrizes;
    public int missionNum;

    //delivery Mission
    public Animator truckAnim;
    public GameObject girl;
    public VehicleTruck_Nav vehicleTruck_Nav;
    public GameObject[] Buses;
    public GameObject truck;
    public GameObject[] playerPos;
    private GameObject player;
    public Vector3 originalPos;
    public Quaternion originalRot;

    //church Mission
    public bool[] churchMissionList;
    public GameObject churchNpc;

    //market Mission
    public GameObject marketNpc;

    //fade effect
    public FadeScreen fadeScreen;
    public PlayerControl playerControl;
    public float deliveryanimDuration = 7f;
    public float churchanimDuration = 10f;
    public float marketanimDuration = 10f;

    //UI
    public TextMeshProUGUI deliverydoneCheck;
    public TextMeshProUGUI churchdoneCheck;
    public TextMeshProUGUI marketdoneCheck;
    public Image deliveryImg;
    public Image churchImg;
    public Image marketImg;
    public Color doneColor;



    // Start is called before the first frame update
    void Start()
    {
        _checkBoxes.deliveryMissionCompl += new EventHandler(ListUISet);
        _checkCandles.churchCandleMissionCompl += new EventHandler(ListUISet);
        _checkPlants.churchPlantMissionCompl += new EventHandler(ListUISet);
        player = GameObject.FindWithTag("Player");
        churchMissionList = new bool[] { false, false };

        

    }

    private void ListUISet(object sender, EventArgs e)
    {
        if (sender.GetType().ToString().Equals("checkBoxes"))
        {
            ActivateBoxMission();
            
        }
        if (sender.GetType().ToString().Equals("checkCandles"))
        {
            churchMissionList[0] = true;
            ActivatechurchMission();
        }
        if (sender.GetType().ToString().Equals("checkPlants"))
        {
            churchMissionList[1] = true;
            ActivatechurchMission();
        }
        if (sender.GetType().ToString().Equals("checkPrizes"))
        {
            ActivatemarketMission();
        }

    }

    

    public void ActivateBoxMission()
    {
        
        truckAnim.SetBool("IsClosed", true);
        girl.SetActive(false);
        vehicleTruck_Nav.isEvent = true;
        missionNum = 0;
        StartCoroutine(BusDisableEnable());
        StartCoroutine(AnimationEffect(missionNum));
        SetUI(missionNum);
    }

    public void ActivatechurchMission()
    {
        foreach (bool missionArr in churchMissionList)
        {
            if (!missionArr)
                return;
        }

        churchNpc.SetActive(true);
        churchNpc.GetComponent<Animator>().Play("Running");
        missionNum = 1;
        StartCoroutine(AnimationEffect(missionNum));
        StartCoroutine(DisableChurchNPC());
        SetUI(missionNum);

    }

    public void ActivatemarketMission()
    {
        
        marketNpc.SetActive(true);
        marketNpc.GetComponent<Animator>().Play("Running");
        missionNum = 2;
        StartCoroutine(AnimationEffect(missionNum));
        StartCoroutine(DisableMarketNPC());
        SetUI(missionNum);
    }

    IEnumerator DisableChurchNPC()
    {
        yield return new WaitForSeconds(churchanimDuration);
        churchNpc.SetActive(false);
    }

    IEnumerator DisableMarketNPC()
    {
        yield return new WaitForSeconds(marketanimDuration);
        marketNpc.SetActive(false);
    }

    IEnumerator AnimationEffect(int missionNum)
    {

        
        playerControl.DisableMoveandTurn();
        //페이드인 
        fadeScreen.FadeOut(1f);
        //1초후
        yield return new WaitForSeconds(1f);
        fadeScreen.FadeIn(1f);
        
        //플레이어 위치 변경
        FixedPlayerPosition(missionNum);
        //animation 진행
        if(missionNum == 0)
            yield return new WaitForSeconds(deliveryanimDuration - 1.0f);
        if (missionNum == 1)
            yield return new WaitForSeconds(churchanimDuration - 1.0f);
        if (missionNum == 2)
            yield return new WaitForSeconds(marketanimDuration - 1.0f);

        fadeScreen.FadeOut(1f);
        yield return new WaitForSeconds(1f);
        // 자리 리셋
        ReStartPlayerPosition();
        fadeScreen.FadeIn(1f);
        playerControl.EnableMoveandTurn();
       

    }

    IEnumerator BusDisableEnable()
    {
        for (int i = 0; i < Buses.Length; i++)
        {
            Buses[i].SetActive(false);
        }

        yield return new WaitForSeconds(deliveryanimDuration);

        for (int i = 0; i < Buses.Length; i++)
        {
            Buses[i].SetActive(true);
        }
        truck.SetActive(false);

    }

    private void FixedPlayerPosition(int missionNum)
    {
        originalPos = player.transform.position;
        originalRot = player.transform.rotation;
 
        
        player.transform.position = playerPos[missionNum].transform.position;
        if(missionNum == 0)
            player.transform.rotation = Quaternion.Euler(new Vector3(0, -110.0f, 0));
        if(missionNum == 1)
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        if (missionNum == 2)
            player.transform.rotation = Quaternion.Euler(new Vector3(0, 80, 0));

    }

    private void ReStartPlayerPosition()
    {

        player.transform.position = originalPos;
        player.transform.rotation = originalRot;
    }

    private void SetUI(int missionNum)
    {
        if (missionNum == 0)
        {
            deliveryImg.color = doneColor;
            deliverydoneCheck.color = doneColor;
        }
        if (missionNum == 1)
        {
            churchImg.color = doneColor;
            churchdoneCheck.color = doneColor;
        }
        if (missionNum == 2)
        {
            marketImg.color = doneColor;
            marketdoneCheck.color = doneColor;
        }
    }
}
