using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projcet : MonoBehaviour
{
    public int id;
    public string name;

    public Projcet(string name,int id)
    {
        this.name = name;
        this.id = id;
    }

    public Projcet()
    {
        
    }


    public IEnumerator saveDb()
    {
        WWWForm form = new WWWForm();
        form.AddField("name",this.name);
        WWW www = new WWW(
            "http://localhost:8000/api/save/project", form);
        yield return www;
        //Debug.Log(www.text);
        if (www.text !="-1")
        {
            this.id = int.Parse(www.text);
        }
        else
        {
            this.id = -1;
        }
    }
}
