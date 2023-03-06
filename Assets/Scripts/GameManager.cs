using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameManager : MonoBehaviour
{
    public Dictionary<int, RealtimeAvatar> Avatars;
    static RealtimeAvatarManager Manager;

    [SerializeField]
    public static GameObject Player1;
    public static GameObject Player2;

    public GameObject NetworkManager;
    public static bool IsServer = false;

    void Start()
    {
        
    }
    /*
    void Update()
    {
        CheckAndSetAvatarArray();
        AssignServer();

        if (CheckIfServerExist())
        {

        }

    }

    public void CheckAndSetAvatarArray()
    {
        if (Avatars.Count == 2) { return; } // only do the following if it doesn't have 2 avatars in its array of avatars. - CHECK THAT THIS WORKS
        if (Manager == null)
        {
            Manager = NetworkManager.GetComponent<RealtimeAvatarManager>();

        }
        else
        {
            Avatars = Manager.avatars;

        }

        if (Avatars == null)
        {
            return;
        }
    }

    void AssignServer()
    {
        if (Avatars.Count == 0) { return; }
        avatars[0].GetComponent<PlayerStat>()._backupVariable1 = true; //isServer
    }

    bool CheckIfServerExist()
    {
        bool isServerExist = true;

        for (int i = 0; i < avatars.Count; i++)
        {
            RealtimeAvatar player = avatars[i];

            if (!player.gameObject.GetComponent<PlayerStat>()._backupVariable1) //isServer
            {
                isServerExist = false;
            }

        }

        return isServerExist;
    }

    void AssignPlayerNumbers()
    {
        for (int i = 0; i < Avatars.Count; i++)
        {
            RealtimeAvatar player = Avatars[i];

            if (player.gameObject.GetComponent<PlayerStat>()._backupVariable1) //isServer
            {
                player1 = player.gameObject;
                isServer = true; // correct to set here?
            }
            else
            {
                player2 = player.gameObject;
            }

        }
    }*/
}
