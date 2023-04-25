using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using Normal.Realtime;
using System;

public class TelemetryData : MonoBehaviour
{
    public Dictionary<int, RealtimeAvatar> avatars;
    static string format = "Mddyyyyhhmmsstt";
    static string datetime = DateTime.Now.ToString(format);

    //Change following for Mac:
    string headPosPath1 = @"/Users/bjornwinther/Desktop/TelemetryData/1headPosPath" + datetime + ".txt";
    string headPosPath2 = @"/Users/bjornwinther/Desktop/TelemetryData/2headPosPath" + datetime + ".txt";

    void Start()
    {
        if (Application.platform == RuntimePlatform.Android) { return; }
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android) { return; } // only computer does following:
        avatars = GetComponent<GameManager>().Avatars;

        for (int i = 0; i < avatars.Count; i++) // maybe for each loop instead
        {
            RealtimeAvatar player = avatars[i];
            if (player.isOwnedLocallySelf) { return; }
            int playerNumber = player.gameObject.GetComponent<Player>().PlayerNumber;
            Debug.Log("PlayerNumber: " + playerNumber);

            Vector3 headPos = player.gameObject.transform.Find("Head").transform.position;
            //Vector3 leftHand = player.gameObject.transform.Find("Left Hand").transform.position;
            //Vector3 rightHand = player.gameObject.transform.Find("Right Hand").transform.position;


            //PlayerStat playerStat = player.gameObject.GetComponent<PlayerStat>();


            if (playerNumber == 1)
            {
                File.AppendAllText(headPosPath1, Time.time.ToString() + " : " + headPos.x + " : " + headPos.y + " : " + headPos.z + "\n");
            }
            if (playerNumber == 2)
            {
                File.AppendAllText(headPosPath2, Time.time.ToString() + " : " + headPos.x + " : " + headPos.y + " : " + headPos.z + "\n");
            }

        }
    }
}
