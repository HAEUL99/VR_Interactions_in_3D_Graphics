using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;


public class BusTutorialStartEvntArgs : EventArgs { }
public class VRInteractionTutorialEvntArgs : EventArgs { }
public class VRInteractionMinimapEvntArgs : EventArgs { public bool isBefore; }
public class VRInteractionListEvntArgs : EventArgs { public bool isBefore1; }
public class VRInteractionMoveEvntArgs : EventArgs { public bool isBefore2; }
public class VRInteractionTurnEvntArgs : EventArgs { public bool isBefore3; }
public class ComebacktoHomeAfterBusEvntArgs : EventArgs { }
public class ShowDialogue : MonoBehaviour
{
    public TextMeshProUGUI dialogueText;
    public MomNPC_Nav momnpc_Nav;
    public string[] lines;
    private float textSpeed = 0.05f;
    public static int index;
    public GameObject dialogueUi;
    public bool IsSentenseFinished;

    //vr interaction
    public InputAction leftThum, rightThum, leftPri, rightPri;
    public int VrInput;
    public event EventHandler VRInteractionTutorialEvnt;
    public bool Ischecked, Ischecked1;
    public event EventHandler VRInteractionMoveEvnt; //leftThum
    public event EventHandler VRInteractionTurnEvnt; //rightThum
    public event EventHandler VRInteractionMinimapEvnt; //rightPri
    public event EventHandler VRInteractionListEvnt; //leftPri
    public event EventHandler ComebacktoHomeAfterBusEvnt;
    public bool Isonce, Isonce1, Isonce2, Isonce3;

    //bus tutorial
    public event EventHandler BusTutorialStartEvnt;
    private bool Ischeck;

    //sound
    public int maxVisibleCharacters;
    public DialogueAudioInfoSO currentAudioInfo;
    private AudioSource audioSource;

    

    private void Start()
    {
        lines = new string[13];
        IsSentenseFinished = false;
        dialogueUi.SetActive(false);
        momnpc_Nav.StartDialogueEvnt += new EventHandler(StartDialogueEvntSender);
        dialogueText.text = string.Empty;
        audioSource = this.gameObject.AddComponent<AudioSource>();
        

        //Input
        leftThum.Enable();
        rightThum.Enable();
        leftPri.Enable();
        rightPri.Enable();

    }


    private void StartDialogueEvntSender(object sender, EventArgs e)
    {
        lines[0] = PlayerNick.Nickname + ", Welcome to the vr world!" ;
        lines[1] = "You can take a bus from here to other places,";
        lines[2] = "and do vr interaction with a few objects";
        lines[3] = "I'll let you know how to control the controller now";
        //Check if the player try or not 
        lines[4] = "You can move with the left thumbstick. Try";
        lines[5] = "you can turn with the right thumbstick.";
        lines[6] = "And push the left x button. It is a todo list";
        lines[7] = "Push the right a button. It is a minimap.";
        lines[8] = "We are going to try getting on and off the bus";
        lines[9] = "";
        lines[10] = "There are things in the house that you can interact with.";
        lines[11] = "Give it a shot";
        lines[12] = "When you're ready, talk to me again!";
        dialogueUi.SetActive(true);
        //index = 0;
        index = 10;
        StartCoroutine(TypeLineFirst());
    }

