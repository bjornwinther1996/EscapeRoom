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
    public GameObject PlayerObject;
    private PlayerData syncedPlayerData;

    float timer;
    // Start is called before the first frame update
    void Start()
    {
        imageComponent = ImageFadeCanvas.GetComponent<Image>();
        imageColor = Color.black;
        imageColor.a = 0f;
        imageComponent.color = imageColor;
        syncedPlayerData = PlayerObject.GetComponent<PlayerData>();

        textComponent = UIText.GetComponent<TextMeshProUGUI>();

    }


    private void OnTriggerStay(Collider other)
    {

        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;

        if (other.gameObject.tag == "Wall")
        {
            //Debug.Log("HITTING WALL");
            FadeScreen();
            textComponent.SetText("Move your head out of the wall!");
        }
        if (other.gameObject.name == "Player1AreaHitbox")
        {
            if (syncedPlayerData._isServer) // If Player 1
            {
                ClearText();
            }
            else
            {
                textComponent.SetText("Move to the Yellow mist");
            }
        }
        if (other.gameObject.name == "Player2AreaHitbox")
        {
            if (!syncedPlayerData._isServer) // If Player 2
            {
                ClearText();
            }
            else
            {
                textComponent.SetText("Move to the Blue mist");
            }
        }
        if (other.gameObject.CompareTag("Elevator"))
        {
            if (other.GetComponentInParent<ElevatorData>()._goUp)
            {
                textComponent.SetText("Please stand still while elevator is moving");
            }
            else
            {
                ClearText();
            }
        }

        if (other.gameObject.CompareTag("WinCollider"))
        {
            FadeWhite();
            SetTextWithColor("Congratulations! Take off your headset", Color.black);
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;

        if (other.gameObject.tag == "Wall")
        {
            //Debug.Log("OnTriggerExit Wall");
            ClearFade();
            ClearText();
        }
        if (other.gameObject.CompareTag("WinCollider"))
        {
            ClearText();
            other.gameObject.GetComponentInChildren<AudioSource>().Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!GetComponent<RealtimeTransform>().isOwnedLocallySelf) return;
        if (other.gameObject.name == "HeavenHitbox")
        {
            ClearFade();
            ClearText();
        }

        if (other.gameObject.CompareTag("WinCollider"))
        {
            other.gameObject.GetComponentInChildren<AudioSource>().Play();
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

    public void FadeWhite()
    {
        if(imageColor.r != Color.white.r && imageColor.g != Color.white.g && imageColor.b != Color.white.b)
        {
            imageColor.r = Color.white.r;
            imageColor.g = Color.white.g;
            imageColor.b = Color.white.b;
        }
        timer += Time.deltaTime;
        if (imageColor.a < 1f)
        {
            imageColor.a += (timer / 4f) * Time.deltaTime;
            imageComponent.color = imageColor;
        }
    }

    public void ClearFade() // set alpha to 0, to make black image transparent.
    {
        imageColor.a = 0;
        imageComponent.color = imageColor;
        timer = 0;
    }

    public void ClearText()
    {
        textComponent.SetText("");
    }

    public void SetText(string text)
    {
        textComponent.SetText(text);
    }
    public void SetTextWithColor(string text, Color color)
    {
        textComponent.color = color;
        textComponent.SetText(text);
    }
}
