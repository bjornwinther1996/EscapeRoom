using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class HandCollision : MonoBehaviour
{
    public GameObject HeadTextObj;
    private FadeControl fadeControl;
    // Start is called before the first frame update
    void Start()
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) { return; }
        fadeControl = HeadTextObj.GetComponent<FadeControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (GetComponent<RealtimeTransform>().isOwnedLocallySelf)
        {
            if (other.CompareTag("LeverBack") || other.CompareTag("LeverFront") || other.CompareTag("LeverLeft") || other.CompareTag("LeverRight"))
            {
                if (gameObject.GetComponentInParent<PlayerData>()._isServer && other.GetComponent<LeverBehaviour>().PlayerLever == 2)
                {
                    fadeControl.SetText("Only your partner can activate this lever");
                }
                if (!gameObject.GetComponentInParent<PlayerData>()._isServer && other.GetComponent<LeverBehaviour>().PlayerLever == 1)
                {
                    fadeControl.SetText("Only your partner can activate this lever");
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) { return; }
        if (other.CompareTag("LeverBack") || other.CompareTag("LeverFront") || other.CompareTag("LeverLeft") || other.CompareTag("LeverRight"))
        {
            if (gameObject.GetComponentInParent<PlayerData>()._isServer && other.GetComponent<LeverBehaviour>().PlayerLever == 2)
            {
                fadeControl.SetText("Only your partner can activate this lever");
            }
            else if (!gameObject.GetComponentInParent<PlayerData>()._isServer && other.GetComponent<LeverBehaviour>().PlayerLever == 1)
            {
                fadeControl.SetText("Only your partner can activate this lever");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (GetComponent<RealtimeTransform>().isOwnedLocallySelf)
        {
            if (other.CompareTag("LeverBack") || other.CompareTag("LeverFront") || other.CompareTag("LeverLeft") || other.CompareTag("LeverRight"))
            {
                Debug.Log("Stopped touching lever");
                fadeControl.ClearText();
            }
        }
        else if (GetComponent<RealtimeTransform>().isOwnedRemotelySelf)
        {
            if (other.CompareTag("LeverBack") || other.CompareTag("LeverFront") || other.CompareTag("LeverLeft") || other.CompareTag("LeverRight"))
            {
                Debug.Log("Stopped touching lever");
                fadeControl.ClearText();
            }
        }

    }
}
