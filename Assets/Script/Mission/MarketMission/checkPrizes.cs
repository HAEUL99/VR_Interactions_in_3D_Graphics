using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumManager;
using UnityEngine.XR.Interaction.Toolkit;
using System.Diagnostics;
using System.Security.Cryptography;

public class marketMissionComplArgs : EventArgs
{
    public MissionType _missionType;
}


public class checkPrizes : MonoBehaviour
{
    public bool isGet;
    public event EventHandler marketMissionCompl;
    public ColliderPrize colliderPrize;
    //public obect rayInteractor;




    public void SetUp()
    {
        //if (TryGetHitInfo)
        if(colliderPrize.isOut == true)
        {
            isGet = true;
            CompletePrizeMission();
        }

    }

    private void CompletePrizeMission()
    {
        UnityEngine.Debug.Log("prize");

        marketMissionComplArgs arg = new marketMissionComplArgs
        {
            _missionType = EnumManager.MissionType.prizes
        };
        this.marketMissionCompl(this, arg);
    }

}
