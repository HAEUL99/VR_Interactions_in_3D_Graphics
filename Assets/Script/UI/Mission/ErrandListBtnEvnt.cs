using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ErrandListBtnEvnt : MonoBehaviour
{
    [SerializeField]
    private Button churchBtn;
    [SerializeField]
    private Button deliveryBtn;
    [SerializeField]
    private Button marketBtn;
    [SerializeField]
    private TMP_Text comment;
    [SerializeField]
    private TMP_Text location;
    [SerializeField]
    private GameObject image;

    private Sprite churchImg, deliveryImg, marketImg;

    private void Start()
    {
        image.SetActive(false);
        churchBtn.onClick.AddListener(churchBtnClicked);
        deliveryBtn.onClick.AddListener(deliveryBtnClicked);
        marketBtn.onClick.AddListener(marketBtnClicked);

        churchImg = Resources.Load<Sprite>("church");
        deliveryImg = Resources.Load<Sprite>("delivery");
        marketImg = Resources.Load<Sprite>("market");
    }

    void churchBtnClicked()
    {
        comment.text = "Light the church candles, water the plants!";
        location.text = $"Location: New Life Church";
        image.SetActive(true);
        image.GetComponent<Image>().sprite = churchImg;
    }

    void deliveryBtnClicked()
    {
        comment.text = "Load the boxes spilled on the floor onto the delivery truck.";
        location.text = $"Location: Rache Bookkeeping";
        image.SetActive(true);
        image.GetComponent<Image>().sprite = deliveryImg;
    }

    void marketBtnClicked()
    {
        comment.text = "Your brother wants the doll in the claw machine. Pick that doll for your brother";
        location.text = $"Location: Marcus Market";
        image.SetActive(true);
        image.GetComponent<Image>().sprite = marketImg;
    }
}
