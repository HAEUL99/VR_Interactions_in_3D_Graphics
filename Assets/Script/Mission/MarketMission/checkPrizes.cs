using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkPrizes : MonoBehaviour
{
    public bool isGet;

    public void SetUp()
    {
        isGet = true;
        CompletePrizeMission();
    }

    private void CompletePrizeMission()
    {
        Debug.Log("prize");
    }

}
