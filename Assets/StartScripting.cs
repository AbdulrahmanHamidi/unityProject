using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class StartScripting : MonoBehaviour
{




 

    private int counter = 0;
    private void Update()
    {
        if (ManagerScript.Instance.isProjectSet && counter ==0)
        {
            StartCoroutine(SetBoxes());
            counter++;
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
        ManagerScript.Instance.boxes = boxes;//JsonConvert.DeserializeObject<List<Box>>(Data);
        ManagerScript.Instance.isBoxesListSet = true;
    }
}
