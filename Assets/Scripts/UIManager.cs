﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    //handle to Text
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartText;
 
    [SerializeField]
    private Text _shotsText;

    [SerializeField]
    private Image _LivesImg;
    [SerializeField]
    private Sprite[] _liveSprites;
    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _shotsText.text = "Ammo: " + 15;
        _gameoverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _shotsText.GetComponent<Text>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprites[currentLives];
    }

    public void UpdateShots(float currentShots)
    {
        _shotsText.text = "Ammo: " + currentShots;
        _shotsText.color = new Color(1.0f, (0.6f * currentShots), 0.0f);

    }

    public void GameOverSequence()
    {
        StartCoroutine(GameOverFlickerRoutine());
        _restartText.gameObject.SetActive(true);
        _gameManager.GameOver();
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while(true)
        {
            _gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }

    }
}
