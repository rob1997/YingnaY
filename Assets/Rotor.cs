using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotor : MonoBehaviour
{
    public float sensitivity;
    
    public Rigidbody rBody;

    private Vector2 _inputVector;
    
    // Update is called once per frame
    void Update()
    {
        _inputVector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        Rotate(_inputVector);
    }

    public void Rotate(Vector2 inputVector)
    {
        inputVector *= sensitivity;

        if (inputVector == Vector2.zero)
        {
            rBody.Sleep();
        }
        
        rBody.AddTorque(inputVector.y, 0, - inputVector.x);
    }

    private void FixedUpdate()
    {
//        Rotate(_inputVector);
    }
}
