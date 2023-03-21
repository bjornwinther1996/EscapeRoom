using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.AmbientAudio == true)
        {
            audioSource.Play();
        }
        else if (GameManager.AmbientAudio == false)
        {
            return;
        }
    }
}
