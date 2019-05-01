using System.Collections;
using System.Collections.Generic;
using Leap;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    private Rigidbody rb;
    private RectTransform rt;
    public Hand hand;
    public GameObject target;

    public bool canMoveOnX = true;
    public bool canMoveOnY = true;
    public bool canMoveOnZ = true;

    void Start()
    {
        rb = target.GetComponent<Rigidbody>();
        rt = target.GetComponent<RectTransform>();
    }

    void FixedUpdate()
    {
        // x axis movement limits
        if (target.transform.position.x >= 4.9f || target.transform.position.x < -4.9f)
            canMoveOnX = true;
        else
            canMoveOnX = false;


        // y axis movement limits
        if (target.transform.position.y <= 7.9f || target.transform.position.y > 0.01f)
            canMoveOnY = true;
        else
            canMoveOnY = false;


        // Z axis movement limits
        if (target.transform.position.z <= 4.9f || target.transform.position.z > -4.9f)
            canMoveOnZ = true;
        else
            canMoveOnZ = false;

        Move();
    }

    public void Move()
    {
        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.W))
        {
            //Move Up
            if (target != null)
                if ((target.transform.position + transform.up * Time.deltaTime).y < 5f)
                    target.transform.position += transform.up * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && Input.GetKey(KeyCode.S))
        {
            //Move Down
            if (target != null)
                if ((target.transform.position + transform.up * Time.deltaTime).y > -1f)
                    target.transform.position += -(transform.up * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.A))
        {
            //Rotate Left
            if (target != null)

                target.transform.Rotate(Vector3.down, 1f, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.D))
        {
            //Rotate Right
            if (target != null)
                target.transform.Rotate(Vector3.up, 1f, Space.World);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.S))
        {
            //Rotate Right
            if (target != null)
                target.transform.Rotate(Vector3.left, 1f, Space.Self);
            //transform.RotateAround((transform.position + new Vector3( rt.rect.width  / 2f, rt.rect.height / 2f, 0f)) ,Vector3.left, 1f );
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.W))
        {
            //Rotate Right
            if (target != null)
                target.transform.Rotate(Vector3.right, 1f, Space.Self);
            //transform.RotateAround((transform.position + new Vector3( rt.rect.width  / 2f, rt.rect.height / 2f, 0f)) ,Vector3.right, 1f );
        }

        //forward
        if (Input.GetKey(KeyCode.W) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space)))
        {
            if (target != null)
                if ((target.transform.position + transform.up * Time.deltaTime).z < 4.5f)
                    target.transform.position += transform.forward * Time.deltaTime;
        }

        //backword
        if (Input.GetKey(KeyCode.S) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space)))
        {
            if (target != null)
                if ((target.transform.position + transform.up * Time.deltaTime).z > -4.5f)
                target.transform.position += -(transform.forward * Time.deltaTime);
        }

        //left
        if (Input.GetKey(KeyCode.A) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space)))
        {
            if (target != null)
                if ((target.transform.position + transform.up * Time.deltaTime).x > -4.5f)
                    target.transform.position += -(transform.right * Time.deltaTime);
        }

        //right
        if (Input.GetKey(KeyCode.D) && !(Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.Space)))
        {
            if (target != null)
                if ((target.transform.position + transform.up * Time.deltaTime).x < 4f)
                    target.transform.position += transform.right * Time.deltaTime;
        }
    }
}