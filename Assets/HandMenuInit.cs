﻿using System.Collections;
using System.Collections.Generic;
using Leap.Unity;
using Leap.Unity.Interaction;
using Newtonsoft.Json;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandMenuInit : MonoBehaviour
{
    public GameObject TopLeft;
    public GameObject TopCenter;
    public GameObject TopRight;
    public GameObject CenterLeft;
    public GameObject CenterCenter;
    public GameObject CenterRight;


    //public GameObject BtnProductObj;
   // public GameObject parent;
    private List<Box> boxes;
    private List<GameObject> btns;
    private List<Transform> positions;
    private string[] Buttons;
    int counter = 0;
    // Start is called before the first frame update
//    IEnumerator Start()
//    {
//        Buttons = new[]
//        {
//            "TopLeft",
//            "TopCenter",
//            "TopRight",
//            "CenterLeft",
//            "CenterCenter",
//            "CenterRight",
//            "BottomLeft",
//            "BottomCenter",
//            "BottomRight",
//        };
//        
//        
//        
//    }

    // Update is called once per frame
//    void Update()
//    {
//        if (ManagerScript.Instance.isBoxesListSet && counter==0)
//        {
//            StartCoroutine(Start());
//        }
//    }


    public void showmsg()
    {
    }
}