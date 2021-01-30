using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Transform _maze;
    [SerializeField] private Rigidbody _ball;
    [SerializeField] private float _rotationSpeed = 40;

    private Vector3 mPrevPos = Vector3.zero;
    private Vector3 mPosDelta = Vector3.zero;
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        MouseRotate2();
        // float smoother = Time.deltaTime * _rotationSpeed;
        // float hMov = Input.GetAxis("Horizontal") * smoother;
        // float vMov = Input.GetAxis("Vertical") * smoother;
        //
        // Vector3 newRotation = transform.localEulerAngles + new Vector3(vMov, 0, -hMov);
        // transform.localEulerAngles = newRotation;
        // _ball.transform.localEulerAngles = transform.localEulerAngles;
    }
    
    private void MouseRotate2()
    {
        if (Input.GetMouseButton(0))
        {
            float smoother = Time.deltaTime * _rotationSpeed;
            float hMov = Input.GetAxis("Mouse X") * smoother;
            float vMov = Input.GetAxis("Mouse Y") * smoother;
            
            Vector3 newRotation = transform.localEulerAngles + new Vector3(vMov, 0, -hMov);
            transform.localEulerAngles = newRotation;
            _ball.transform.localEulerAngles = transform.localEulerAngles;
        }
    }
    
    private void MouseRotate()
    {
        if (Input.GetMouseButton(0))
        {
            mPosDelta = Input.mousePosition - mPrevPos;
            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
            else
            {
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
            
            transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);
        }

        mPrevPos = Input.mousePosition;
    }

}
