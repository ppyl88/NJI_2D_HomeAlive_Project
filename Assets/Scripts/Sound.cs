using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound : MonoBehaviour
{
    AudioSource gameAudio;
    public AudioClip[] sounds;

    // Start is called before the first frame update
    void Start()
    {
        gameAudio = GetComponent<AudioSource>();
    }

    public void SoundPlay(int soundNumber)
    {
        gameAudio.PlayOneShot(sounds[soundNumber]);
    }

}
