using System.Collections;
using UnityEngine;

namespace Assets.Scripts {
    public class Destroyer : MonoBehaviour {

        private GameObject player;

        // Use this for initialization
        void Start() {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        // Update is called once per frame
        void Update() {
            if (gameObject.transform.position.z < player.transform.position.z - 15) {
                Destroy(gameObject);
            }
        }
    }
}