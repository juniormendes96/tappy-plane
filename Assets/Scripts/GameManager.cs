﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameState { WAITING, IN_GAME, RANKING, PAUSED, GAME_OVER }

public class GameManager : MonoBehaviour {

    static GameManager _instance;

    Vector3 initialPlayerPosition;
    public GameObject player;
    public GameObject pausePanel;
    public GameObject gameOverPanel;
    public GameObject creditsPanel;

    public GameObject SCORE_UI;

    public GameState gameState { get; private set; }

    public Text finalScore;

    int score;

    public static GameManager instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType(typeof(GameManager)) as GameManager;

                if (_instance == null) {
                    var go = new GameObject("_gameManager");
                    DontDestroyOnLoad(go);
                    _instance = go.AddComponent<GameManager>();
                    _instance.player = GameObject.FindGameObjectWithTag("Player");
                }
            }

            return _instance;
        }
    }

    void Start() {
        Wait();
        initialPlayerPosition = player.GetComponent<Transform>().position;
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            TogglePause();
        }
    }

    public void StartGame() {
        ResetPlayerPosition();
        SetKinematicRigidBody(false);
        gameState = GameState.IN_GAME;
        GameReady();
    }

    public void RestartGame() {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void TogglePause() {
        if (gameState == GameState.PAUSED) {
            Time.timeScale = 1f;
            gameState = GameState.IN_GAME;
            pausePanel.SetActive(false);
            FindObjectOfType<AudioManager>().backgroundMusic.Play();
        } else {
            Time.timeScale = 0f;
            gameState = GameState.PAUSED;
            pausePanel.SetActive(true);
            FindObjectOfType<AudioManager>().backgroundMusic.Pause();
        }
    }

    public void EndGame() {
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        finalScore.text = "Pontuação\n" + score;
        gameState = GameState.GAME_OVER;
        FindObjectOfType<AudioManager>().backgroundMusic.Stop();
        FindObjectOfType<AudioManager>().gameOverSound.Play();
    }

    public void Wait() {
        ResetPlayerPosition();
        SetKinematicRigidBody(true);
        gameState = GameState.WAITING;
    }

    public void ToggleCreditsPanel() {
        creditsPanel.SetActive(!creditsPanel.activeSelf);
    }

    public void QuitApplication() {
        Application.Quit();
    }

    public void AddPoints() {
        score++;
        SetScoreValue(score);
        FindObjectOfType<AudioManager>().mountainSound.Play();
    }

    public void SetScoreValue(int value) {
        score = value;

        var scoreText = SCORE_UI.FindChildObject<Text>("Score");
        scoreText.text = value.ToString();
    }

    public void GameReady() {
        SCORE_UI.SetActive(true);
    }

    private void ResetPlayerPosition() {
        player.transform.eulerAngles = new Vector3(0, 0, 0);
        player.GetComponent<Transform>().position = initialPlayerPosition;
        player.SetActive(true);
    }

    private void SetKinematicRigidBody(bool isKinematic) {
        player.GetComponent<Rigidbody2D>().isKinematic = isKinematic;
        return;
    }
}