    IEnumerator TypeLineFirst()
    {
        dialogueText.text = string.Empty;
        maxVisibleCharacters = 0;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;

            PlayDialogueSound(maxVisibleCharacters, dialogueText.text[maxVisibleCharacters]);
            maxVisibleCharacters++;

            yield return new WaitForSeconds(textSpeed);
        }
        NextLine();
    }

    IEnumerator TypeLineAfterFirst()
    {
        IsSentenseFinished = false;
        yield return new WaitForSeconds(1f);
        dialogueText.text = string.Empty;
        maxVisibleCharacters = 0;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            
            PlayDialogueSound(maxVisibleCharacters, dialogueText.text[maxVisibleCharacters]);
            maxVisibleCharacters++;
            yield return new WaitForSeconds(textSpeed);
        }
        IsSentenseFinished = true;
 
    }

    private void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
    {
        // set variables for the below based on our config
        AudioClip[] dialogueTypingSoundClips = currentAudioInfo.dialogueTypingSoundClips;
        int frequencyLevel = currentAudioInfo.frequencyLevel;
        float minPitch = currentAudioInfo.minPitch;
        float maxPitch = currentAudioInfo.maxPitch;
        bool stopAudioSource = currentAudioInfo.stopAudioSource;

        // play the sound based on the config
        if (currentDisplayedCharacterCount % frequencyLevel == 0)
        {
            if (stopAudioSource)
            {
                audioSource.Stop();
            }
            AudioClip soundClip = null;
            // create predictable audio from hashing
            int hashCode = currentCharacter.GetHashCode();
            // sound clip
            int predictableIndex = hashCode % dialogueTypingSoundClips.Length;
            soundClip = dialogueTypingSoundClips[predictableIndex];
            // pitch
            int minPitchInt = (int)(minPitch * 100);
            int maxPitchInt = (int)(maxPitch * 100);
            int pitchRangeInt = maxPitchInt - minPitchInt;
            // cannot divide by 0, so if there is no range then skip the selection
            if (pitchRangeInt != 0)
            {
                int predictablePitchInt = (hashCode % pitchRangeInt) + minPitchInt;
                float predictablePitch = predictablePitchInt / 100f;
                audioSource.pitch = predictablePitch;
            }
            else
            {
                audioSource.pitch = minPitch;
            }

            // play sound
            audioSource.PlayOneShot(soundClip);
        }
    }

    public void NextLine()
    {
        //mapUi.SetActive(false);
        //errandListUi.SetActive(false);
        if (index < lines.Length - 1)
        {
            index++;
            StartCoroutine(TypeLineAfterFirst());
            
        }
    }

    private void Update()
    {
 
        if (IsSentenseFinished)
        {
            VRInputCheck();

            
        }
        
    }

    void VRInputCheck()
    {
        if (index == 4)
        {

            if ((leftThum.triggered || Input.GetKeyDown(KeyCode.A)) && Isonce == false )
            {
                Isonce = true;
                VrInput = 1;
                StartCoroutine(ActionandNextDialogue(1));

            }

        }
        else if (index == 5)
        {
            if((rightThum.triggered || Input.GetKeyDown(KeyCode.B)) && Isonce1 == false)
            {
                Isonce1 = true;
                VrInput = 2;
                StartCoroutine(ActionandNextDialogue(2));

            }

        }
        else if (index == 6)
        {
            if((leftPri.triggered || Input.GetKeyDown(KeyCode.C)) && Isonce2 == false) //list
            {
                Isonce2 = true;
                VrInput = 3;
                StartCoroutine(ActionandNextDialogue(3));
                //VRInteractionListEvntArgs arg = new VRInteractionListEvntArgs { };
                //this.VRInteractionListEvnt(this, arg);

            }
        }
        else if (index == 7)
        {
            if((rightPri.triggered || Input.GetKeyDown(KeyCode.D)) && Isonce3 == false) //minimap
            {
                Isonce3 = true;
                VrInput = 4;
                StartCoroutine(ActionandNextDialogue(4));
                //VRInteractionMinimapEvntArgs arg1 = new VRInteractionMinimapEvntArgs { };
                //this.VRInteractionMinimapEvnt(this, arg1);

            }
        }

        else if (index == 9)
        {
            if (Ischeck == false)
            {

                BusTutorialStartEvntArgs arg = new BusTutorialStartEvntArgs
                { };

                this.BusTutorialStartEvnt(this, arg);
                Ischeck = true;


            }


        }

        else
        {
            if (index == 12 && Ischecked == false)
            {

                Ischecked = true;
                VRInteractionTutorialEvntArgs arg = new VRInteractionTutorialEvntArgs
                { };

                this.VRInteractionTutorialEvnt(this, arg);



            }
            if (index == 10 && Ischecked1 == false)
            {
                Ischecked1 = true;
                ComebacktoHomeAfterBusEvntArgs arg = new ComebacktoHomeAfterBusEvntArgs { };
                this.ComebacktoHomeAfterBusEvnt(this, arg);


            }

            NextLine();

        }

    }

    IEnumerator ActionandNextDialogue(int num)
    {

        switch (num)
        {
            case 1: //move
                VRInteractionMoveEvntArgs arg = new VRInteractionMoveEvntArgs { };
                arg.isBefore2 = true;
                this.VRInteractionMoveEvnt(this, arg);     
                break;
            case 2:
                VRInteractionTurnEvntArgs arg1= new VRInteractionTurnEvntArgs { };
                arg1.isBefore3 = true;
                this.VRInteractionTurnEvnt(this, arg1);  
                break;
            case 3:
                VRInteractionListEvntArgs arg2 = new VRInteractionListEvntArgs { };
                arg2.isBefore1 = true;
                this.VRInteractionListEvnt(this, arg2);     
                break;
            case 4:
                VRInteractionMinimapEvntArgs arg3 = new VRInteractionMinimapEvntArgs { };
                arg3.isBefore = true;
                this.VRInteractionMinimapEvnt(this, arg3);
                break;

        }
            
        yield return new WaitForSeconds(3.0f);

        switch (num)
        {
            case 1: //move
                VRInteractionMoveEvntArgs arg = new VRInteractionMoveEvntArgs { };
                arg.isBefore2 = false;
                this.VRInteractionMoveEvnt(this, arg);
                break;
            case 2:
                VRInteractionTurnEvntArgs arg1 = new VRInteractionTurnEvntArgs { };
                arg1.isBefore3 = false;
                this.VRInteractionTurnEvnt(this, arg1);
                break;
            case 3:
                VRInteractionListEvntArgs arg2 = new VRInteractionListEvntArgs { };
                arg2.isBefore1 = false;
                this.VRInteractionListEvnt(this, arg2);
                break;
            case 4:
                VRInteractionMinimapEvntArgs arg3 = new VRInteractionMinimapEvntArgs { };
                arg3.isBefore = false;
                this.VRInteractionMinimapEvnt(this, arg3);
                break;

        }

        NextLine();


    }

    



}
