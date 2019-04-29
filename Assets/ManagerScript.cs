﻿using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Serialization;


public class ManagerScript : MonoBehaviour
{
    public static ManagerScript Instance { get; private set; }

    public int value;
    public List<Box> boxes;
    public Palet palet ;
    public List<GameBox> gameBoxes;
    public double headNumber;
    public string headNumberString;
    public GameObject selectedField;
    public List<Palet> pallets;
    public bool LeftSave = false;
    public bool RightSave = false;
    public bool BothSave = false;
    public string ImagePath;
    public string BoxName;
    public Box tempBox = new Box();
    

    public GameObject PalletXWall;
    public GameObject PalletXWall2;
    public GameObject PalletZWall;
    public GameObject PalletZWall2;
    public GameObject Pallet;


    public List<GameBox> tempGameboxes;

    public Projcet project;

    public CanvasGroup alertBoxCanvaseGroup;
    
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        headNumber = 0;
        headNumberString = "";
        tempGameboxes = new List<GameBox>();
        project = new Projcet();
    }
    
    
//    IEnumerator Start()
//    {
//        WWW BoxesData = new WWW("http://localhost:8000/api/boxes");
//        yield return BoxesData;
//        string Data = BoxesData.text;
//        boxes = new List<Box>();
//        boxes = JsonConvert.DeserializeObject<List<Box>>(Data);
//        ManagerScript.Instance.boxes = JsonConvert.DeserializeObject<List<Box>>(Data);
//
//    }
    
    
    


    public void ClearLastTouched()
    {
        foreach (var item in gameBoxes)
        {
            item.isLastTouched = false;
        }
    }

    public void MakeItLastTouched(string _name)
    {
        ClearLastTouched();
        gameBoxes.Find(x => x.go.name == _name).isLastTouched = true;   
    }
}

 
