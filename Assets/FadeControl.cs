using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;
using UnityEngine.UI;
using TMPro;

public class FadeControl : MonoBehaviour
{

    public GameObject UIText;
    private TextMeshProUGUI textComponent;

    public GameObject ImageFadeCanvas;
    private Image imageComponent;
    private Color imageColor;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        imageComponent = ImageFadeCanvas.GetComponent<Image>();
        imageColor = Color.black;
        imageColor.a = 0f;
        imageComponent.color = imageColor;

        textComponent = UIText.GetComponent<TextMeshProUGUI>();

    }


    private void OnTriggerStay(Collider other)
    {

        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;

        if (other.gameObject.tag == "Wall")
        {
            //Debug.Log("HITTING WALL");
            FadeScreen();
            textComponent.SetText("Oops! Move out from the wall, fucking pig cheater!");
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;

        if (other.gameObject.tag == "Wall")
        {
            //Debug.Log("OnTriggerExit Wall");
            ClearFade();
            textComponent.SetText("");
        }
    }

    public void FadeScreen() // Fade screen by changing alpha value of black image
    {
        timer += Time.deltaTime;
        if (imageColor.a < 1f)
        {
            imageColor.a += (timer / 0.3f) * Time.deltaTime;
            imageComponent.color = imageColor;
        }
    }

    public void ClearFade() // set alpha to 0, to make black image transparent.
    {
        imageColor.a = 0;
        imageComponent.color = imageColor;
        timer = 0;
    }
}
