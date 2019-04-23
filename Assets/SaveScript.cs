using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.UI;

public class SaveScript : MonoBehaviour
{
    public bool isOldRecordSaved = false;


    public void print()
    {
        foreach (var gameBox in ManagerScript.Instance.gameBoxes)
        {
            Debug.Log(gameBox.name);
            var position = gameBox.transform.position;
            Debug.Log(position.x);
            Debug.Log(position.y);
            Debug.Log(position.z);
        }

        Debug.Log("=================");
    }


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
                    x.id.ToString(),
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
                //Debug.Log(www.text + counter++);
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
}