using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MissionManager : MonoBehaviour
{
    public checkBoxes _checkBoxes;
    public checkCandles _checkCandles;
    public checkPlants _checkPlants;
    public checkPrizes _checkPrizes;

    
    // Start is called before the first frame update
    void Start()
    {
        _checkBoxes.deliveryMissionCompl += new EventHandler(ListUISet);
    }

    private void ListUISet(object sender, EventArgs e)
    {
        if (sender.GetType().ToString().Equals("checkBoxes"))
        {
            ActivateBoxMission();
        }
        if (sender.GetType().ToString().Equals("checkCandles"))
        {

        }
        if (sender.GetType().ToString().Equals("checkPlants"))
        {

        }
        if (sender.GetType().ToString().Equals("checkPrizes"))
        {

        }

    }

    private void ActivateBoxMission()
    {
        FixedPlayerPosition();
    }

    private void FixedPlayerPosition()
    {

    }
}
