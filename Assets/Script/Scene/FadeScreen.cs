using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
    public bool fadeOnStart = true;
    public float fadeDuration = 3;
    public Color fadeColor;
    private Renderer renderer;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    public void FadeIn() { Fade(1, 0); }
    public void FadeOut() { Fade(0, 1); }

    public void FadeIn(float fadeDura)
    {
        Fade(1, 0, fadeDura);
    }

    public void FadeOut(float fadeDura)
    {
        Fade(0, 1, fadeDura);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    public void Fade(float alphaIn, float alphaOut, float fadeDura)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut, fadeDura));
    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0;
        while (timer <= fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer/fadeDuration);

            renderer.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;

        renderer.material.SetColor("_Color", newColor2);


    }

    public IEnumerator FadeRoutine(float alphaIn, float alphaOut, float fadeDura)
    {
        float timer = 0;
        while (timer <= fadeDura)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDura);

            renderer.material.SetColor("_Color", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColor2 = fadeColor;
        newColor2.a = alphaOut;

        renderer.material.SetColor("_Color", newColor2);


    }



}
