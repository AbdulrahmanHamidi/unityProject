﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [SerializeField]
   // private GameObject cam;

    //private Vector3 velocity = Vector3.zero;

    //private Vector3 rotation = Vector3.zero;
    //private Vector3 cameraRotation = Vector3.zero;
    private Rigidbody rb;
    private RectTransform rt;
    public GameObject target;
    void Start()
    {
        rb = target.GetComponent<Rigidbody>();
        rt = target.GetComponent<RectTransform>();
        
    }
    //gets a movment vector
//    public void Move(Vector3 _velocity)
//    {
//        velocity = _velocity;
//
//    }
//
//
//    //gets a rotational vector
//    public void Rotate(Vector3 _rotation)
//    {
//        rotation = _rotation;
//    }


    //gets a rotational vector for the camera
//    public void RotateCamera(Vector3 _cameraRotation)
//    {
//        cameraRotation = _cameraRotation;
//    }

    // run every physics iteration
    void FixedUpdate()
    {
        //PerformMovment();
        //PerformRotation();
        Move();
    }
//
//    //Perform movment based on velocity variable
//    void PerformMovment()
//    {
//        if (velocity != Vector3.zero)
//            rb.MovePosition(rb.position + velocity * Time.deltaTime);
//    }
//
//    //Perform Rotation
//    void PerformRotation()
//    {
//        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
//        if (cam != null)   
//            cam.transform.Rotate(-cameraRotation,Space.Self);
//    }
//    
//    
    public void Move()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W))
        {
            //Move Up
            if (target != null)
            target.transform.position += transform.up * Time.deltaTime;
        }
        
        if ( Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        {
            //Move Down
            if (target != null)
            target.transform.position += -(transform.up * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            //Rotate Left
            if (target != null)
            target.transform.Rotate(Vector3.down, 1f,  Space.World);
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            //Rotate Right
            if (target != null)
            target.transform.Rotate(Vector3.up, 1f ,Space.World);
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            //Rotate Right
            if (target != null)
            target.transform.Rotate(Vector3.left, 1f,Space.Self);
            //transform.RotateAround((transform.position + new Vector3( rt.rect.width  / 2f, rt.rect.height / 2f, 0f)) ,Vector3.left, 1f );
        }
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            //Rotate Right
            if (target != null)
            target.transform.Rotate(Vector3.right, 1f,Space.Self);
            //transform.RotateAround((transform.position + new Vector3( rt.rect.width  / 2f, rt.rect.height / 2f, 0f)) ,Vector3.right, 1f );
        }
        
        //forward
        if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space)))
        {
            if (target != null)
            target.transform.position += transform.forward * Time.deltaTime;
        }
        //backword
        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space)))
        {
            if (target != null)
            target.transform.position += -(transform.forward * Time.deltaTime);
        }
        //left
        if ( Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space)))
        {
            if (target != null)
            target.transform.position += -(transform.right * Time.deltaTime);
        }
        
        //right
        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space)))
        {
            if (target != null)
            target.transform.position += transform.right * Time.deltaTime;
        }
        
        
        

    }

}
