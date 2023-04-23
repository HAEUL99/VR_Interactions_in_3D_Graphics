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

    //sound
    public int maxVisibleCharacters;
    public DialogueAudioInfoSO currentAudioInfo;

    public string[] lines;
    public TextMeshProUGUI dialText;
    private NPCDialogueSound nPCDialogueSound;

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


        nPCDialogueSound = gameObject.AddComponent<NPCDialogueSound>();
        nPCDialogueSound.Init(gameObject, currentAudioInfo, dialText, lines);

        gameObject.SetActive(false);
    }


    private void Update()
    {
        Movement(this.gameObject);

        if (waypointNavigator.currentWaypoint.name == "Waypoint 3")
        {
            anim.SetBool("IsStop", true);
        }

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Stopwalking") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.4f && IsEvntSend == false)
        {
            //anim.SetBool("IsTalk", true);
            IsEvntSend = true;
            DialogueUI.SetActive(true);
            nPCDialogueSound.StartTyping();
            
        }
    }


}
