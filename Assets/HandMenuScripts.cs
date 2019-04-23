using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HandMenuScripts : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DisableTransformTools()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Transform Tool");
        foreach (var item in objects)
        {
            item.SetActive(false);
        }
    }
    
    public void EnableTransformTools()
    {
        var objects = Resources.FindObjectsOfTypeAll<GameObject>().Where(obj => obj.name == "Transform Tool");
        foreach (var item in objects)
        {
            item.SetActive(true);
        }
    }
    
    
}
