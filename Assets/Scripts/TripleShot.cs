using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleShot : MonoBehaviour
{
    [SerializeField]
    private GameObject _laserPrefab;


    // Start is called before the first frame update
    void Start()
    {
        Instantiate(_laserPrefab, transform.position + new Vector3(-0.79f, -0.35f, 0), Quaternion.identity);
        Instantiate(_laserPrefab, transform.position + new Vector3(0.79f, -0.35f, 0), Quaternion.identity);
        Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
