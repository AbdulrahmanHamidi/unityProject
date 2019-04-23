using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class GameScripts : MonoBehaviour
{


    public GameObject lamp;


    void Update()
    {

    }


    
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        string obje;
        obje = hit.collider.gameObject.name;
    }
}