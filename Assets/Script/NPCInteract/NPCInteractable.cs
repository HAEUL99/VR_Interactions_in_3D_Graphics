using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FinishTutorialEvntArgs : EventArgs { }
public class NPCInteractable : MonoBehaviour
{
    public GameObject dialogueImg;
    public TextMeshProUGUI dialogueText;
    private int index;
    public string[] lines;
    private float textSpeed = 0.05f;
    //Event sender
    public event EventHandler FinishTutorialEvnt;
    public GameObject playerCharacter;
    //sound
    public int maxVisibleCharacters;
    public DialogueAudioInfoSO currentAudioInfo;
    private AudioSource audioSource;

    public void Interact()
    {
        dialogueImg.SetActive(true);
        dialogueText.text = string.Empty;
        audioSource = this.gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShowtheDialogue());


    }

    IEnumerator ShowtheDialogue()
    {

        dialogueText.text = "";
        //yield return null;
        maxVisibleCharacters = 0;
        foreach (char c in lines[index].ToCharArray())
        {
            dialogueText.text += c;
            PlayDialogueSound(maxVisibleCharacters, dialogueText.text[maxVisibleCharacters]);
            maxVisibleCharacters++;
            yield return new WaitForSeconds(textSpeed);
        }

        yield return new WaitForSeconds(3f);
        FinishTutorial();


    }

    public void FinishTutorial()
    {
        playerCharacter.SetActive(true);
        FinishTutorialEvntArgs arg = new FinishTutorialEvntArgs { };
        this.FinishTutorialEvnt(this, arg);
        //gameObject.transform.parent.gameObject.SetActive(false);
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
}
