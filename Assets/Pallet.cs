using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Palet :MonoBehaviour
{
    public int id;
    public float x;
    public float y;
    public float z;
    public float mass;
    private Palet()
    {
        this.x = 0;
        this.y = 0;
        this.z = 0;
        this.mass = 0;
    }

    public Palet(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.mass = 0;
        this.id = 0;
    }

    
    public IEnumerator saveDB()
    {

        WWWForm form = new WWWForm();
        string[] cols = { "x", "y", "z", "mass" };
        string[] vals = {
            this.x.ToString(),
            this.y.ToString(),
            this.z.ToString(),
            "0"};
        AddToForm(ref form, cols, vals);



        WWW www = new WWW(
            "http://localhost:8000/api/pallet", form);
        yield return www;
        if (www.text != "-1")
            id =int.Parse(www.text);
        else
            id = -1;


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