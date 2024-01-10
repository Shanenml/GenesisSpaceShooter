using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPulse : MonoBehaviour
{
    [SerializeField]
    private AudioClip _deleteTarget;
    private AudioSource _audiosource;

    private void Start()
    {
        _audiosource = GetComponent<AudioSource>();

        if (_audiosource == null)
        {
            Debug.LogError("Shield Pulse Audio Source is NULL");
        }
        else
        {
            _audiosource.clip = _deleteTarget;
        }
        StartCoroutine(ShieldPulsePowerdown());
    }
    void Update()
    {
        CalculateGrowth();
    }

    void CalculateGrowth()
    {
        transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);
    }

    IEnumerator ShieldPulsePowerdown()
    {
        yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player")
        {
            _audiosource.Play();
            Destroy(other.gameObject);
        }
    }
}
