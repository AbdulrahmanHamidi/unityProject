using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotObj : MonoBehaviour
{
    public float rotSpeed;
    public float distance =1;
    void OnMouseDrag()
    {
//        float rotX = Input.GetAxis("Mouse X") * rotSpeed*Mathf.Deg2Rad;
//        float rotY = Input.GetAxis("Mouse Y") * rotSpeed*Mathf.Deg2Rad;
//
//        transform.RotateAround(Vector3.up, -rotX);
//        transform.RotateAround(Vector3.right, -rotY);

        Vector3 mousePositoin = new Vector3(Input.mousePosition.x,Input.mousePosition.y,distance);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePositoin);

        transform.position = objPosition;



    }

}
