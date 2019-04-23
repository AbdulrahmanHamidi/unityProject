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


    public GameObject BtnProductObj;
    public GameObject parent;


    private List<Box> boxes;
    private List<GameObject> btns;
    private List<Transform> positions;
    private string[] Buttons;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        Buttons = new[]
        {
            "TopLeft",
            "TopCenter",
            "TopRight",
            "CenterLeft",
            "CenterCenter",
            "CenterRight",
            "BottomLeft",
            "BottomCenter",
            "BottomRight",
        };
//        positions
//            = new List<Transform>();
//        positions.Add(TopLeft.transform);
//        positions.Add(TopCenter.transform);
//        positions.Add(TopRight.transform);
//        positions.Add(CenterLeft.transform);
//        positions.Add(CenterCenter.transform);
//        positions.Add(CenterRight.transform);

        int counter = 0;
        yield return new WaitForSeconds(2);
        boxes = ManagerScript.Instance.boxes;
        Transform temp;
        foreach (var box in ManagerScript.Instance.boxes)
        {
            temp = parent.transform.Find(Buttons[counter]).transform;
            
            GameObject newMenuBtn = Instantiate(
                BtnProductObj,
                temp.position,
                temp.rotation,
                parent.transform
            );

            newMenuBtn.name = box.name;
            string boxText = box.name + "\n" + box.x + " x " + box.y + " x " + box.z;
            newMenuBtn
                    

                .transform.Find("ButtonBackdrop")
                .transform.Find("Text")
                .gameObject.GetComponent<Text>()
                .text = boxText;

            counter++;
        }



        //get data from the database
        WWW DropDownData = new WWW("http://localhost:8000/api/boxes");
        yield return DropDownData;
        string Data = DropDownData.text;
        boxes = new List<Box>();
        boxes = JsonConvert.DeserializeObject<List<Box>>(Data);

        btns = new List<GameObject>();
        int sayac = 0;

    }

    // Update is called once per frame
    void Update()
    {
    }


    public void showmsg()
    {
    }
}