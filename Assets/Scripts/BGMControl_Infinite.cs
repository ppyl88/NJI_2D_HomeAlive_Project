using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMControl_Infinite : MonoBehaviour
{
    AudioSource gameAudio;
    public AudioClip soundBGM;
    float fadeTime;
    float startVolume;
    bool trigOver = false;
    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
        gameAudio.clip = soundBGM;
        gameAudio.Play();
        fadeTime = 2f;
        startVolume = gameAudio.volume;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Player").GetComponent<PlayerCtrl_Infinite>().chkstate)
        {
            trigOver = true;
        }
        if (trigOver)
        {
            gameAudio.volume -= startVolume * Time.deltaTime / fadeTime;
            if (gameAudio.volume <= 0)
            {
                gameAudio.Stop();
                trigOver = false;
            }
        }
    }
}
