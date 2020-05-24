using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkUser : MonoBehaviour
{
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = Microphone.Start(Microphone.devices[0], true, 2, 44100);
        audioSource.Play();
    }
}
