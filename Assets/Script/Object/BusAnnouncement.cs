using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

public class BusAnnouncement : MonoBehaviour
{
    //Event Recieve
    public BusCollider busCollider;

    private int currentBusStopnInt;
    private TMP_Text AnnounceText;
    private RectTransform AnnouRectTrans;
    private Vector2 pos;

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
        busCollider.ArrivedNearBusStopEvnt += new EventHandler(SaveCurrentBusStop);
        AnnounceText = gameObject.GetComponentInChildren<TMP_Text>();
        AnnouRectTrans = AnnounceText.GetComponent<RectTransform>();
        pos = AnnouRectTrans.anchoredPosition;
    }

    private void SaveCurrentBusStop(object sender, EventArgs e)
    {
        ArrivedNearBusStopEvntArgs arg = e as ArrivedNearBusStopEvntArgs;
        string currentBusStop = arg.currentBusStopName;
        BusStopName currentBusStopName = (BusStopName)Enum.Parse(typeof(BusStopName), $"{currentBusStop}");
        currentBusStopnInt = (int)currentBusStopName;
        //ReplacetheNextStop();
        StartCoroutine(ReplacetheNextStop());
    }

    
    IEnumerator ReplacetheNextStop()
    {
        AnnounceText.text = $"This Bus Stop is {(BusStopName)((currentBusStopnInt) % 8)}";
        Vector2 Pos = new Vector2(-1.5f, 0);
        AnnouRectTrans.DOAnchorPos(Pos, 10f);
        yield return new WaitForSeconds(10f);

        AnnounceText.text = $"";
        AnnouRectTrans.anchoredPosition = pos;
        yield return new WaitForSeconds(4f);

        AnnounceText.text = $"This Bus Stop is {(BusStopName)((currentBusStopnInt + 1) % 8)}, Next Bus Stop is {(BusStopName)((currentBusStopnInt + 2) % 8)}";
        Vector2 Pos2 = new Vector2(-1.5f, 0);
        AnnouRectTrans.DOAnchorPos(Pos2, 8f);
        yield return new WaitForSeconds(8f);


        AnnounceText.text = $"";
        AnnouRectTrans.anchoredPosition = pos;
        yield return new WaitForSeconds(2f);
    }
}
