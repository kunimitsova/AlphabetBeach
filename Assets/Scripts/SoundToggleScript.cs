using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundToggleScript : MonoBehaviour
{
    public Toggle soundToggle;
    public Sprite musicOn; // shows when the audio is not muted
    public Sprite musicOff; // shows when the audio is muted

    public void MuteToggle(bool unMute) {
        if (unMute) {
            AudioListener.volume = 1;
            soundToggle.image.sprite = musicOn;
        }
        else {
            AudioListener.volume = 0;
            soundToggle.image.sprite = musicOff;
        }
    }
}
