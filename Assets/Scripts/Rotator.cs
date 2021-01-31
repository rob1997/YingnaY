using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Rigidbody _ball;
    [SerializeField] private float _rotationSpeed = 30;
    [SerializeField] private float _mouseSensitivity = 2;

       
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse Rotate Control
        MouseRotate();

        // Keyboard Rotate Control
        //KeyBoardRotate();

    }

    private void KeyBoardRotate()
    {
        float smoother = Time.deltaTime * _rotationSpeed;
        float hMov = Input.GetAxis("Horizontal") * smoother;
        float vMov = Input.GetAxis("Vertical") * smoother;

        if (hMov != 0 || vMov != 0)
        {

            Vector3 newRotation = transform.eulerAngles + new Vector3(vMov, 0, -hMov);
            newRotation = Clamp(newRotation, 20f, 340f);
            transform.rotation = Quaternion.Euler(newRotation);
            _ball.transform.rotation = transform.rotation;
        }
        else
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), _rotationSpeed * Time.deltaTime);
            _ball.transform.rotation = transform.rotation;
        }
    }

    private Vector3 Clamp(Vector3 value, float minMax, float maxMax)
    {
        if (value.x > maxMax - 50 && value.x < maxMax)
        {
            value.x = maxMax;
        }
        if(value.x < maxMax - 50 && value.x > minMax)
        {
            value.x = minMax;
        }

        if (value.z > maxMax - 50 && value.z < maxMax)
        {
            value.z = maxMax;
        }
        if (value.z < maxMax - 50 && value.z > minMax)
        {
            value.z = minMax;
        }

        return value;
    }


    private void MouseRotate()
    {

        Cursor.lockState = CursorLockMode.Locked;
        
            float smoother = Time.deltaTime * _rotationSpeed * _mouseSensitivity;
            float hMov = Input.GetAxis("Mouse X") * smoother;
            float vMov = Input.GetAxis("Mouse Y") * smoother;

            if (hMov != 0 || vMov != 0)
            {

                Vector3 newRotation = transform.eulerAngles + new Vector3(vMov, 0, -hMov);
                newRotation = Clamp(newRotation, 20f, 340f);
                transform.rotation = Quaternion.Euler(newRotation);
                _ball.transform.rotation = transform.rotation;
            }
            //Back to zero Positioner
            //else
            //{
            //    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), _rotationSpeed * Time.deltaTime);
            //    _ball.transform.rotation = transform.rotation;
            //}
    }
    

}
