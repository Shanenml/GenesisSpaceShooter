﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private int laserID;
    
    void Update()
    {
        CalculateMovement();

    }
    void CalculateMovement()
    {
        switch(laserID)
        {
            case 0:
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
                break;
            case 1:
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
                break;
        }
        

        if (transform.position.y >= 8.0f || transform.position.y <= -8.0f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

}
