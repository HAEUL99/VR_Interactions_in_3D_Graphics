using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerOpendoorAnim : MonoBehaviour
{
    public ErrandOkbtn errandOkbtn;
    private Animator animator;
    private bool IsEvntSend = false;

    public CameraFade cameraFade;

    private void Start()
    {
        errandOkbtn.DialogueOkEvnt += new EventHandler(StartAnim);
        animator = gameObject.GetComponent<Animator>();
    }

    private void StartAnim(object sender, EventArgs e)
    {

        animator.SetBool("IsCheck", true);
        StartCoroutine(CompletedDialogue());
    }


    IEnumerator CompletedDialogue()
    {
        yield return new WaitForSeconds(0.8f);
        cameraFade.m_IsFading = false;
        cameraFade.FadeOut(1.2f, true);
        yield return new WaitForSeconds(1.1f);
        SceneManager.LoadScene("GameScene");

    }
    /*
    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("OpenDoor") &&
   animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f && IsEvntSend == false)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    */


}
