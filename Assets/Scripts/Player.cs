using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField]
    private float _speed = 4.5f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    void Start()
    {
        transform.position = new Vector3(0, 0, 0);
    }

    void Update()
    {        
        CalculateMovement();

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            FireLaser();
        }
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 directioncom = new Vector3(horizontalInput, verticalInput, 0);
        Vector3 rightwall = new Vector3(11.3f, transform.position.y, 0);
        Vector3 leftwall = new Vector3(-11.3f, transform.position.y, 0);

        transform.Translate(directioncom * _speed * Time.deltaTime);

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

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 0.8f, 0), Quaternion.identity);
    }

    public void Damage()
    {
        _lives--;

        if(_lives < 1)
        {
            Destroy(this.gameObject);
        }
    }

}
