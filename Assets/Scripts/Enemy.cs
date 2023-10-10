using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;
    //handle to animator component
    private Animator _destroyAnim;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        if(_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        //assign the compenent
        _destroyAnim = GetComponent<Animator>();

        if(_destroyAnim == null)
        {
            Debug.LogError("The animator is NULL");
        }
        
    }

    void Update()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -12.5f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }
            //trigger anim
            _destroyAnim.SetTrigger("OnEnemyDeath");
            Destroy(this.gameObject, 2.5f);
        }

        if(other.tag == "Laser")
        {
           Destroy(other.gameObject);

           if (_player != null)
           {
                _player.AddScore(10);
           }
            //trigger anim
            _destroyAnim.SetTrigger("OnEnemyDeath");
            _speed = 0f;
           Destroy(this.gameObject, 2.5f);
        }
    }
}
