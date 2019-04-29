using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public bool isOldRecordSaved = false;


   


    public void CallSave()
    {
        StartCoroutine(save());
    }

    public IEnumerator save()
    {
        yield return saveOldRecords();
        if (isOldRecordSaved)
        {
            yield return DeleteOldRecords();
            List<GameBox> boxesToUpload = new List<GameBox>();
            WWWForm form = new WWWForm();
            string[] cols =
            {
                "project_id",
                "box_id",
                "position_x",
                "position_y",
                "position_z",
                "rotation_x",
                "rotation_y",
                "rotation_z",
                "scale_x",
                "scale_y",
                "scale_z",
                "in_pallet",
                "is_done",
            };
            //int counter = 0;


            foreach (var x in ManagerScript.Instance.gameBoxes)
            {
                string[] vals =
                {
                    ManagerScript.Instance.project.id.ToString(),
                    x.box_id.ToString(),
                    x.cube.transform.position.x.ToString(),
                    x.cube.transform.position.y.ToString(),
                    x.cube.transform.position.z.ToString(),

                    x.cube.transform.rotation.x.ToString(),
                    x.cube.transform.rotation.y.ToString(),
                    x.cube.transform.rotation.z.ToString(),

                    x.cube.transform.localScale.x.ToString(),
                    x.cube.transform.localScale.y.ToString(),
                    x.cube.transform.localScale.z.ToString(),

                    "1",
                    x.is_done ? "1" : "0",
                };

                AddToForm(ref form, cols, vals);


                WWW www = new WWW(
                    "http://localhost:8000/api/save/gamebox", form);
                yield return www;
                Debug.Log(www.text );
            }
        }
    }


    public IEnumerator saveOldRecords()
    {
        // http://localhost:8000/api/Gameboxes/project/
        WWWForm form = new WWWForm();

        WWW www = new WWW(
            "http://localhost:8000/api/Gameboxes/project/" + ManagerScript.Instance.project.id, form);
        yield return www;

        Debug.Log(www.text);
        string Data = www.text;
        if (Data == "[]")
        {
            isOldRecordSaved = true;
        }

        ManagerScript.Instance.tempGameboxes = JsonConvert.DeserializeObject<List<GameBox>>(Data);
        isOldRecordSaved = true;
        //ManagerScript.Instance.boxes = JsonConvert.DeserializeObject<List<Box>>(Data);
    }


    public IEnumerator DeleteOldRecords()
    {
        WWWForm form = new WWWForm();
        WWW www = new WWW(
            "http://localhost:8000/api/Gameboxes/delete/project/" + ManagerScript.Instance.project.id, form);
        yield return www;
        print(www.text);
    }
    private List<GameBox> convertToGameBoxClassArray(List<GameObject> gameBoxes)
    {
        List<GameBox> gb = new List<GameBox>();

        GameBox _gameBox = new GameBox();

        foreach (var gameBox in gameBoxes)
        {
            var position = gameBox.transform.position;
            var rotation = gameBox.transform.rotation;
            _gameBox.position_x = position.x;
            _gameBox.position_y = position.y;
            _gameBox.position_z = position.z;
            _gameBox.rotation_x = rotation.x;
            _gameBox.rotation_x = rotation.y;
            _gameBox.rotation_x = rotation.z;
            _gameBox.name = gameBox.name;
        }


        return gb;
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


    public void SaveToFile()
    {
        StartCoroutine(IESaveToFile());
//        using (System.IO.StreamWriter file = 
           //            new System.IO.StreamWriter(@"C:\Users\Public\TestFolder\WriteLines2.txt", true))
           //        {
           //            foreach (var box in ManagerScript.Instance.gameBoxes)
           //            {
           //                
           //            }
           //            file.WriteLine("Fourth line");
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
        string path = @"C:\Users\abdul\OneDrive\Desktop\Menu test\Assets\testdata.txt";
        File.WriteAllText(path, www.text);
        string fileName = @"C:\Users\abdul\OneDrive\Desktop\Menu test\Assets\testdata.txt";
//        System.IO.StreamWriter objWriter;
//        objWriter = new StreamWriter(fileName);
//        
//        objWriter.Write(www.text);




        fileName = @"C:\Users\abdul\OneDrive\Desktop\Menu test\Assets\testdata.txt";    
    
//        try    
//        {    
//            // Check if file already exists. If yes, delete it.     
//            if (File.Exists(fileName))    
//            {    
//                File.Delete(fileName);    
//            }    
//    
//            // Create a new file     
//            using (FileStream fs = File.Create(fileName))     
//            {
//
//
//                foreach (var box in ManagerScript.Instance.gameBoxes)
//                {
//                    string stringLine =
//                            box.name + " " +
//                            box.position_x + " " +
//                            box.position_y + " " +
//                            box.position_z + " " +
//                            box.rotation_x + " " +
//                            box.rotation_y + " " +
//                            box.rotation_z + " " +
//                            box.scale_x + " " +
//                            box.scale_y + " " +
//                            box.scale_z + " " +
//                            box.is_done + " " +
//                            box.box_id + " " +
//                            box.project_id + " " 
//                        ;
//                    Byte[] line = new UTF8Encoding(true)
//                        .GetBytes(stringLine);    
//                    fs.Write(line, 0, line.Length);   
//                }
//
//            }    
//    
//            // Open the stream and read it back.    
//            using (StreamReader sr = File.OpenText(fileName))    
//            {    
//                string s = "";    
//                while ((s = sr.ReadLine()) != null)    
//                {    
//                    Console.WriteLine(s);    
//                }    
//            }    
//        }    
//        catch (Exception Ex)    
//        {    
//            Console.WriteLine(Ex.ToString());    
//        }
    }
}