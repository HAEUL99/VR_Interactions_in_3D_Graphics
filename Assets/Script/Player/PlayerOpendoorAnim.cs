using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class PlayerOpendoorAnim : MonoBehaviour
{
    public NPCInteractable npcInteractable;
    private Animator animator;
    public FadeScreen fadeScreen;

    private void Start()
    {
        npcInteractable.FinishTutorialEvnt += new EventHandler(StartAnim);
        animator = gameObject.GetComponent<Animator>();
        gameObject.SetActive(false);

    }

    private void StartAnim(object sender, EventArgs e)
    {

        animator.SetBool("IsCheck", true);
        StartCoroutine(CompletedDialogue());
    }

    
    IEnumerator CompletedDialogue()
    {
       
        //ield return new WaitForSeconds(0.5f);
        fadeScreen.FadeOut();
        //yield return new WaitForSeconds(fadeScreen.fadeDuration);

        
        AsyncOperation operation  = SceneManager.LoadSceneAsync("MapScene_VR");
        operation.allowSceneActivation = false;

        float timer = 0;
        while (timer <= fadeScreen.fadeDuration && !operation.isDone)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        operation.allowSceneActivation = true;
        

    }
    
 


}
