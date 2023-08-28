using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    // Variable
    [SerializeField]
    private float _speed = 4.5f;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        CalcMovement();
    }

    void CalcMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 directioncom = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 topwall = new Vector3(transform.position.x, 0, 0);
        Vector3 bottomwall = new Vector3(transform.position.x, -5f, 0);
        Vector3 rightwall = new Vector3(11.3f, transform.position.y, 0);
        Vector3 leftwall = new Vector3(-11.3f, transform.position.y, 0);


        transform.Translate(directioncom * _speed * Time.deltaTime);

        //y axis clamp
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5, 0), 0);
        if (transform.position.x >= 11.3f)
        {
            transform.position = leftwall;
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = rightwall;
        }
    }

}
