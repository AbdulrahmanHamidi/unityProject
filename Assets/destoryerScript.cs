using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destoryerScript : MonoBehaviour
{

    public float lifetime = 10f;


    // Update is called once per frame
    void Update()
    {
        if (lifetime >0)
        {
            lifetime -= Time.deltaTime;
            if (lifetime == 0)
            {
                Destruction();
            }

        }

        if (this.transform.position.y <= -20)
        {
            
        }
        
    }


    void Destruction()
    {
        Destroy(this.gameObject);
    }
}
