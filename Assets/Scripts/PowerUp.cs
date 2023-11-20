using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.0f;
    [SerializeField]
    private int powerupID;
    //0 = Triple Shot
    //1 = Speed
    //2 = Shields
    //3 = Ammo
    //4 = Health
    [SerializeField]
    private AudioClip _powerupSound;

    void Start()
    {

    }

    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        if (transform.position.y <= -7.0f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();

            switch(powerupID)
            {
                case 0:
                    player.TripleShotActive();
                    break;
                case 1:
                    player.SpeedBoostActive();
                    break;
                case 2:
                    player.ShieldActive();
                    break;
                case 3:
                    player.Reload();
                    break;
                case 4:
                    player.Heal();
                    break;
                default:
                    Debug.Log("Default case");
                    break;
            }

            AudioSource.PlayClipAtPoint(_powerupSound, transform.position);
            Destroy(this.gameObject);
        }
    }
}

