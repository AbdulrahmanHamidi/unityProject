using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
//using Newtonsoft.json;
using Newtonsoft.Json;
using System;
using System.Text.RegularExpressions;
using Crosstales.FB;

public class UIScript07 : MonoBehaviour
{


    #region Varaibles

    public List<Box> boxes = new List<Box>(); // our list to save boxes list to be able to use them in the program 
    public GameObject Canvas; // we create this object to reach ever GameObject in the canvas

    public Box box;

    public GameObject AlertBox; //the box that will give users feedback when new box is added
    public GameObject SaveBtn;
    private bool IsValid = false; // we use this variable to check if box info are valid or not

    //pallet input field x ..
    public GameObject PInputFieldX; //pallet x input field
    public GameObject PInputFieldY; //pallet y input field
    public GameObject PInputFieldZ; //pallet z input field

    public string NewPath = "s";
    public string OldPath = "s";
    private string path;

    private float PalletXAxis, PalletYAxis, PalletZAxis;

   
    private Palet pallet;
   

    #endregion



    public GameObject PreviewImage;

    // Start is called before the first frame update
    void Start()
    {
        boxes = new List<Box>();
    }

    #region Palet control

    public bool CheckPalletInfo() // QuestionGroup is the form that can take box info form users
    {
        //Palet pallet = new Palet(QuestionGroup);

        string x = PInputFieldX.GetComponent<InputField>().text;
        string y = PInputFieldY.GetComponent<InputField>().text;
        string z = PInputFieldZ.GetComponent<InputField>().text;


        //        string x = pallet.x.ToString();
        //        string y = pallet.y.ToString();
        //        string z = pallet.z.ToString();

        if (x != "" && // we need to check if all input fields are not empty to move on to the next step
            y != "" &&
            z != "")
        {
            PalletXAxis = float.Parse(x); // converting all fields to double varaibles to work with them 
            PalletYAxis = float.Parse(y);
            PalletZAxis = float.Parse(z);

            if (PalletXAxis > 0 || PalletYAxis > 0 || PalletZAxis > 0)
            {
                return true;
            }
            else
            {
                StartCoroutine(Show("Pallet Dimensions Can Not Be Negative Number", 2));
            }
        }

        return false;
    }

    public void CallAddPallet()
    {
        AddPallet();
    }


    public bool AddPallet()
    {
        var check = CheckPalletInfo();


        if (check)
        {
            pallet = new Palet(PalletXAxis, PalletYAxis, PalletZAxis);

            StartCoroutine(pallet.saveDB());

            if (pallet.id != -1)
            {
                disablePalletInputs();
                ActivateBoxInputs();
            }
            else
            {
                Debug.Log("Pallet creation field. Error #");
            }
        }

        return true;
    }

    private void disablePalletInputs()
    {
        //disable palet info fields
        PInputFieldX.GetComponent<InputField>().readOnly = true;
        PInputFieldY.GetComponent<InputField>().readOnly = true;
        PInputFieldZ.GetComponent<InputField>().readOnly = true;

        //show palet input field as disabled
        PInputFieldX.GetComponent<InputField>().image.color = Color.gray;
        PInputFieldY.GetComponent<InputField>().image.color = Color.gray;
        PInputFieldZ.GetComponent<InputField>().image.color = Color.gray;
        //Transform.Find("StartButton").SetActive(true);
        Canvas.transform.Find("StartButton").gameObject.SetActive(true);
        Canvas.transform.Find("ClearButton").gameObject.SetActive(true);
        Canvas.transform.Find("QuestionGroup").Find("Answer").Find("SavePalletButton").gameObject
            .SetActive(false);

        //GameObject.Find("ClearButton").SetActive(true);
    }


    private void ActivatePalletInputs()
    {
        //activate palet info fields
        PInputFieldX.GetComponent<InputField>().readOnly = false;
        PInputFieldY.GetComponent<InputField>().readOnly = false;
        PInputFieldZ.GetComponent<InputField>().readOnly = false;

        //show palet input field as active
        PInputFieldX.GetComponent<InputField>().image.color = Color.white;
        PInputFieldY.GetComponent<InputField>().image.color = Color.white;
        PInputFieldZ.GetComponent<InputField>().image.color = Color.white;

        GameObject.Find("QuestionGroup (3)").SetActive(false);
        SaveBtn.SetActive(true);

        boxes.Clear();
        Canvas.transform.Find("StartButton").gameObject.SetActive(false);
        Canvas.transform.Find("ClearButton").gameObject.SetActive(false);
        PInputFieldX.GetComponent<InputField>().text = "";
        PInputFieldY.GetComponent<InputField>().text = "";
        PInputFieldZ.GetComponent<InputField>().text = "";
    }


