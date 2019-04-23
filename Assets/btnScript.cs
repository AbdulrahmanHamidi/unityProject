﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class btnScript : MonoBehaviour
{
    public GameObject ProductObj;
    public GameObject ProductObjPos;
    public Texture texture;

    private static readonly int MainTex = Shader.PropertyToID("_MainTex");

    public void CallCreateBox()
    {
        StartCoroutine(CreateBox());
    }


    public IEnumerator CreateBox()
    {
        if (!ManagerScript.Instance.gameBoxes.Exists(x => x.is_done == false))
        {
            Box box;
            box = ManagerScript.Instance.boxes.Find(x => x.name == this.name);

            var objX = box.x;
            var objY = box.y;
            var objZ = box.z;

            GameObject b = new GameObject();
            b = ProductObjPos;
            Vector3 localScale;
            localScale = Vector3.zero;
            localScale += new Vector3(objX, objY, objZ);
            ProductObj.transform.localScale = localScale;

            GameObject g = new GameObject();
            g.transform.position = new Vector3(0, 83f, 1.63f);
            g.transform.rotation = Quaternion.Euler(Vector3.zero);
            if (ManagerScript.Instance.gameBoxes.Exists(x => x.go.name == box.name))
            {
                Random random = new Random();
                box.name += random.Next(1, 100);
            }

            GameObject newCube = Instantiate(ProductObj, ProductObjPos.transform.position,
                ProductObjPos.transform.rotation);
            newCube.name = box.name;

            ManagerScript.Instance.gameBoxes.Add(new GameBox(newCube,ManagerScript.Instance.boxes.Find(x =>x.name == box.name)));

//            Renderer rend = newCube.transform.GetComponent<MeshRenderer>();
            Renderer rend = newCube.transform.Find("Cube").gameObject.GetComponent<MeshRenderer>();


            //get box from database by its name then get its image 
            string image_url = ManagerScript.Instance.boxes.Find(x => x.name == box.name).ImagePath;
            WWW w = new WWW(image_url);
            yield return w;
       
            texture = w.texture;
    
            rend.material.SetTexture(MainTex, texture);
            
            GameObject.Find("Menus").gameObject.SetActive(false);
            
        }
        else
        {
            Debug.Log(ManagerScript.Instance.gameBoxes.Find(x => x.is_done == false).go.name);
        }
    }


    public void ActivateDone()
    {
        GameBox gb = new GameBox();
        gb = ManagerScript.Instance.gameBoxes.Find(x => x.isLastTouched == true);
        List<GameBox> ggg = ManagerScript.Instance.gameBoxes;
        if (ManagerScript.Instance.gameBoxes.Contains(gb))
        {
            gb.is_done = true;
            gb.MakeItDone();
            gb.isLastTouched = false;
        }
        else
        {
            //do nothing
        }
    }

    public void ActivateRotate()
    {
        if (!ManagerScript.Instance.gameBoxes.Exists(x => x.is_done == false))
        {
            Debug.Log("rotate out");
            GameBox gb = new GameBox();
            gb = ManagerScript.Instance.gameBoxes.Find(x => x.isLastTouched == true);
            if (ManagerScript.Instance.gameBoxes.Exists(x => x.isLastTouched == true))
            {
                Debug.Log(gb.is_done);
                if (gb.is_done)
                {
                    Debug.Log("rotate");
                    gb.is_done = false;
                    gb.MakeItRotate();
                }
            }
        }
    }


    public void showmsg()
    {
        Debug.Log(ManagerScript.Instance.headNumber);
    }
}