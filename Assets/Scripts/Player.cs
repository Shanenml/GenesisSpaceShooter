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
    private float _thrusterBoost = 2.0f;
    [SerializeField]
    private float _fireRate = 0.25f;
    private float _canFire = -1f;
    [SerializeField]
    private float _laserAmmo = 15f;
    public float _shieldPulseAmmo = 0f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField]
    private int _score;
    private int _shieldLives;

    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private GameObject _shieldPlayer;
    [SerializeField]
    private GameObject _shieldPulsePrefab;
    [SerializeField]
    private GameObject _rightEngine, _leftEngine;

    [SerializeField]
    private AudioClip _laserSound;
    [SerializeField]
    private AudioClip _tripleShotSound;
    [SerializeField]
    private AudioClip _emptyAmmoSound;
    private AudioSource _audioSource;

    private SpawnManager _spawnManager;
    private UIManager _uiManager;
    private SpriteRenderer _shieldSpriteRenderer;

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
        else
        {
            _spawnManager.UpdateLives(_lives);
        }

        if (_uiManager == null)
        {
            Debug.LogError("UI Manager is NULL");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio Source on the Player is NULL");
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
            if(_laserAmmo > 0f)
            {
                FireLaser();
            }
            else
            {
                _audioSource.clip = _emptyAmmoSound;
                _audioSource.Play();
            }
        }

        if(Input.GetKeyDown(KeyCode.R) && _shieldPulseAmmo > 0f)
        {
            ShieldPulse();
        }

    }


    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 directioncom = new Vector3(horizontalInput, verticalInput, 0);

        if(Input.GetKey(KeyCode.LeftShift)) //thruster boost being used
        {
            transform.Translate(directioncom * _speed * _thrusterBoost * Time.deltaTime);
            _uiManager.UpdateThrusterBoost(true);
            Debug.Log("Thruster On");
        }
        if(Input.GetKeyUp(KeyCode.LeftShift)) //thruster boost let go
        {
            _uiManager.UpdateThrusterBoost(false);
            Debug.Log("Thruster Off");
        }
        else //not thruster movement
        {
            transform.Translate(directioncom * _speed * Time.deltaTime);
        }

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -5, 2), 0);
       
        if (transform.position.x >= 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x <= -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = Time.time + _fireRate;
        if(_isTripleShotActive == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
        }
        else if(_isTripleShotActive == false)
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.1f, 0), Quaternion.identity);
            _laserAmmo--;
            _uiManager.UpdateShots(_laserAmmo);
        }

        _audioSource.Play();

    }

    public void Reload()
    {
        _laserAmmo = 15f;
        _uiManager.UpdateShots(_laserAmmo);
    }

    public void Heal()
    {
        _lives++;
        _uiManager.UpdateLives(_lives);
        _spawnManager.UpdateLives(_lives);

        if(_lives == 3)
        {
            _rightEngine.SetActive(false);
            _leftEngine.SetActive(false);
        }
        if(_lives == 2)
        {
            _leftEngine.SetActive(false);
        }
    }

    public void ShieldPulse()
    {
        Instantiate(_shieldPulsePrefab, transform.position, Quaternion.identity);
        _shieldPulseAmmo--;
        _uiManager.UpdateShieldPulse(_shieldPulseAmmo);
    }

    public void Damage()
    {

        if(_isShieldActive == true)
        {
            if(_shieldLives == 3)
            {
                _shieldLives--;
                _shieldSpriteRenderer.color = new Color32(255, 57, 250, 255);
                Debug.Log("Shield 3-2");
            }
            else if (_shieldLives == 2)
            {
                _shieldLives--;
                _shieldSpriteRenderer.color = Color.red;
                Debug.Log("Shield 2-1");
            }
            else if (_shieldLives == 1)
            {
                _shieldLives--;
                Debug.Log("Shield 1-0");
                _isShieldActive = false;
                _shieldPlayer.SetActive(false);
            }
            return;
        }

        _lives--;
        _uiManager.UpdateLives(_lives);
        _spawnManager.UpdateLives(_lives);

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
        _audioSource.clip = _tripleShotSound;
        StartCoroutine(TripleShotPowerDownRoutine());
    }


    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
        _audioSource.clip = _laserSound;
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
        Debug.Log("Shield at 3");

        _shieldSpriteRenderer = _shieldPlayer.GetComponent<SpriteRenderer>();

        if (_shieldSpriteRenderer == null)
        {
            Debug.LogError("Shield Renderer is NULL");
        }

        _shieldSpriteRenderer.color = Color.white;
        _shieldLives = 3;
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

}