    private IEnumerator removePallet()
    {
        WWWForm form = new WWWForm();
        form.AddField("id", pallet.id);
        WWW www = new WWW(
            "http://localhost:8000/api/delete/pallet", form);
        yield return www;
    }
    #endregion


    #region box control
    private void ActivateBoxInputs()
    {
        GameObject questionGroup = Canvas.transform.Find("QuestionGroup (3)").gameObject;

        questionGroup.SetActive(true);
    }
    private IEnumerator removeBoxes()
    {
        foreach (var item in boxes)
        {
            WWWForm form = new WWWForm();
            form.AddField("id", item.id);
            WWW www = new WWW(
                "http://localhost:8000/api/delete/box", form);
            yield return www;
        }
    }
    public bool CheckIfBoxInfoIsValid(Box box) // from the name of the function we can tell what can this function do 
    {
        if (!boxes.Select(x => x.name).Contains(box.name))
        {
            Vector3 p = new Vector3(pallet.x, pallet.y, pallet.z);
            Vector3 b = new Vector3(box.x, box.y, box.z);
            if (
                (p.y >= b.y && p.x >= b.z && p.z >= b.x) ||
                (p.y >= b.y && p.x >= b.x && p.z >= b.z) ||
                (p.y >= b.x && p.x >= b.z && p.z >= b.y) ||
                (p.y >= b.z && p.x >= b.y && p.z >= b.x) ||
                (p.y >= b.x && p.x >= b.y && p.z >= b.z) ||
                (p.y >= b.z && p.x >= b.x && p.z >= b.y)
            )
            {
                return true; // assign IsValid global variable to true to be able to use later
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    public void CallStoreFunction(GameObject box)
    {
        SetBoxInfo(box);
        //  SetBoxInfo(box);
    }
    public bool CheckIfValueIsValid(string value)
    {
        bool sonuc = true;
        if (value == "")
            sonuc = false;
        else if (float.Parse(value) < 0)
            sonuc = false;

        return sonuc;
    }

    //this function take box info and check them then add new box to boxes list
    void SetBoxInfo(GameObject boxx)
    {
        box = new Box(boxx);
        if (CheckIfValueIsValid(box.x.ToString()) &&
            CheckIfValueIsValid(box.y.ToString()) &&
            CheckIfValueIsValid(box.z.ToString()) &&
            CheckIfValueIsValid(box.mass.ToString()) &&
            CheckIfValueIsValid(box.stamina.ToString()) &&
            CheckIfBoxInfoIsValid(box) &&
            box.name != "")
        {
            // this code will store new box info to the database; 
            box.ImagePath = NewPath;
            StartCoroutine(box.saveDB());
            if (box.id != -1 || box.id != 0)
                boxes.Add(box);
            else
            {
                Debug.Log("Box creation field. Error ");
            }
        }
        else
        {
            StartCoroutine(Show("Your Box Is Not Added, Please Check your info", 2));
        }
    }

    #endregion



    public void ClearFields()
    {
        ActivatePalletInputs();
        RemoveOldRecords();
    }

    private void RemoveOldRecords()
    {
        StartCoroutine(removePallet());
        StartCoroutine(removeBoxes());
    }

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator Show(string msg, float time)
    {
        AlertBox.GetComponent<Text>().text = msg;
        AlertBox.GetComponent<Text>().enabled = true;
        yield return new WaitForSeconds(time);
        AlertBox.GetComponent<Text>().enabled = false;
    }

    public void OpenSingleFile(GameObject box) // this function will run when user press on upload btn
    {
        Box boxx = new Box(box);

        string path = FileBrowser.OpenSingleFile("jpg"); // this will search for our image but for testing we wrote txt

        if (path != "")
        {
            OldPath = path;
            NewPath = Application.dataPath + "/" + boxx.name + "_" + boxx.x + "_" + boxx.y + "_" + boxx.z + "_" +
                      Time.deltaTime % 100 + ".jpg";

            if (!File.Exists(NewPath))
            {
                boxx.ImagePath = NewPath;
                File.Copy(OldPath, NewPath);
            }
        }
    }
}