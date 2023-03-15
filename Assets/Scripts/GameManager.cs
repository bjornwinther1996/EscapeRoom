using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    public static GameObject Player1;
    public static GameObject Player2;

    public GameObject NetworkManager;
    public Dictionary<int, RealtimeAvatar> Avatars;
    static RealtimeAvatarManager Manager;
    public static bool IsServer = false;
    private bool firstConnected;

    public GameObject PlatformManagerObject;
    private PlatformManager PlatformManagerScript;

    private GameManagerData syncedGameVariables;

    private bool isPlatformsInstantiated; // maybe make public static?

    private bool incrementOnce;


    void Start()
    {
        syncedGameVariables = GetComponent<GameManagerData>();
        PlatformManagerScript = PlatformManagerObject.GetComponent<PlatformManager>();
    }
    
    void Update()
    {
        //if (!BoolFirstConnectedDevice()) { return; } // If the device is not the first connected device to the server - return
        
        CheckAndSetAvatarArray();
        //AssignServer();
        AssignPlayerNumbers(); // also sets IsServer!
        Debug.Log("Avatars Computer: " + Avatars.Count);
        Debug.Log("BackupInt Avatars: " + syncedGameVariables._backupInt);
        if (CheckIfServerExist())
        {
            //if (!CheckIfServerExist()) { return; }
            Debug.Log("Server Exists");

            if (!IsServer) { return; } // Only do the following if client is server:
            DebuggerVR.DebugMessage1 = "Avatars: " + Avatars.Count;
            //if (!CheckAllPlayersConnected()) { return; } // check if all players are connected, before realtime spawning objects that need to have local sceneObj as parent.
            Debug.Log("IS SERVER AND ALL CONNECTED");
            if (!isPlatformsInstantiated)
            {
                Debug.Log("IS SERVER AND PLATFORMINSTANTIATED");
                PlatformManagerScript.RealtimeInstantiatePlatforms();
                PlatformManagerScript.SetRandomSequence(); // sync the sequence index int? 
                isPlatformsInstantiated = true;
            }
            PlatformManagerScript.ActivateNextRow(PlatformManagerScript.RowIndex); // gettiing PlatformManagerScript.rowIndex fails?? no
            PlatformManagerScript.CheckCorrectPath(PlatformManagerScript.RowIndex);
        }
        else
        {
            Debug.Log("Server assigned");
            AssignServer();
        }


    }


    void FirstConnectedDevice()
    {
        if (syncedGameVariables._backupBool == false)
        {
            firstConnected = true;
            syncedGameVariables._backupBool = firstConnected;
        }
    }

    bool BoolFirstConnectedDevice()
    {
        if (syncedGameVariables._backupBool == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void CheckAndSetAvatarArray() // needs to be run only by server, to set avatar count!
    {
        // Will fuck with computer, as it will count it as avatar as well
        //if (Avatars.Count == 2) { return; } // only do the following if it doesn't have 2 avatars in its array of avatars. - CHECK THAT THIS WORKS
        //if(!firstConnected) { return;  }
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
        if (!BoolFirstConnectedDevice()) { return; }
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
                IsServer = true; // correct to set here? - I would think so xD maybe better to set from playerscript?
                if (!incrementOnce)
                {
                    syncedGameVariables._backupInt++;
                    incrementOnce = true;
                }
            }
            else
            {
                Player2 = player.gameObject;
                if (!incrementOnce)
                {
                    syncedGameVariables._backupInt++;
                    incrementOnce = true;
                }
            }

        }
    }

    bool CheckAllPlayersConnected()
    {
        if(syncedGameVariables._backupInt == 1) // was Avatars.Count 
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}

/*
 * if (Avatars.Count < 2)
        {
            return false;
            //DebuggerVR.DebugMessage2 = "False - Not enough avatars " + Avatars.Count;
        }
        else
        {
            //DebuggerVR.DebugMessage3 = "True - AllPlayersConnected  " + Avatars.Count;
            return true;
        }
*/
