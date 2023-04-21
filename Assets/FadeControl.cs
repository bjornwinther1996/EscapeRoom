using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.UI;

public class FadeControl : MonoBehaviour
{

    public GameObject UICanvas;

    public GameObject ImageFadeCanvas;
    private Image imageComponent;
    private Color imageColor;
    private bool callOnce;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        imageComponent = ImageFadeCanvas.GetComponent<Image>();
        imageColor = Color.black;
        imageColor.a = 0f;
        imageComponent.color = imageColor;

    }


    private void OnTriggerStay(Collider other)
    {

        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;

        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("HITTING WALL");
            timer += Time.deltaTime;
            if (imageColor.a < 1f)
            {
                imageColor.a += (timer / 0.5f) *Time.deltaTime;
                imageComponent.color = imageColor;
            }
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;

        if (other.gameObject.tag == "Wall")
        {
            Debug.Log("OnTriggerExit Wall");
            imageColor.a = 0;
            imageComponent.color = imageColor;
            timer = 0;
        }
    }
}
