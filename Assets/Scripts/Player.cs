using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField]
    private float _speed = 5f;
    [SerializeField]
    private float _speedboostmult = 2f;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPlayer;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;
 
    [SerializeField]
    private AudioClip _laserSound;
    private AudioSource  _audioSource;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;

    [SerializeField]
    private bool _isTripleShotActive = false;
    [SerializeField]
    private bool _isShieldActive = false;


    void Start()
    {
       transform.position = new Vector3(0, -4f, 0);
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager is NULL");
        }
        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }
        if (_audioSource == null)
        {
            Debug.LogError("Audio Source is NULL");
        }
        else
        {
            _audioSource.clip = _laserSound;
        }
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

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5, 2), 0);
       
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
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
        }

        _audioSource.Play();
        //play laser audio clip
    }

    public void Damage()
    {

        if(_isShieldActive == true)
        {
            _isShieldActive = false;
            _shieldPlayer.SetActive(false);
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);

        if(_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        else if(_lives == 1)
        {
            _leftEngine.SetActive(true);
        }
        else if(_lives < 1)
        {
           _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            _uiManager.GameOverSequence();
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDownRoutine());
    }


    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
 
    public void SpeedBoostActive()
    {
        _speed *= _speedboostmult;
        StartCoroutine(SpeedBoostPowerDown());
    }

    IEnumerator SpeedBoostPowerDown()
    {
        yield return new WaitForSeconds(5.0f);
        _speed /= _speedboostmult;
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        _shieldPlayer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
