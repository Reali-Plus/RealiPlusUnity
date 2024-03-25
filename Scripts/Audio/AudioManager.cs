using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.mute = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayAudio()
    {
        audioSource.mute = false;
        audioSource.Play();
    }
}
