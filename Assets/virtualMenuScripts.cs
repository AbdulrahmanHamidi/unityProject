﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Crosstales.FB;
using Newtonsoft.Json;
//using NUnit.Framework.Constraints;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class virtualMenuScripts : MonoBehaviour
{
    private string _boxName = "";
    private bool _getKeyboardKeys = false;

    private KeyCode kCode;
    private bool bDetectKey;
    private int counter = 0;
    public GameObject MovementController;
    public GameObject BoxNameText;
    public GameObject PalletX;
    public GameObject PalletY;
    public GameObject PalletZ;
    public GameObject BoxX;
    public GameObject BoxY;
    public GameObject BoxZ;
    public GameObject screenText;
    public GameObject BoxSec;
    private string NewPath = "s";
    private string OldPath = "s";
    private string path;

    private List<GameBox> oldGameBoxes;

    // box toggles

    #region toggles

    public GameObject BoxToggleX;
    public GameObject BoxToggleY;
    public GameObject BoxToggleZ;

    #endregion

    public GameObject boxStamina;
    public GameObject boxName;


    public bool isDrifting = false;

    private Vector3 startPos;
    private Vector3 endPos;

    public float driftSec = 3;

    private float driftTimer = 0;


    public GameObject MovableBox;


    // functions


    #region calculater section

    public void digitPressed()
    {
        string numberString = ManagerScript.Instance.headNumberString;

        if (numberString.Length < 5)
        {
            if (name == "point") // we only want just one point in the number
            {
                if (!numberString.Contains("."))
                {
                    numberString += transform.Find("ButtonBackdrop").gameObject.transform.Find("Text").gameObject
                        .GetComponent<Text>().text;
                }
            }
            else
            {
                numberString += transform.Find("ButtonBackdrop").gameObject.transform.Find("Text").GetComponent<Text>()
                    .text;
            }
        }

        screenText.GetComponent<Text>().text =
            numberString.Replace("\n", "");
        //Debug.Log(numberString);
        ManagerScript.Instance.headNumberString = numberString.Replace("\n", "");
        GameObject field = ManagerScript.Instance.selectedField;
        if (field != null)
        {
            field.transform.Find("ButtonBackdrop").gameObject.transform.Find("Text").GetComponent<Text>().text =
                numberString.Replace("\n", "");
//            if (field.name.Contains("Pallet"))
//            {
//                if (CheckPalletInfo())
//                {
//                    Debug.Log("pallet true");
//                    BoxSec.SetActive(true);
//                }
//                else
//                {
//                    Debug.Log("pallet false");
//
//                    BoxSec.SetActive(false);
//                }
//            }
        }
    }

    public void digitCurrect()
    {
        if (ManagerScript.Instance.headNumberString != "")
        {
            string s =
                ManagerScript.Instance.headNumberString =
                    ManagerScript.Instance.headNumberString.Substring(0,
                        ManagerScript.Instance.headNumberString.Length - 1);
            screenText.GetComponent<Text>()
                .text = s;
            if (ManagerScript.Instance.selectedField != null)
            {
                ManagerScript.Instance.selectedField.transform.Find("ButtonBackdrop").gameObject.transform.Find("Text")
                        .gameObject.GetComponent<Text>()
                        .text =
                    ManagerScript.Instance.headNumberString;
            }
        }
    }

    public void cleanScreen()
    {
        screenText.GetComponent<Text>().text = ManagerScript.Instance.headNumberString = "";
        spreadnumber();


        if (ManagerScript.Instance.selectedField != null)
        {
            ManagerScript.Instance.selectedField.transform.Find("ButtonBackdrop").gameObject.transform.Find("Text")
                    .gameObject
                    .GetComponent<Text>().text =
                ManagerScript.Instance.headNumberString;
        }
    }

    public void spreadnumber()
    {
        //ManagerScript.Instance.selectedField.transform.Find("text").gameObject.GetComponent<TextMeshPro>().t
    }

    #endregion


    #region pallet menu section

    public void selectField()
    {
        // when user press on pallet x input for example this field will be main selected field to edit after that
        ManagerScript.Instance.selectedField = this.gameObject;
        transform.Find("ButtonBackdrop").gameObject.transform.Find("Text").gameObject
            .GetComponent<Text>().text = ManagerScript.Instance.headNumberString;
    }

    public bool CheckPalletInfo() // QuestionGroup is the form that can take box info form users
    {
        //Palet pallet = new Palet(QuestionGroup);

        string x = PalletX.GetComponent<Text>().text;
        string y = PalletY.GetComponent<Text>().text;
        string z = PalletZ.GetComponent<Text>().text;

        var b = x != "" && // we need to check if all input fields are not empty to move on to the next step
                y != "" &&
                z != "";
        if (b && !(x != "X" || y != "Y" || z != "Z"))
        {
            float pX = float.Parse(x); // converting all fields to double varaibles to work with them 
            float pY = float.Parse(y); // converting all fields to double varaibles to work with them 
            float pZ = float.Parse(z); // converting all fields to double varaibles to work with them 


            if (pX > 0 || pY > 0 || pZ > 0)
            {
                return true;
            }
            else
            {
                Debug.Log("x" + x);
                Debug.Log("y" + y);
                Debug.Log("z" + z);
                StartCoroutine(Show("Pallet Dimensions Can Not Be Negative Number", 2));
            }
        }

        return false;
    }

    #endregion


    #region Box menu section

    public void getBoxName()
    {
        // get box name from input field and put it in temp obj in manager script
        ManagerScript.Instance.tempBox.name = GetComponent<InputField>().text;
        
    }

    public void getKeysFromTheKeyboard()
    {
        bDetectKey = true;
    }

    public void DetectInput()
    {
        foreach (KeyCode vkey in System.Enum.GetValues(typeof(KeyCode)))
        {
            if (Input.GetKeyUp(vkey))
            {
                if (vkey != KeyCode.Return)
                {
                    if (vkey == KeyCode.Backspace)
                    {
                        _boxName = _boxName.Substring(0, _boxName.Length - 1);
                    }
                    else if (vkey == KeyCode.Space)
                    {
                        _boxName += " ";
                    }
                    else
                    {
                        kCode = vkey; //this saves the key being pressed               
                        _boxName += vkey.ToString().Length == 1 ? vkey.ToString() : "";
                        Debug.Log(_boxName);
                    }
                }
                else
                {
                    bDetectKey = false;
                    counter = 0;
                    turnOnMovementController(MovementController);
                    // MovementController.SetActive(true);
                }
            }
        }
    }


    public void activateBoxSecOrDeactivate()
    {
        
        if (CheckPalletInfo())
        {
            BoxSec.SetActive(true);
        }
        else
        {
            BoxSec.SetActive(false);
        }
    }
    
    

    #endregion


    #region toggle events

    public void toggle(string axis)
    {
        transform.Find("text").gameObject.GetComponent<TextMeshPro>().text = axis + " is On";
    }

    public void untoggle(string axis)
    {
        transform.Find("text").gameObject.GetComponent<TextMeshPro>().text = axis + " is Off";
    }

    #endregion


    public void turnOffMovementController(GameObject Controller)
    {
        Controller.SetActive(false);
    }

    public void turnOnMovementController(GameObject Controller)
    {
        Controller.SetActive(true);
    }


    #region uploading

    public void OpenSingleFile() // this function will run when user press on upload btn
    {
        string path = FileBrowser.OpenSingleFile("jpg"); // this will search for our image but for testing we wrote txt

        if (path != "")
        {
            OldPath = path;
            NewPath = Application.dataPath + "/" + "_" +
                      Time.deltaTime % 100 + ".jpg";

            if (!File.Exists(NewPath))
            {
                ManagerScript.Instance.tempBox.ImagePath = NewPath;
                File.Copy(OldPath, NewPath);
            }
        }
    }

    #endregion


    #region helpers

    IEnumerator Show(string msg, float time)
    {
        Debug.Log(msg);
//        AlertBox.GetComponent<Text>().text = msg;
//        AlertBox.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(time);
//        AlertBox.GetComponent<Text>().enabled = false;
    }

    #endregion


    #region Saving Project
    public void save() //save box
    {
        Box box = new Box();
        box.ImagePath = ManagerScript.Instance.tempBox.ImagePath;
        box.name = boxName.GetComponent<InputField>().text;
        box.x = float.Parse(BoxX.GetComponent<Text>().text);
        box.y = float.Parse(BoxY.GetComponent<Text>().text);
        box.z = float.Parse(BoxZ.GetComponent<Text>().text);
        box.xAxisRotation = BoxToggleX.GetComponent<Toggle>().isOn;
        box.yAxisRotation = BoxToggleY.GetComponent<Toggle>().isOn;
        box.zAxisRotation = BoxToggleZ.GetComponent<Toggle>().isOn;
        box.stamina = float.Parse(boxStamina.GetComponent<Text>().text);

        StartCoroutine(IESave(box));


    }

    public IEnumerator IESave(Box box)
    {
        yield return StartCoroutine(box.saveDB());
        if (box.id >0)
        {
            //Animations script = new Animations();
            //script.SetAlertBoxContent(box.name+ " Box Has Been Added To The Database");
            //script.CallAnimate(box.name+ " Box Has Been Added To The Database");
            //StartCoroutine( script.Animate());
        }
    }

    public void SetToggle(Toggle toggle)
    {
        if (toggle.isOn)
        {
            //text.text = "On";
        }
        else
        {
            //text.text = "Off";
        }
    }


    public void CallSaveProject(GameObject Menu)
    {
        StartCoroutine(SaveProject(Menu));
    }


    public IEnumerator SaveProject(GameObject Menu)
    {
        Projcet projcet = new Projcet();
        projcet.name = Menu.transform.Find("ProjectName").gameObject.GetComponent<InputField>().text;

        yield return projcet.saveDb();
        if (projcet.id > 0)
        {
            ManagerScript.Instance.project.id = projcet.id;
            ManagerScript.Instance.project.name = projcet.name;

            Menu.transform.Find("UIElementsPanel").gameObject.transform.Find("Pallet Sec").gameObject.SetActive(true);
            yield return getOldGameBoxes();
            if (oldGameBoxes.Count() > 0)
            {
                foreach (var box in oldGameBoxes)
                {
                    Vector3 position = new Vector3(box.position_x, box.position_y, box.position_z);
                    Vector3 scale = new Vector3(box.scale_x, box.scale_y, box.scale_y);
                    Quaternion rotation = Quaternion.Euler(box.rotation_x, box.rotation_y, box.rotation_z);
                    if (GameObject.Find(box.id.ToString()) == null)
                    {
                        GameObject boxGameObject = Instantiate(MovableBox, position, rotation);
                        // create the new box and adding it to th gameBoxes list in the manager script
                    
                        ManagerScript.Instance.gameBoxes.Add(new GameBox(box, boxGameObject));
                        // setting name of the new box
                        GameBox newBox = ManagerScript.Instance.gameBoxes.Last();
                        newBox.go.name = box.id.ToString();
                        yield return setBoxPicture(newBox);
                    }
                  

                }
            }
        }
        else
        {
            Debug.Log("something went wrong while saving the project name");
        }
    }


    public IEnumerator getOldGameBoxes()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW(
            "http://localhost:8000/api/Gameboxes/project/" + ManagerScript.Instance.project.id, form);
        yield return www;
        oldGameBoxes = new List<GameBox>();
        oldGameBoxes = JsonConvert.DeserializeObject<List<GameBox>>(www.text);
        
        //private Excel.Application app = null;
        //TODO try to make excel file
       
        
    }


    public IEnumerator setBoxPicture(GameBox box)
    {
        Renderer rend = box.go.transform.Find("Cube").gameObject.GetComponent<MeshRenderer>();


       
        string image_url = ManagerScript.Instance.boxes.Find(x => x.id == box.box_id).ImagePath;
        WWW w = new WWW(image_url);
        yield return w;
        Texture texture;
        texture = w.texture;

        rend.material.SetTexture("_MainTex", texture);
    }


    public void deactivateVirtualMenu(GameObject VirtualMenu)
    {
        StartCoroutine(IEdeactivateVirtualMenu( VirtualMenu));
    }

    public IEnumerator IEdeactivateVirtualMenu(GameObject VirtualMenu)
    {
        yield return new WaitForSeconds(1);
        VirtualMenu.SetActive(false);
    }


    public void SaveToFile()
    {

        StartCoroutine(IESaveToFile());


//        using (WebClient wc = new WebClient())
//        {
//            //wc.DownloadProgressChanged += wc_DownloadProgressChanged;
//            wc.DownloadFileAsync (
//                // Param1 = Link of file
//                new System.Uri("http://localhost:8000/api/download/project/"+ManagerScript.Instance.project.id),
//                // Param2 = Path to save
//                "C:\\Users\\abdul\\OneDrive\\Desktop\\Menu test\\projects.xlsx"
//            );
//        }
    }


    public IEnumerator IESaveToFile()
    {
        WWWForm form = new WWWForm();

        WWW www = new WWW(
            "http://localhost:8000/api/Gameboxes/project/" + ManagerScript.Instance.project.id, form);
        yield return www;

        Debug.Log(www.text);
        string Data = www.text;


        ManagerScript.Instance.tempGameboxes = JsonConvert.DeserializeObject<List<GameBox>>(Data);
//
//        string fileName = "testdata.txt";
//        System.IO.StreamWriter objWriter;
//        objWriter = new StreamWriter(fileName);
//        
//        objWriter.WriteLine("teest line");




        string fileName = @"C:\Users\abdul\OneDrive\Desktop\Menu test\Assets\test.txt";    
    
        try    
        {    
            // Check if file already exists. If yes, delete it.     
            if (File.Exists(fileName))    
            {    
                File.Delete(fileName);    
            }    
    
            // Create a new file     
            using (FileStream fs = File.Create(fileName))     
            {    
                // Add some text to file    
                Byte[] title = new UTF8Encoding(true).GetBytes("New Text File");    
                fs.Write(title, 0, title.Length);    
                byte[] author = new UTF8Encoding(true).GetBytes("Mahesh Chand");    
                fs.Write(author, 0, author.Length);    
            }    
    
            // Open the stream and read it back.    
            using (StreamReader sr = File.OpenText(fileName))    
            {    
                string s = "";    
                while ((s = sr.ReadLine()) != null)    
                {    
                    Console.WriteLine(s);    
                }    
            }    
        }    
        catch (Exception Ex)    
        {    
            Console.WriteLine(Ex.ToString());    
        }
    }
    
    
    #endregion
}