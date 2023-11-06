using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public GameObject sceneManager;
    public float playerSpeed = Constants.PLAYER_SPEED_INIT;
    public float directionalSpeed = Constants.DIRECTIONAL_SPEED;
    public AudioClip scoreUp;
    public AudioClip damage;
 //   private float rmbSpeed = Constants.PLAYER_SPEED_INIT;

    // if the variable is in camel case then when it's on the Inspector it will show as two words.

    // The demo has the player speed at 1500. My computer cannot deal with that but my phone we shall see.

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update()    {
        // update speed only when the Player is active (i.e. rigidBody is not freezeposition.z ) 
        //Debug.Log("GetComponent<Rigidbody>().constraints = " + GetComponent<Rigidbody>().constraints.ToString() + " RigidBodyConstraints.FreezePosition = " + RigidbodyConstraints.FreezePosition.ToString());
        playerSpeed = (GetComponent<Rigidbody>().constraints == RigidbodyConstraints.FreezePosition) ? Constants.PLAYER_SPEED_INIT : playerSpeed + Constants.SPEED_INCREMENT;

        // different things are needed on different platforms, and editor is different from android so:
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
        float moveHorizontal = Input.GetAxis("Horizontal");
        transform.position = Vector3.Lerp(
            gameObject.transform.position, 
            new Vector3(Mathf.Clamp(gameObject.transform.position.x + moveHorizontal, -(Constants.SIDE_MAX_MOVE), Constants.SIDE_MAX_MOVE), gameObject.transform.position.y, gameObject.transform.position.z),
            directionalSpeed * Time.deltaTime);
#endif
        // transform.position matches up with Inspector transform section position markers
        // Mathf.Clamp keeps it between a boundary. I don't know what the -2.5 and 2.5 mean but they are the boundary.
        //  Time.deltaTime is the time since last frame.
        GetComponent<Rigidbody>().velocity = Vector3.forward * playerSpeed * Time.deltaTime;
        transform.Rotate(Vector3.forward * GetComponent<Rigidbody>().velocity.z / 3);

        // mobile controls
        Vector2 touch = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0, 0, 10f));
        if (Input.touchCount > 0) {
            transform.position = new Vector3(touch.x, transform.position.y, transform.position.z);
        }
    }

    private void OnTriggerEnter(Collider other) {
        // all the collectible letters have a tag like lettera letterb etc
        if (other.gameObject.tag.StartsWith("letter")) {
            GetComponent<AudioSource>().PlayOneShot(scoreUp, 1.0f);
        }
        // the triangle tag is for the dangerous spikes
        if (other.gameObject.tag == "triangle") {
            GetComponent<AudioSource>().PlayOneShot(damage, 1.0f);
            StartCoroutine(TriggerGameOverCoroutine(other.transform.parent.gameObject));
        }
    }
    IEnumerator TriggerGameOverCoroutine(GameObject other) {
        // stop the player as soon as the killing collider is hit
        if (!other.name.StartsWith("triangles")) {
            Debug.Log("Destroyed object = " + other.name.ToString());
        }

        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        //Debug.Log("freeze all in triggerGameOverCoroutine has fired");

        // give it a sec so the player can accept their fate
        yield return new WaitForSeconds(0.5f);
        sceneManager.GetComponent<App_Initialize>().GameOver();

        //  Destroy the killing collider so you don't accidentally die immediately
        Destroy(other);
    }
}
