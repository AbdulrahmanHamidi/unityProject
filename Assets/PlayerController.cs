
using UnityEngine;
[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 5f;

    private Rigidbody rb;

    private PlayerMotor motor;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        motor = GetComponent<PlayerMotor>();
    }

    private void Update()
    {
//        //calculate movment velocity as a 3D vector
//        float _xMov = Input.GetAxisRaw("Horizontal");
//        float _zMov = Input.GetAxisRaw("Vertical");
//
//
//
//        Vector3 _movHorizontal = transform.right * _xMov;  //(1,0,0)  left =-1
//        Vector3 _movVertical = transform.forward * _zMov; //(0,0,1)   backware =-1
//
//        //final movment vector
//        Vector3 _velocity = (_movHorizontal + _movVertical).normalized * speed;
//
//        //apply movment
//        motor.Move(_velocity);
//
//
//        //Calulate Rotation as 3d vector(turning around just the camera without touching the player object)
//        float _yRot = Input.GetAxisRaw("Mouse X");
//
//        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;
//
//        //Apply Rotation
//        motor.Rotate(_rotation);
//
//
//        //Calulate Camera Rotation as 3d vector
//        float _xRot = Input.GetAxisRaw("Mouse Y");
//
//        Vector3 _cameraRotation = new Vector3(_xRot, 0f, 0f) * lookSensitivity;
//
//        //Apply Camera Rotation
//        motor.RotateCamera(_cameraRotation);
//        
//        //MoveUpAndDown();
    }


    
    
    
}
