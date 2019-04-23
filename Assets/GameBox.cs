using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameBox : MonoBehaviour
{
    public int id;
    public int project_id;
    public int box_id;
    public float position_x;
    public float position_y;
    public float position_z;
    public float rotation_x;
    public float rotation_y;
    public float rotation_z;
    public float scale_x;
    public float scale_y;
    public float scale_z;
   
    public bool in_pallet;
   
    //[FormerlySerializedAs("isDone")] 
    public bool is_done;
    public bool isRotatable;
    public bool isLastTouched;
    public GameObject go;
    public GameObject cube;
    public GameObject transformTools;

    // Start is called before the first frame update
    void Start()
    {
        go = new GameObject();
        is_done = true;
        
        isRotatable = false;
        isLastTouched = false;
        in_pallet = false;
    }

    public GameBox(GameObject go, Box box)
    {
        this.id = box.id;
        this.go = go;
        this.cube = go.transform.Find("Cube").gameObject;
        this.transformTools = go.transform.Find("Transform Tool").gameObject;
        this.position_x = go.transform.position.x;
        this.position_y = go.transform.position.y;
        this.position_z = go.transform.position.z;
        this.rotation_x = go.transform.rotation.x;
        this.rotation_y = go.transform.rotation.y;
        this.rotation_z = go.transform.rotation.z;
       // this.is_done = false;
    }
    
    public GameBox(GameBox gamebox,GameObject go)
    {
        this.box_id = gamebox.box_id;
        this.go = go;
        this.cube = go.transform.Find("Cube").gameObject;
        this.transformTools = go.transform.Find("Transform Tool").gameObject;
        this.position_x = go.transform.position.x;
        this.position_y = go.transform.position.y;
        this.position_z = go.transform.position.z;
        this.rotation_x = go.transform.rotation.x;
        this.rotation_y = go.transform.rotation.y;
        this.rotation_z = go.transform.rotation.z;
        this.is_done = gamebox.is_done;
    }

    public GameBox()
    {
    }


    public void MakeItDone()
    {
        this.is_done = true;
        RigidbodyConstraints rc;
        rc = RigidbodyConstraints.None;

        go.transform.position += go.transform.Find("Cube").gameObject.transform.localPosition;
        go.transform.rotation = new Quaternion(go.transform.Find("Cube").gameObject.transform.localRotation.x,
            go.transform.Find("Cube").gameObject.transform.localRotation.y,
            go.transform.Find("Cube").gameObject.transform.localRotation.z, 1);
        ;
        go.transform.Find("Cube").gameObject.transform.localPosition = Vector3.zero;
        go.transform.Find("Cube").gameObject.transform.localRotation = new Quaternion(0,0,0,1);
        go.transform.Find("Cube").gameObject.GetComponent<Rigidbody>().constraints = rc;


        go.transform.Find("Transform Tool").gameObject.SetActive(false);
        //go.transform.Find("Transform Tool").gameObject.transform.Find("Translate Handles").gameObject.SetActive(false);
    }


    public void MakeItRotate()
    {
        if (ManagerScript.Instance.gameBoxes.Exists(x => x.is_done == false))
        {
            this.is_done = false;
            RigidbodyConstraints rc;
            rc = RigidbodyConstraints.FreezeAll;
            this.go.GetComponent<Rigidbody>().MovePosition(this.go.transform.localPosition);
            this.go.transform.Find("Cube").gameObject.GetComponent<Rigidbody>().constraints = rc;
            this.go.transform.Find("Cube").gameObject.transform.localPosition = Vector3.zero;
            Debug.Log("rotate done");
        }
    }
}