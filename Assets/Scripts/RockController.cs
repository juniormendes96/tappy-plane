using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockController : MonoBehaviour {

    public float movementSpeed;

    private GameManager GM;

    void Start() {
        GM = GameManager.instance;
    }

    void Update() {
        if (GM.gameState != GameState.IN_GAME) {
            return;
        }
        
        transform.position += new Vector3(movementSpeed, 0, 0) * Time.deltaTime;

        if (transform.position.x < -13.5) {
            gameObject.SetActive(false);
        }
    }
}
