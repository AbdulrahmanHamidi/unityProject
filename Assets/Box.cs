﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;




public class Box 
{
    public int id;
    public float x;
    public float y;
    public float z;
    public string name;
    public float mass;
    public float stamina;
    public string ImagePath;
    public bool xAxisRotation = true;
    public bool yAxisRotation = true;
    public bool zAxisRotation = true;

    public float rotation_x;
    public float rotation_y;
    public float rotation_z;
    public float scale_x;
    public float scale_y;
    public float scale_z;

    public Box(GameObject box)
    {
        UIScript07 script = new UIScript07();

        // this constructor will convert information from GameObject format to Box class format
        this.x = float.Parse(box.transform.Find("InputFieldX").gameObject.GetComponent<InputField>().text);
        this.y = float.Parse(box.transform.Find("InputFieldY").gameObject.GetComponent<InputField>().text);
        this.z = float.Parse(box.transform.Find("InputFieldZ").gameObject.GetComponent<InputField>().text);
        this.xAxisRotation = box.transform.Find("xToggle").gameObject.GetComponent<Toggle>().isOn;
        this.yAxisRotation = box.transform.Find("yToggle").gameObject.GetComponent<Toggle>().isOn;
        this.zAxisRotation = box.transform.Find("zToggle").gameObject.GetComponent<Toggle>().isOn;
        this.name = box.transform.Find("InputFieldName").gameObject.GetComponent<InputField>().text;
        this.stamina = float.Parse(box.transform.Find("BoxStamina").gameObject.GetComponent<InputField>().text);
        this.mass = float.Parse(box.transform.Find("BoxMass").gameObject.GetComponent<InputField>().text);
        this.ImagePath = script.NewPath;
    }


    public Box(Box box)
    {

    }


    public Box()
    {
    }

    public IEnumerator saveDB()
    {
        WWWForm form = new WWWForm();
        string[] cols = { "project_id","x", "y", "z", "mass","name","stamina","ImagePath", "xAxisRotation", "yAxisRotation", "zAxisRotation" };
        string[] vals = {
            "1",
            this.x.ToString(),
            this.y.ToString(),
            this.z.ToString(),
            this.mass.ToString(),
            this.name,
            this.stamina.ToString(),
            this.ImagePath,
            xAxisRotation == true ? "1" : "0",
            yAxisRotation == true ? "1" : "0",
            zAxisRotation == true ? "1" : "0",
         };
        AddToForm(ref form,cols,vals);
        WWW www = new WWW(
            "http://localhost:8000/api/box", form);
        yield return www;
       // Debug.Log(www.text);
        if (www.text != "-1")
        {


            id = int.Parse(www.text);
            
         
        }
        else
            id = -1;
        
        
    }

    public void AddToForm(ref WWWForm form, string[] col, string[] val)
    {
        int counter = 0;
        foreach (var item in col)
        {
            form.AddField(item, val[counter]);
            counter++;
        }
    }
}


