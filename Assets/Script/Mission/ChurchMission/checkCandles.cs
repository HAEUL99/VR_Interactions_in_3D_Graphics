using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using EnumManager;

public class churchCandleMissionComplArgs : EventArgs
{
    public MissionType _missionType;
}

public class checkCandles : MonoBehaviour
{

    private struct Candles
    {
        public Candles(GameObject candle, bool isOn)
        {
            Candle = candle;
            IsOn = isOn;
        }

        public GameObject Candle { get; }
        public bool IsOn { get; }
    }


    [SerializeField]
    private GameObject[] candleObjs;

    private Candles[] candles;

    public event EventHandler churchCandleMissionCompl;


    public void Start()
    {
        candles = new Candles[candleObjs.Length];

        for(int i = 0; i< candleObjs.Length; i++)
        {
            candles[i] = new Candles(candleObjs[i], false);
        }
    }

    public void SetUp(int numOfcandl)
    {

        candles[numOfcandl] = new Candles(candleObjs[numOfcandl], true);

        int count = 0;
        for (int i = 0; i < candleObjs.Length; i++)
        {
            if (candles[i].IsOn)
                count++;

        }

        if (count == candleObjs.Length)
        {
            CompletecCandleMission();
            
        }
          



    }

    private void CompletecCandleMission()
    {
        Debug.Log("candle");
        churchCandleMissionComplArgs arg = new churchCandleMissionComplArgs
        {
            _missionType = EnumManager.MissionType.candles
        };
        this.churchCandleMissionCompl(this, arg);

    }



}
