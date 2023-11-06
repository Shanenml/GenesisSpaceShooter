using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 4.0f;
    [SerializeField]
    private GameObject _enemylaserPrefab;

    private Player _player;
    private Animator _destroyAnim;
    private AudioSource _explosionSound;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if(_player == null)
        {
            Debug.LogError("Player is NULL");
        }

        _destroyAnim = GetComponent<Animator>();
        if(_destroyAnim == null)
        {
            Debug.LogError("The animator is NULL");
        }

        _explosionSound = GetComponent<AudioSource>();
        if (_explosionSound == null)
        {
            Debug.LogError("Audio Source on Enemy is NULL");
        }

        //StartCoroutine(FireLaserRoutine());
        
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

            _explosionSound.Play();
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

           _explosionSound.Play();
           _destroyAnim.SetTrigger("OnEnemyDeath");
           _speed = 0f;
           Destroy(GetComponent<Collider2D>());
           Destroy(this.gameObject, 2.5f);
        }
    }

    IEnumerator FireLaserRoutine()
    {
        yield return new WaitForSeconds(Random.Range(1, 3));
        while(this.gameObject != null)
        {
            Instantiate(_enemylaserPrefab, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(3);
        }
        
    }
}
