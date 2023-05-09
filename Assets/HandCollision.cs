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
        fadeControl = HeadTextObj.GetComponent<FadeControl>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
        if (other.CompareTag("LeverBack") || other.CompareTag("LeverFront") || other.CompareTag("LeverLeft") || other.CompareTag("LeverRight"))
        {
            if (gameObject.GetComponentInParent<PlayerData>()._isServer && other.GetComponent<LeverBehaviour>().PlayerLever == 2)
            {
                fadeControl.SetText("Only your partner can activate this lever");
            }
            else if (!gameObject.GetComponentInParent<PlayerData>() && other.GetComponent<LeverBehaviour>().PlayerLever == 1)
            {
                fadeControl.SetText("Only your partner can activate this lever");
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
        if (other.CompareTag("LeverBack") || other.CompareTag("LeverFront") || other.CompareTag("LeverLeft") || other.CompareTag("LeverRight"))
        {
            Debug.Log("Stopped touching lever");
            fadeControl.ClearText();
        }
    }
}
