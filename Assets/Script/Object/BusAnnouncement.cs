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
    private int busDirection;

    //sound
    public int maxVisibleCharacters;
    public DialogueAudioInfoSO currentAudioInfo;
    private AudioSource audioSource;
    public string dialogueText;
    public string line;
    public BusCollider[] busColliders;
    public bool isPlayerRide;
    private PlayerRideBus playerRideBus;
    private Transform bus;


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
        Tennyson,

    }

    // Start is called before the first frame update
    void Start()
    {
        busCollider.ArrivedNearBusStopEvnt += new EventHandler(SaveCurrentBusStop);
        AnnounceText = gameObject.GetComponentInChildren<TMP_Text>();
        AnnouRectTrans = AnnounceText.GetComponent<RectTransform>();
        pos = AnnouRectTrans.anchoredPosition;

        audioSource = this.gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
        for (int i = 0; i < busColliders.Length; i++)
        {
            busColliders[i].PlayerEnterBusEvnt += new EventHandler(EnableAudio);
        }
        playerRideBus = GameObject.FindWithTag("Player").GetComponent<PlayerRideBus>(); 
        playerRideBus.PlayerGetOffBusEvnt += new EventHandler(DisableAudio);



    }

    private void SaveCurrentBusStop(object sender, EventArgs e)
    {
        ArrivedNearBusStopEvntArgs arg = e as ArrivedNearBusStopEvntArgs;
        string currentBusStop = arg.currentBusStopName;
        busDirection = arg.busdirection;
        
        BusStopName currentBusStopName = (BusStopName)Enum.Parse(typeof(BusStopName), $"{currentBusStop}");
        currentBusStopnInt = (int)currentBusStopName;
        //ReplacetheNextStop();
        StartCoroutine(ReplacetheNextStop());
    }


    private void EnableAudio(object sender, EventArgs e)
    {
        
        PlayerEnterBusEvntArgs arg = e as PlayerEnterBusEvntArgs;
        bus = arg.bus.transform.parent;

        if (bus == gameObject.transform.parent.parent)
        {
            isPlayerRide = true;
        }
    }

    private void DisableAudio(object sender, EventArgs e)
    {
        isPlayerRide = false;
    }

    IEnumerator ReplacetheNextStop()
    {
 
        AnnounceText.text = $"This Bus Stop is {(BusStopName)((currentBusStopnInt) % 9)}";
        if (isPlayerRide)
        {
            line = AnnounceText.text;
            StartCoroutine(BusAnnounceSound());
        }
        Vector2 Pos = new Vector2(-1.5f, 0);
        AnnouRectTrans.DOAnchorPos(Pos, 10f);
        yield return new WaitForSeconds(10f);

        AnnounceText.text = $"";
        AnnouRectTrans.anchoredPosition = pos;
        yield return new WaitForSeconds(4f);

        if (busDirection == 0) //forward
        {
            AnnounceText.text = $"This Bus Stop is {(BusStopName)((currentBusStopnInt + 1) % 9)}, Next Bus Stop is {(BusStopName)((currentBusStopnInt + 2) % 9)}";

            if (isPlayerRide)
            {
                line = AnnounceText.text;
                StartCoroutine(BusAnnounceSound());
            }
            

            Vector2 Pos2 = new Vector2(-1.5f, 0);
            AnnouRectTrans.DOAnchorPos(Pos2, 8f);
            yield return new WaitForSeconds(8f);
        }
        else
        {
            AnnounceText.text = $"This Bus Stop is {(BusStopName)((currentBusStopnInt + 8 ) % 9)}, Next Bus Stop is {(BusStopName)((currentBusStopnInt + 7) % 9)}";
            if (isPlayerRide)
            {
                line = AnnounceText.text;
                StartCoroutine(BusAnnounceSound());
            }
            Vector2 Pos2 = new Vector2(-1.5f, 0);
            AnnouRectTrans.DOAnchorPos(Pos2, 8f);
            yield return new WaitForSeconds(8f);
        }
        


        AnnounceText.text = $"";
        AnnouRectTrans.anchoredPosition = pos;
        yield return new WaitForSeconds(2f);
    }

    IEnumerator BusAnnounceSound()
    {
        dialogueText = string.Empty;
        maxVisibleCharacters = 0;
        foreach (char c in line.ToCharArray())
        {
            dialogueText += c;

            PlayDialogueSound(maxVisibleCharacters, dialogueText[maxVisibleCharacters]);
            maxVisibleCharacters++;

            yield return new WaitForSeconds(0.1f);

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
