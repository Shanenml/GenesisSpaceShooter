using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser_Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 8.0f;
    
    void Update()
    {
        CalculateMovement();

    }
    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y >= 8.0f)
        {
            if(transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.transform.GetComponent<Player>();
        if(player != null)
        {
            player.Damage();
            Destroy(this.gameObject);
        }
        
    }

}
