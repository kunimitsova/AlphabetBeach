using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour {

    public GameObject player;
    public GameObject[] trianglePrefabs;
    public GameObject[] letterPrefabs;
    private Vector3 spawnObstaclePosition;
    private Vector3 spawnTrianglePosition;
    public int[] xPositions;

    // Update is called once per frame
    void Update() {
        float distanceToHorizon = Vector3.Distance(player.gameObject.transform.position, spawnObstaclePosition);
        if (distanceToHorizon < 120) {
            SpawnAllThings();
        }
    }

    void SpawnAllThings() {
        // starting at 30. letters spawn at 10 point intervals then after 20 triangles spawn.
        int dist = Constants.LETTER_SPAWN_DIST;
        if (spawnObstaclePosition.z < Constants.INITIAL_SPAWN_DIST) {
            dist = Constants.INITIAL_SPAWN_DIST;
        }
        switch (Random.Range(0, Constants.SPAWN_VARIATION)) {
            case 0:
                dist = spawnObstaclePosition.z < Constants.INITIAL_SPAWN_DIST ? Constants.INITIAL_SPAWN_DIST : Constants.TRIANGLE_SPAWN_DIST;
                SpawnTriangles(dist);
                break;
            default:
                dist = spawnObstaclePosition.z < Constants.INITIAL_SPAWN_DIST ? Constants.INITIAL_SPAWN_DIST : Constants.LETTER_SPAWN_DIST;
                SpawnLetters(dist);
                break;
        }
    }

    void SpawnTriangles(int DistFromPrev) {
        spawnObstaclePosition = new Vector3(0, 0, spawnObstaclePosition.z + DistFromPrev);
        Instantiate(trianglePrefabs[(Random.Range(0, trianglePrefabs.Length))], spawnObstaclePosition, Quaternion.identity); // Quaternion.identity is Game object's default rotation.
        //Instantiate(trianglePrefabs[(trianglePrefabs.Length)], spawnObstaclePosition, Quaternion.identity);  // for testing purposes
    }

    void SpawnLetters(int DistFromPrev) {
        spawnObstaclePosition = new Vector3(xPositions[(Random.Range(0, xPositions.Length))], 0, spawnObstaclePosition.z + DistFromPrev);
        Instantiate(letterPrefabs[(Random.Range(0, letterPrefabs.Length))], spawnObstaclePosition, transform.rotation * Quaternion.Euler(0f, 180f, 0f));
    }
}
