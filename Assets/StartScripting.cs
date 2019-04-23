using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScripting : MonoBehaviour
{

    public GameObject Leap;
    public bool isDrifting = false;
    private Vector3 startPos;
    private Vector3 endPos;
    public float driftSec =3;
    private float driftTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
         
//        if (isDrifting)
//        {
//            driftTimer += Time.deltaTime;
//            if (driftTimer>driftSec)
//            {
//                stopDrifting();
//            }
//            else
//            {
//                Debug.Log("drifting");
//                float ratio = driftTimer / driftSec;
//                transform.position = Vector3.Lerp(endPos, startPos, ratio);
//            }
//        }
    }
    
    
//    public void startGame()
//    {
//        isDrifting = true;
//
//        startPos = Leap.transform.position;
//    }


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
