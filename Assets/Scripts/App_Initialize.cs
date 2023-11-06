using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class App_Initialize : MonoBehaviour {

    public GameObject inMenuUI;
    public GameObject inGameUI;
    public GameObject gameOverUI;
    public GameObject creditsUI;
    public GameObject pauseUI;
    public GameObject adButton;
    public GameObject restartButton;
    public GameObject player;
    public GameObject adsManager;
    public static bool hasSeenRewardAd = false;
    private bool hasGameStarted = false;
    private bool isPaused;

    void Awake () {
        Shader.SetGlobalFloat("_Curvature", 2.0f);
        Shader.SetGlobalFloat("_Trimming", 0.1f);
        Application.targetFrameRate = 60;

    }

    // Start is called before the first frame update
    void Start() {
        isPaused = false;
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        inMenuUI.gameObject.SetActive(true);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(false);
        pauseUI.gameObject.SetActive(false);
    }

    public void PlayButton() {
        float waitTime = (hasGameStarted) ? Constants.WAIT_RESUMEGAME : Constants.WAIT_STARTGAME;
        StartGame(waitTime); 
    }

    public void PauseGame() {
        isPaused = !isPaused;
        pauseUI.gameObject.SetActive(isPaused);
        hasGameStarted = true;
        if (isPaused) {
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        } else {
            StartGame(Constants.WAIT_RESUMEGAME);
        }
    }

    public void GameOver() {
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePosition;
        //Debug.Log("freeze position in App_Innitialize.GameOver has fired");
        hasGameStarted = true;
        inMenuUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(true);
        if (hasSeenRewardAd == true) {
            adButton.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
            adButton.GetComponent<Button>().enabled = false;
            adButton.GetComponent<Animator>().enabled = false;
            restartButton.GetComponent<Animator>().enabled = true;
        }
    }

    public void RestartGame() {
        SceneManager.LoadScene(0); // 0 is the index of our "Main" scene which is our only scene.
        hasSeenRewardAd = false;
    }

    public void StartGame(float waitTime) {
        StartCoroutine(StartGameCoroutine(waitTime));
    }
    // IEnumerator is a coroutine
    IEnumerator StartGameCoroutine(float waitTime) {
        inMenuUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(true);
        gameOverUI.gameObject.SetActive(false);
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //Debug.Log("Rigidbodyconstraints.None has fired (StartGameCoroutine)");
        player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezePositionY;
    }

    public void ShowCredits() {
        inMenuUI.gameObject.SetActive(false);
        inGameUI.gameObject.SetActive(false);
        gameOverUI.gameObject.SetActive(false);
        creditsUI.gameObject.SetActive(true);
    }

    public void AdButtonClick() {

    }
}
