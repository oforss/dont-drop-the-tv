using UnityEngine;
using System.Collections;

public class Spawner : MonoBehaviour {
    public float spawnRangeMin = 2f;
    public float spawnRangeMax = 4.5f;

    public float minMoveSpeed = 3f;
    public float maxMoveSpeed = 5f;

    public float chanceForEgg = 1f;
    public float baseSpawnRate = 1.25f;

    public GameObject birdPrefab;

    float spawnRate;
    float spawnCooldown = 0f;

    float spawnPositionX;

    void Start() {
        float screenRatio = (float)Screen.width / Screen.height;
        spawnPositionX = screenRatio * Camera.main.orthographicSize + 2;

        spawnRate = baseSpawnRate;
    }

    void Update () {
        // SPAWN BIRDS

        spawnCooldown += Time.deltaTime;
        if(spawnCooldown >= spawnRate) {
            GameObject newBird = Instantiate(birdPrefab) as GameObject;
            BirdBehaviour bBehaviour = newBird.GetComponent<BirdBehaviour>();

            int dir = (int)(Mathf.Floor(Random.value * 2) * 2 - 1); // -1 or 1
            bBehaviour.direction = dir;

            if(Random.value < chanceForEgg) { bBehaviour.hasEgg = true; }

            float spawnPositionY = Random.value * (spawnRangeMax - spawnRangeMin) + spawnRangeMin;

            newBird.transform.position = new Vector3(-dir*spawnPositionX, spawnPositionY, 0);
            newBird.transform.parent = transform;

            bBehaviour.moveSpeed = Random.value * (maxMoveSpeed - minMoveSpeed) + minMoveSpeed;

            spawnRate = (float)(baseSpawnRate) * ( 2 / (2.0f + GameController.Level));

            spawnCooldown -= spawnRate;
            GameController.SpawnedBird();
        }

        // DESPAWN BIRDS

        for(int i = 0; i < transform.childCount; i++) {
            Transform c = transform.GetChild(i);
            BirdBehaviour bB = c.GetComponent<BirdBehaviour>();
            float absolutePosX = c.transform.position.x * bB.direction;
            
            if(absolutePosX > spawnPositionX) {
                Destroy(c.gameObject);
            }
        }
    }
}
