using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using UnityEngine.InputSystem;

public class ShowDialogueBus : MonoBehaviour
{
    private TextMeshProUGUI dialogueText;
    private float textSpeed = 0.05f;
    public string[] lines;
    private int index;

    //sound
    public int maxVisibleCharacters;
    public DialogueAudioInfoSO currentAudioInfo;
    private AudioSource audioSource;

    public void Init()
    {
        //lines = new string[2];
        dialogueText = gameObject.GetComponent<TextMeshProUGUI>();
        //lines[0] = "If you get close to the bus, you can get on the bus!";
        //lines[1] = "If you want to get off, push the button in the bus";/
        dialogueText.text = string.Empty;
        index = 0;
        audioSource = this.gameObject.AddComponent<AudioSource>();
        StartCoroutine(ShowtheDialogue());
        
    }


    IEnumerator ShowtheDialogue()
    {

        while (true)
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

            index = (index + 1) % 2;

            yield return new WaitForSeconds(4f);
        }

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
