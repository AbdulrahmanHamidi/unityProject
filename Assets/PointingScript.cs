﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class PointingScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private int counter = 0;
    private int counter2 = 0;
    public void showmsg()
    {
       
        Debug.Log("worked"+counter++);
        
    }


    public void Goout()
    {
        Debug.Log("out of target"+counter2++);
    }

    // when save no btn is pressed
    public void SaveNo()
    {
        ManageSaveBtns(true,false,false);
    }
    
    public void SaveYes()
    {
        ManageSaveBtns(true,false,false);
    }

    public void ShowYesAndNoBtns()
    {
        
       ManageSaveBtns(false,true,true);
    }


    public void ManageSaveBtns(bool father,bool yes,bool no)
    {
        GameObject SaveBtns = GameObject.Find("SavingBtns");
        if (SaveBtns != null)
        {
            SaveBtns.transform.Find("SaveBox").gameObject.SetActive(father);
            SaveBtns.transform.Find("SaveYesBox").gameObject.SetActive(yes);
            SaveBtns.transform.Find("SaveNoBox").gameObject.SetActive(no);
        }
    }

    public void test(string tst)
    {
        int counter = 0;
        //Debug.Log("debug "+tst+counter++);
    }


    public void ActivateFixedMenu(GameObject menus)
    {
        if (ManagerScript.Instance.Started)
        {
            menus.SetActive(true);

        }
    }
    
    public void deactivateFixedMenu(GameObject menus)
    {
        menus.SetActive(false);
    }



    public void DeleteBox()
    {
        GameBox temp = ManagerScript.Instance.gameBoxes.Find(x => x.isLastTouched == true);
        ManagerScript.Instance.gameBoxes.Remove(temp);
        
        
        GameObject go = GameObject.Find(temp.go.name).gameObject;
        go.SetActive(false);
    }
}
