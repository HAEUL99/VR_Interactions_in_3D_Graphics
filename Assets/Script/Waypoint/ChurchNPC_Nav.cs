using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChurchNPC_Nav : CharacterNavigatorController
{
    public Animator anim;
    private WaypointNavigator waypointNavigator;
    private bool IsEvntSend;
    public GameObject DialogueUI;

    public string[] lines;
    public TextMeshProUGUI dialText;


    //sound
    public int maxVisibleCharacters;
    public DialogueAudioInfoSO currentAudioInfo;
    private NPCDialogueSound npcDialogueSound;

    public override void Init()
    {
        movementSpeed = 1f;
        rotationSpeed = 120;
        stopDistance = 1f;
    }


    private void Start()
    {
        anim = GetComponent<Animator>();
        waypointNavigator = gameObject.GetComponent<WaypointNavigator>();
        Init();
        DialogueUI.SetActive(false);


        npcDialogueSound = gameObject.AddComponent<NPCDialogueSound>();
        npcDialogueSound.Init(gameObject, currentAudioInfo, dialText, lines);

        gameObject.SetActive(false);
    }


    private void Update()
    {


        if (waypointNavigator.currentWaypoint.name == "Waypoint 3" || waypointNavigator.currentWaypoint.name == "Waypoint 7")
        {
            anim.SetBool("IsStop", true);
        }
        else
        {
            Movement(this.gameObject);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stopwalking") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f && IsEvntSend == false)
        {
            //anim.SetBool("IsTalk", true);
            IsEvntSend = true;
            DialogueUI.SetActive(true);
            npcDialogueSound.StartTyping();
            Invoke("ChangeWaypoint", 4f);

        }


    }

    private void ChangeWaypoint()
    {
        waypointNavigator.currentWaypoint = waypointNavigator.currentWaypoint.nextWaypoint;
        anim.SetBool("IsStop", false);
    }


}
