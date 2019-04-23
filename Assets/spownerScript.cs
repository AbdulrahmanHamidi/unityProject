using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using UnityEngine;

public class spownerScript : MonoBehaviour
{
    public Transform spownPos;
    public GameObject spownee;
    private string path;
    private Vector3 pallet;

    void Start()
    {
        path = Application.dataPath + "/data.txt";
        
        string line;
        var file = new StreamReader(path);
        line = file.ReadLine(); // read from data.txt and take pallet info 

        var PlineSplitedByVirgul = line.Split(',');

        pallet.x = float.Parse(PlineSplitedByVirgul[0].Split(':')[1]);
        pallet.y = float.Parse(PlineSplitedByVirgul[1].Split(':')[1]);
        pallet.z = float.Parse(PlineSplitedByVirgul[2].Split(':')[1]);
        spownee.transform.localScale = Vector3.zero;
       
        spownee.transform.localScale += new Vector3(pallet.x,0.15f,pallet.z); // y input of the pallet will always be the same

        Debug.Log(spownee.transform.localScale.x + " "+ spownee.transform.localScale.y + " " + spownee.transform.localScale.z);
        Instantiate(spownee, new Vector3(-0, 0, -00000), spownPos.rotation);
    }



    // Update is called once per frame
    void Update()
    {

        
    }

   




}
