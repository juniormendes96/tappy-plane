using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState { WAITING, IN_GAME, RANKING, PAUSED, GAME_OVER }

public class GameManager : MonoBehaviour {

    static GameManager _instance;

    Vector3 initialPlayerPosition;
    public GameObject player;
    public GameState gameState { get; private set; }

    // UI Objects
    public GameObject SCORE_UI;

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

        // ExtensionMethods.FindAndSetActive("_obstacles", typeof(GameObject), true);
    }

    public void TogglePause() {
        if (gameState == GameState.PAUSED) {
            Time.timeScale = 1;
            gameState = GameState.IN_GAME;
        } else {
            Time.timeScale = 0;
            gameState = GameState.PAUSED;
        }
    }

    public void EndGame() {
        Time.timeScale = 0;
        gameState = GameState.GAME_OVER;
    }

    public void Wait() {
        ResetPlayerPosition();
        SetKinematicRigidBody(true);
        gameState = GameState.WAITING;
    }

    public void AddPoints() {
        score++;
        SetScoreValue(score);
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
