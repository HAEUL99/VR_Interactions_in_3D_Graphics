using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialBusGetOff : MonoBehaviour
{
    //event receiver
    public ShowDialogue showDialogue;
    public PlayerRideBus playerRidebus;

    //bus tutorial
    public GameObject player;
    public GameObject bus;
    public GameObject loginPanel;
    public GameObject spawnPoint;
    //at the busStop
    public GameObject Mom1;
    //on the bus
    public GameObject Mom2;
    public event EventHandler BusTutorialStartEvnt;
    public event EventHandler PlayerRideBus;

    //dialogue
    public bool playerPushbtn;
    public TextMeshProUGUI dialogueText;
    private float textSpeed = 0.05f;
    public string[] lines;
    private int index;
    public GameObject dialougeImg;

    //spawn to home
    public GameObject spawnPoint1;
    // Start is called before the first frame update
    void Start()
    {
        playerPushbtn = player.GetComponent<PlayerRideBus>();
        showDialogue.BusTutorialStartEvnt += new EventHandler(BusTutorialStart);
        playerRidebus.BusGetOffEvnt += new EventHandler(CheckIfPlayerPushBtn);
        bus.SetActive(false);
        Mom1.SetActive(false);
        dialougeImg.SetActive(false);

    }

    public void BusTutorialStart(object sender, EventArgs e)
    {
        loginPanel.SetActive(false);
        //player move
        player.transform.position = spawnPoint.transform.position;
        //bus move
        bus.SetActive(true);
        //char show
        Mom1.SetActive(true);
        Mom2.SetActive(true);
    }

    public void CheckIfPlayerPushBtn(object sender, EventArgs e)
    {
        dialougeImg.SetActive(true);
        BusGetOffEvntArgs args = e as BusGetOffEvntArgs;
        bool IsPushed = args.IsPlayerPushBtn;
        lines = new string[2];
        
        if (IsPushed)
        {   
            lines[0] = "Good Job. You got off the bus well ";
        }
        else
        {
            lines[0] = "Hmm. Don't forget to press the button when getting off.";
        }
        lines[1] = "Let's go back home!";
        StartCoroutine(ShowtheDialogue());

    }

    IEnumerator ShowtheDialogue()
    {

        for(int i = 0; i < 2; i++)
        {
            dialogueText.text = "";
            //yield return null;
            foreach (char c in lines[index].ToCharArray())
            {
                dialogueText.text += c;
                yield return new WaitForSeconds(textSpeed);
            }

            index = (index + 1) % 2;
            yield return new WaitForSeconds(4f);
        }
        MovetoHome();


    }

    public void MovetoHome()
    {
        player.transform.position = spawnPoint1.transform.position;
        showDialogue.NextLine();
 
    }
}
