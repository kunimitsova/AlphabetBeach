using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour {

    public int score;
    public int highScore;
    public string word = "";
    public string lastWord = "";
    public TextMeshProUGUI scoreUI;
    public TextMeshProUGUI highScoreUI;
    public TextMeshProUGUI wordUI;
    public TextMeshProUGUI lastWordUI;

    private void Start() {
        // for when we want to remove all the test data
//        PlayerPrefs.DeleteAll();
        highScore = PlayerPrefs.GetInt("highscore");
        // last word does not persist so no playerpref
    }

    // Update is called once per frame
    void Update() {
        scoreUI.text = score.ToString();
        wordUI.text = word;
        highScoreUI.text = highScore.ToString();
        lastWordUI.text = word;
        if (score > highScore) {
            highScore = score;
            PlayerPrefs.SetInt("highscore", highScore);
        }
        // this seems like a bad idea since it updates every frame??
        // would it work if we set the persisted high score at GameOver?...
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag.StartsWith("letter")) {
            score++;
            AddToWord(other.gameObject.tag.Substring(6));
        }
    }

    void AddToWord(string letter) {
        string tmpWord = word;
        string tmp = "";
        int startInt = (tmpWord.Length < Constants.MAX_WORD_LENGTH) ? 0 : 1;
        // if the word is less than max word length, use the whole word before appending 'letter'
        // otherwise remove the first letter of 'word' then append 'letter'
        for (int i = startInt; i < tmpWord.Length; i++) {
            tmp = tmp + tmpWord[i];
        }
        tmp = tmp + letter;
        word = tmp;
        //Debug.Log("The word of the day is " + word);
    }
}
