﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreateProductScript : MonoBehaviour
{
    // ReSharper disable InconsistentNaming
    #pragma warning disable 414
    
    
    
    
    public Dropdown dd;
    public Transform ProductObjPos;
    public GameObject ProductObj;

    private bool _xRotation = true;

    
    private bool yRotation = true;
    private bool zRotation = true;
    public Texture texture;
    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

//
//    public void Createbox()
//    {
//        StartCoroutine(CreateBoxx());
//    }

    //this function will work when we press on one of the dropdown options
//    public IEnumerator CreateBoxx()
//    {
//
//        int dd_index = dd.value;
//        string dd_name = dd.options[dd_index].text;
//        string box_name = dd_name.Split('(')[0];
//        Debug.Log(box_name);
//        var ebatlarstringiHazirlama = dd_name.Split('(')[1];
//        var ebatlarstringi = ebatlarstringiHazirlama.Remove(ebatlarstringiHazirlama.Length - 1);
//        var ebatlarDizisi = ebatlarstringi.Split('x');
//        var objX = float.Parse(ebatlarDizisi[0]);
//        var objY = float.Parse(ebatlarDizisi[1]);
//        var objZ = float.Parse(ebatlarDizisi[2]);
//
//
//
//        ProductObj.transform.localScale = Vector3.zero;
//        ProductObj.transform.localScale +=
//            new Vector3(objX, objY, objZ); // y input of the pallet will always be the same
//
//
//
//
//
//        GameObject newCube = Instantiate(ProductObj, ProductObjPos.position, ProductObjPos.rotation);
//        newCube.name = box_name;
//        //ManagerScript.Instance.gameBoxes.Add(Instantiate(ProductObj, ProductObjPos.position, ProductObjPos.rotation));
//        ManagerScript.Instance.gameBoxes.Add(new GameBox(newCube));
//        //Renderer rend = newCube.GetComponent<MeshRenderer>();
//        Renderer rend = newCube.transform.Find("Cube").gameObject.GetComponent<MeshRenderer>();
//
//        //get box from database by its name then get its image 
//        string image_url = ManagerScript.Instance.boxes.Find(x => x.name == box_name).ImagePath;
//        WWW w = new WWW(image_url);
//        yield return w;
//       
//        texture = w.texture;
//
//        rend.material.SetTexture(MainTex, texture);
//
//
//                
//                
//                
//        //        newCube.transform.rotation = Quaternion.Euler(0, 0, 0);
//
//        //newCube.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY;
//        // we need to create if for merging to axis into one varaible 
//
//
//    }
//


    public IEnumerator getBoxInfoBy(string by,string var)
    {
        string url = "http://localhost:8000/api/box/"+by+" / " +var;
        WWW w = new WWW(url);
        yield return w.text;
    }

}
