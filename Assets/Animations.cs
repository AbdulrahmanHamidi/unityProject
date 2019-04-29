﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animations : MonoBehaviour
{
    public CanvasGroup uiElement;
    public static GameObject AlertBox;

    public GameObject player;

    #region alert box
    private void Start()
    {
        AlertBox = ManagerScript.Instance.alertBoxCanvaseGroup.transform.Find("AlertBox").gameObject;
    }

    public void FadeIn()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, .5f));
    }

    public void FadeOut()
    {
        StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, .5f));
    }

    public IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
    {
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;

        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete);

            cg.alpha = currentValue;

            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }

        print("done");
    }



    public void CallAnimate(string content)
    {
        StartCoroutine(Animate(content));
    }
    public IEnumerator Animate(string content)
    {
        SetAlertBoxContent(content);
        CanvasGroup cg = ManagerScript.Instance.alertBoxCanvaseGroup;
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 1, .5f));
        yield return new WaitForSeconds(2f);
        StartCoroutine(FadeCanvasGroup(cg, cg.alpha, 0, 0.5f));
    }


    public void SetAlertBoxContent(string content)
    {
        AlertBox.GetComponent<TextMeshProUGUI>().text = content;
    }

    

    #endregion



    #region player start movement
    


    public void MoveForwoard()
    {
        StartCoroutine(MovePlayer(player, player.transform.position.z, 0, 2f));
    }

    public IEnumerator MovePlayer(GameObject obje, float start, float end, float lerpTime = 1)
    {
        
        float _timeStartedLerping = Time.time;
        float timeSinceStarted = Time.time - _timeStartedLerping;
        float percentageComplete = timeSinceStarted / lerpTime;
        
        while (true)
        {
            timeSinceStarted = Time.time - _timeStartedLerping;
            percentageComplete = timeSinceStarted / lerpTime;

            float currentValue = Mathf.Lerp(start, end, percentageComplete)/50;
            print(currentValue);
            obje.transform.position -= new Vector3(0,0,currentValue ); //currentValue;
            if (obje.transform.position.z >0)
            {
                break;
            }
            if (percentageComplete >= 1) break;

            yield return new WaitForFixedUpdate();
        }

        print("done");
    }



    #endregion
    
    
    
}
