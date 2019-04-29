using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class StartScripting : MonoBehaviour
{

    public GameObject Leap;
    public bool isDrifting = false;
    private Vector3 startPos;
    private Vector3 endPos;
    public float driftSec =3;
    private float driftTimer = 0;


    public IEnumerator Start()
    {
        startPos = transform.position;
        WWW BoxesData = new WWW("http://localhost:8000/api/boxes");
        yield return BoxesData;
        string Data = BoxesData.text;
        List<Box> boxes;
        boxes = new List<Box>();
        boxes = JsonConvert.DeserializeObject<List<Box>>(Data);
        ManagerScript.Instance.boxes = boxes;//JsonConvert.DeserializeObject<List<Box>>(Data);

    }


    public void startDrifting(GameObject target)
    {
        for (int i = 0; i < 500; i++)
        {
            target.transform.position += transform.forward * Time.deltaTime;
        }
        GameObject.Find("Virtual Menu").gameObject.SetActive(false);
        
        
        
        Debug.Log("start drifting");
        isDrifting = true;
        driftTimer = 0;
        endPos = transform.position;
    }
    
    
    
    public void stopDrifting()
    {
        isDrifting = false;
       
        transform.position = startPos;
    }
}
