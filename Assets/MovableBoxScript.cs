using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovableBoxScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(transform.position.x > 0.82f)
            transform.position = new Vector3(0.82f, transform.position.y, transform.position.z);
        if (transform.position.x < 0)
            transform.position = new Vector3(0, transform.position.y, transform.position.z);
        
        if(transform.position.z > 2.17f)
            transform.position = new Vector3(2.17f, transform.position.y, transform.position.z);
        if (transform.position.z < 1.35f)
            transform.position = new Vector3(1.35f, transform.position.y, transform.position.z);
    }
}
