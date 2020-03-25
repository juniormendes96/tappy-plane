using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float gravityForce;
    public float angleSpeed = 0.1f;
    public float forceSpeed = 0.1f;

    private Rigidbody2D playerRigidBody2D;
    private Animator playerAnimator;
    private GameManager GM;

    void Start() {
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        GM = GameManager.instance;
    }

    void Update() {
        if (Input.GetMouseButtonDown(0)) {
            if (GM.gameState == GameState.IN_GAME && GM.gameState != GameState.GAME_OVER) {
                HandleMouseClick();
            } else if (GM.gameState == GameState.WAITING) {
                GM.StartGame();
            }
        }
        ChangeAngle();
    }

    void OnCollisionEnter2D(Collision2D collision2D) {
        GM.EndGame();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.tag == "Point") {
            GM.AddPoints();
        }
    }

    private void HandleMouseClick() {
        playerRigidBody2D.velocity = Vector2.zero;
        playerRigidBody2D.AddForce(new Vector2(0, forceSpeed) * gravityForce);
    }

    private void ChangeAngle() {
        if (IsComingDown()) {
            Incline(-30, 0.1f);
        } else if (IsGoingUp()) {
            Incline(30, 1);
        }
    }

    private bool IsGoingUp() {
        return playerRigidBody2D.velocity.y > 0;
    }

    private bool IsComingDown() {
        return playerRigidBody2D.velocity.y < 0;
    }

    private void Incline(int degrees, float playerAnimatorSpeed) {
        var currentAngle = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        var newAngle = Quaternion.Euler(new Vector3(0, 0, degrees));
        transform.rotation = Quaternion.Slerp(currentAngle, newAngle, angleSpeed);
        playerAnimator.speed = playerAnimatorSpeed;
    }
}
