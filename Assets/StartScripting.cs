using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class StartScripting : MonoBehaviour
{
    private int counter = 0;
    private int counter1 = 0;
    private int counter2 = 0;

    private void Update()
    {
        if (ManagerScript.Instance.isProjectSet && counter == 0)
        {
            StartCoroutine(SetBoxes());
            counter++;
        }

        if (ManagerScript.Instance.CanStart && counter1 == 0)
        {
            ManagerScript.Instance.StartBtn.SetActive(true);
            counter1++;
        }


        if (ManagerScript.Instance.isBoxesListSet && counter2 == 0)
        {
            setHandMenuBtns();
            counter2++;
        }
    }


    public IEnumerator SetBoxes()
    {
        WWW BoxesData = new WWW("http://localhost:8000/api/boxes/project/" + ManagerScript.Instance.project.id);
        yield return BoxesData;
        string Data = BoxesData.text;
        List<Box> boxes;
        boxes = new List<Box>();
        boxes = JsonConvert.DeserializeObject<List<Box>>(Data);
        ManagerScript.Instance.boxes = boxes; //JsonConvert.DeserializeObject<List<Box>>(Data);
        ManagerScript.Instance.isBoxesListSet = true;
    }

    public void setHandMenuBtns()
    {
        
       // List<Box> boxes = ManagerScript.Instance.boxes;
        Transform temp;
        string[] Buttons = new[]
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

        foreach (var box in ManagerScript.Instance.boxes)
        {
            GameObject parent = ManagerScript.Instance.HandMenuBtnParent;
            temp = parent.transform.Find(Buttons[counter]).transform;

            GameObject newMenuBtn = Instantiate(
                ManagerScript.Instance.BtnProductObj,
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

        //ManagerScript.Instance.CanStart = true;
    }


    public void startTheGame()
    {
        ManagerScript.Instance.Started = true;
    }
}