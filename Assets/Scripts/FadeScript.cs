using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{

    public Texture WhiteTexture;
    public Color FadeColor;

    public float FadeTime;
    private float currentTime;
    Color ColorLerp;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnGUI()
    {
        GUI.color = FadeColor;
        GUI.DrawTexture(new Rect(0,0,Screen.width,Screen.height), WhiteTexture);
    }
}
