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
    private Stopwatch stopwatch;

    private GameManagerData syncedGameVariables;

    private bool isPlatformsInstantiated; // maybe make public static?

    private bool incrementOnce;
    private bool runOnce;

    public static bool AmbientAudio;
    public bool AmbientAudioEnabled;


    void Start()
    {
        syncedGameVariables = GetComponent<GameManagerData>();
        PlatformManagerScript = PlatformManagerObject.GetComponent<PlatformManager>();
        stopwatch = GetComponent<Stopwatch>();

    }
    
    void Update()
    {

        if (!runOnce)
        {
            if (AmbientAudioEnabled == true)
            {
                AmbientAudio = true;
                runOnce = true;
            }
            else if (AmbientAudioEnabled == false)
            {
                AmbientAudio = false;
                runOnce = true;
            }
        }

        //if (!BoolFirstConnectedDevice()) { return; } // If the device is not the first connected device to the server - return
        
        CheckAndSetAvatarArray();
        AssignPlayerNumbers(); // also sets IsServer!
        if (CheckIfServerExist())
        {
            if (!IsServer) // Only do this for client
            {
                
            }

            // Only the server executes the following:
            if (!IsServer) { return; } // Only do the following if client is server:
            if (!CheckAllPlayersConnected()) { return; } // check if all players are connected, before realtime spawning objects that need to have local sceneObj as parent.
            if (!isPlatformsInstantiated)
            {
                PlatformManagerScript.RealtimeInstantiatePlatforms();
                PlatformManagerScript.SetRandomSequence(); // sync the sequence index int? 
                isPlatformsInstantiated = true;
                syncedGameVariables._backupBool = true; // Platforms Instantiated = true
            }

            if (Platform.NumberOfPlatformsDestroyed > 0) // IF FAIL:
            {
                PlatformManager.isResetFinished = false;
                syncedGameVariables._backupFloat = Platform.NumberOfPlatformsDestroyed; // To reset material
                PlatformManagerScript.DestroyAllSurfaces();
                PlatformManagerScript.ResetAllPlatforms(); // Resets: ((Vars:) Rowindex, ActivatedPlatformsInRow), PlatformActivated and 
                //PlatformManagerScript.StartCoroutine(PlatformManagerScript.EnableAllSurfaces(3)); // enable all surfaces again after X sec
                PlatformManagerScript.StartCoroutine(PlatformManagerScript.ActivateNextRowIE(PlatformManagerScript.RowIndex,3)); // enable all surfaces again after X sec
                PlatformManagerScript.StartCoroutine(PlatformManagerScript.SetRandomSequenceAfterXTime(5));
                PlatformManagerScript.StartCoroutine(PlatformManagerScript.ResetLocalPlatformVariables(7));
                PlatformManagerScript.StartCoroutine(PlatformManagerScript.ResetMaterial(8)); // Only for server // Also Sets IsResetFinished = true // if this timer is adjusted - remember to adjust for client accordingly (in Platform)
                //Debug.Log("GAME MANAGER FAIL CONDITION: " + PlatformManagerScript.RowIndex);
                Platform.NumberOfPlatformsDestroyed = 0;
            }
            else if(PlatformManager.isResetFinished) // RUNNING ALL THE TIME IF PLAYERS HAVE NOT FAILED:
            {
                //Debug.Log("GAME MANAGER - Row Index: " + PlatformManagerScript.RowIndex);
                PlatformManagerScript.ActivateNextRow(PlatformManagerScript.RowIndex);
                PlatformManagerScript.CheckCorrectPath(PlatformManagerScript.RowIndex);
            }

        }
        else
        {
            //if (!BoolFirstConnectedDevice()) { return; }
            AssignServer();
        }

    }

    public IEnumerator ResetNumberOfPlatformsDestroyed(float time)
    {
        yield return new WaitForSeconds(time);
        Platform.NumberOfPlatformsDestroyed = 0;
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
        Avatars[0].GetComponent<PlayerData>()._isServer = true; //isServer
    }

    bool CheckIfServerExist()
    {
        //Debug.Log("CheckIfServerExist Method Triggered");
        bool isServerExist = false;

        for (int i = 0; i < Avatars.Count; i++)
        {
            //Debug.Log("CheckIfServerExist: Avatars.count:" + Avatars.Count);
            RealtimeAvatar player = Avatars[i];

            if (player.gameObject.GetComponent<PlayerData>()._isServer) // If one of the avatars in the list _isServer = true, return true
            {
                isServerExist = true;
                //Debug.Log("CheckIfServerExist: IF statement triggered!");
            }

        }
        //Debug.Log("CheckIfServerExist: isServerExist: " + isServerExist);

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
                //IsServer = true; // correct to set here? - I would think so xD maybe better to set from playerscript?
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
     void FirstConnectedDevice() // not used
    {
        if (syncedGameVariables._backupBool == false)
        {
            firstConnected = true;
            syncedGameVariables._backupBool = firstConnected;
        }
    }

    bool BoolFirstConnectedDevice() // not used
    {
        if (syncedGameVariables._backupBool == false)
        {
            syncedGameVariables._backupBool = true;
            return true;
        }
        else
        {
            return false;
        }
    }
 */



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
