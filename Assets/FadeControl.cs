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
                imageColor.a += timer / 10;
                imageComponent.color = imageColor;
                //imageColor.a = timer / 10; // set alpha value of local color variable
                //imageComponent.color = imageColor; // set local color variable to the one used in Image. -- can't set imageComponent.color.a directly.
            }
        }
        else
        {
            Debug.Log("ELSE WALL");
            if (timer <= 0)
            {
                timer = 0;

                if (!callOnce)
                {
                    imageColor.a = 0;
                    imageComponent.color = imageColor;
                }
                callOnce = true;
                return;
            }
            else
            {
                timer -= Time.deltaTime;
                imageColor.a -= timer / 10; // set alpha value of local color variable
                imageComponent.color = imageColor;
                callOnce = false;
            }

        }

    }
}
