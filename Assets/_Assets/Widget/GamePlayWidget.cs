using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayWidget : MonoBehaviour
{
    [SerializeField] Image mTransitionImage;

    void Awake()
    {
        mTransitionImage.gameObject.SetActive(false);
    }

    public void DipToBlack(float dipInAndOutDuration, float dipStayDuration, Action dippedToBlackCallback)
    {
        StartCoroutine(StartDipToBlack(dipInAndOutDuration, dipStayDuration, dippedToBlackCallback));
    }
    
    IEnumerator StartDipToBlack(float dipInAndOutDuration, float dipStayDuration, Action dippedToBlackCallback)
    {
        float timeCounter = 0;
        mTransitionImage.gameObject.SetActive(true);
        Color transitionImageColor = Color.black;
        transitionImageColor.a = 0;

        while (timeCounter < dipInAndOutDuration)
        {
            transitionImageColor.a = timeCounter / dipInAndOutDuration;
            mTransitionImage.color = transitionImageColor;
            timeCounter += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }

        transitionImageColor.a = 1;
        mTransitionImage.color = transitionImageColor;
        dippedToBlackCallback();

        //wait for dipStayDuration
        yield return new WaitForSeconds(dipStayDuration);

        //Dip out from black
        while (transitionImageColor.a > 0)
        {
            transitionImageColor.a -= Time.deltaTime;
            mTransitionImage.color = transitionImageColor;
            yield return new WaitForEndOfFrame();
        }

        mTransitionImage.gameObject.SetActive(false);
        
    }
}
