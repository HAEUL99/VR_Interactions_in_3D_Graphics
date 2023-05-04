using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class NPCDialogueSound: MonoBehaviour
{
    //sound
    private int maxVisibleCharacters;
    private DialogueAudioInfoSO currentAudioInfo;
    private AudioSource audioSource;

    //lines
    private string[] lines;
    private TextMeshProUGUI dialogueText;
    private float textSpeed = 0.05f;
    public int index;



    public void Init(GameObject thisObj, DialogueAudioInfoSO currentAudioInfo, TextMeshProUGUI dialText, string[] lines, int[] pauseIndex = null)
    {
        audioSource = thisObj.AddComponent<AudioSource>();
        dialogueText = dialText;
        this.lines = lines;
        this.currentAudioInfo = currentAudioInfo;
        this.index = 0;


    }
    
    public IEnumerator TypeDialogue(int index)
    {
        this.index = index;
        if (this.index == lines.Length)
            yield break;

        yield return new WaitForSeconds(1.0f);
        dialogueText.text = string.Empty;
        maxVisibleCharacters = 0;

        
        foreach (char c in lines[this.index].ToCharArray())
        {
            dialogueText.text += c;

            PlayDialogueSound(maxVisibleCharacters, dialogueText.text[maxVisibleCharacters]);
            maxVisibleCharacters++;
            yield return new WaitForSeconds(textSpeed);
        }


        StartCoroutine(TypeDialogue(++index));
        

    }
    /*
    public IEnumerator TypeDialogue(int index, bool isEvent)
    {
        this.index = index;
        if (this.index == lines.Length)
            yield break;

        yield return new WaitForSeconds(1.0f);
        dialogueText.text = string.Empty;
        maxVisibleCharacters = 0;


        foreach (char c in lines[this.index].ToCharArray())
        {
            dialogueText.text += c;

            PlayDialogueSound(maxVisibleCharacters, dialogueText.text[maxVisibleCharacters]);
            maxVisibleCharacters++;
            yield return new WaitForSeconds(textSpeed);
        }

        if (isEvent && index )
        {
            
        }
        else
        {
            StartCoroutine(TypeDialogue(++index, false));
        }
        


    }
    */

    
    public void StartTyping()
    {
        StartCoroutine(TypeDialogue(0));

    }
    /*
    public void StartTyping(bool isEvent)
    {
        StartCoroutine(TypeDialogue(0, false));

    }

    */
    public void PlayDialogueSound(int currentDisplayedCharacterCount, char currentCharacter)
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


}
