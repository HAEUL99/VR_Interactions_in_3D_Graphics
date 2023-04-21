using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

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
    }



}
