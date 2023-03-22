using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource audioSource;
    public AudioClip audioClip;

    public bool runOnce = true;
    public bool wasDone = false;

    public float timer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

        timer += Time.deltaTime;

        if (timer >= 3 && !wasDone) {
            runOnce = false;
            wasDone = true;
        }

        if (!runOnce)
        {
            if (GameManager.AmbientAudio == true)
            {
                audioSource.PlayOneShot(audioClip);
                runOnce = true;
            }
            else if (GameManager.AmbientAudio == false)
            {
                runOnce = true;
                return;
            }
        }


    }
}
