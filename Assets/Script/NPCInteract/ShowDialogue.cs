using System.Collections;
using UnityEngine;
using TMPro;
using System;
using UnityEngine.InputSystem;


public class BusTutorialStartEvntArgs : EventArgs
{

}

public class VRInteractionTutorialEvntArgs : EventArgs
{

}

public class VRInteractionMinimapEvntArgs : EventArgs { }
public class VRInteractionListEvntArgs : EventArgs { }

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
    public bool Ischecked;
    public event EventHandler VRInteractionMinimapEvnt; //rightPri
    public event EventHandler VRInteractionListEvnt; //leftPri

    //bus tutorial
    public event EventHandler BusTutorialStartEvnt;
    private bool Ischeck;

    //sound
    public int maxVisibleCharacters;
    public DialogueAudioInfoSO currentAudioInfo;
    private AudioSource audioSource;


    //UI


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
        lines[3] = "I'll let you know how to operate the controller now";
        //Check if the player try or not 
        lines[4] = "You can rotate your view with the left thumbstick. Try";
        lines[5] = "you can move with the right thumbstick.";
        lines[6] = "And push the left x button ";
        lines[7] = "Push the right a button";
        lines[8] = "We are going to try getting on and off the bus";
        lines[9] = "";
        lines[10] = "Finally, there are things in the house that you can interact with.";
        lines[11] = "Give it a shot";
        lines[12] = "When you're ready, talk to me again!";
        dialogueUi.SetActive(true);
        index = 0;
        //index = 11;
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
            VRInputAndIndexCheck();
            
        }
        
    }

    void VRInputCheck()
    {
        if (leftThum.triggered || Input.GetKeyDown(KeyCode.A))
        {
            VrInput = 1;
            //mapUi.SetActive(true);
        }
        if (rightThum.triggered || Input.GetKeyDown(KeyCode.B))
        {
            VrInput = 2;
            //errandListUi.SetActive(true);
        }
        if (leftPri.triggered || Input.GetKeyDown(KeyCode.C)) //list
        {
            VrInput = 3;
            VRInteractionListEvntArgs arg = new VRInteractionListEvntArgs { };
            this.VRInteractionListEvnt(this, arg);
          
        }
        if (rightPri.triggered || Input.GetKeyDown(KeyCode.D)) //minimap
        {
            VrInput = 4;
            VRInteractionMinimapEvntArgs arg1 = new VRInteractionMinimapEvntArgs { };
            this.VRInteractionMinimapEvnt(this, arg1);

        }
            
    }

    void VRInputAndIndexCheck()
    {
        if (index == 4)
        {
            if (VrInput == 1)
            {
         
                NextLine();
            }
        }
        else if (index == 5)
        {
            if (VrInput == 2)
            {
          
                NextLine();
            }
        }
        else if (index == 6)
        {
            if (VrInput == 3) 
            {

                NextLine();
            }
                
        }
        else if (index == 7)
        {
            if (VrInput == 4)
            {

                NextLine();
            }
        }
        //bus getting on off tutorial
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
            
            NextLine();


        }

        
                  
            
    }



}
