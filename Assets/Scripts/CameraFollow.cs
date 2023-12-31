using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public GameObject player;
    public float cameraDistance = 10;

    void LateUpdate() {
        gameObject.transform.position = Vector3.Lerp(
            gameObject.transform.position, 
            new Vector3(0, gameObject.transform.position.y, player.gameObject.transform.position.z - cameraDistance), 
            Time.deltaTime * 100);
    }
}
