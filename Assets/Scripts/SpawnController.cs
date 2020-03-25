using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour {

    public float minHeight;
    public float maxHeight;
    public float maxSpawnRate;
    public int maxSpawnRocksNumber;
    public GameObject rockPrefab;
    public List<GameObject> rockList;

    private float currentSpawnRate;
    private GameManager GM;

    void Start() {
        currentSpawnRate = maxSpawnRate;
        rockList.InstantiateObjects(maxSpawnRocksNumber, rockPrefab);
        GM = GameManager.instance;
    }

    void Update() {
        if (GM.gameState != GameState.IN_GAME) {
            return;
        }

        currentSpawnRate += Time.deltaTime;

        if (currentSpawnRate > maxSpawnRate) {
            currentSpawnRate = 0;
            Spawn();
        }
    }

    void Spawn() {
        float randomHeight = Random.Range(minHeight, maxHeight);
        GameObject tempRock = rockList.ReturnActive();

        if (tempRock != null) {
            tempRock.transform.position = new Vector3(transform.position.x, randomHeight, transform.position.z);
            tempRock.SetActive(true);
        }
    }
}
