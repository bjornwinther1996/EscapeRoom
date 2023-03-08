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

    public GameObject PlatformManagerObject;
    private PlatformManager PlatformManagerScript;

    private GameManagerData syncedGameVariables;

    private bool platformsInstantiated; // maybe make public static?



    void Start()
    {
        syncedGameVariables = GetComponent<GameManagerData>();
        PlatformManagerScript = PlatformManagerObject.GetComponent<PlatformManager>();
    }
    
    void Update()
    {
        CheckAndSetAvatarArray();
        AssignServer();

        if (!CheckIfServerExist()) { return; }

        AssignPlayerNumbers();


        if (IsServer) // Only do the following if client is server: 
        {
            if (!platformsInstantiated)
            {
                PlatformManagerScript.RealtimeInstantiatePlatforms();
                PlatformManagerScript.SetParentForPlatform();
                PlatformManagerScript.SetRandomSequence(); // sync the sequence index int? 
                platformsInstantiated = true;
            }
            PlatformManagerScript.ActivateNextRow(PlatformManagerScript.rowIndex);
            PlatformManagerScript.CheckCorrectPath(PlatformManagerScript.rowIndex);
        }
        else // if not server:
        {
            if (platformsInstantiated)
            {
                PlatformManagerScript.SetParentForPlatform();
            }
        }

    }

    public void CheckAndSetAvatarArray()
    {
        // Will fuck with computer, as it will count it as avatar as well
        //if (Avatars.Count == 2) { return; } // only do the following if it doesn't have 2 avatars in its array of avatars. - CHECK THAT THIS WORKS
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
        Avatars[0].GetComponent<PlayerData>()._isServer = true; //isServer
    }

    bool CheckIfServerExist()
    {
        bool isServerExist = true;

        for (int i = 0; i < Avatars.Count; i++)
        {
            RealtimeAvatar player = Avatars[i];

            if (!player.gameObject.GetComponent<PlayerData>()._isServer) //isServer
            {
                isServerExist = false;
            }

        }

        return isServerExist;
    }

    void AssignPlayerNumbers() // make condition so it isn't checked and assigned all the time.
    {
        for (int i = 0; i < Avatars.Count; i++)
        {
            RealtimeAvatar player = Avatars[i];

            if (player.gameObject.GetComponent<PlayerData>()._isServer) //isServer
            {
                Player1 = player.gameObject;
                IsServer = true; // correct to set here? - I would think so xD
            }
            else
            {
                Player2 = player.gameObject;
            }

        }
    }
}
