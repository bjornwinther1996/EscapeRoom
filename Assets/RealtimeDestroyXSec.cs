using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Normal.Realtime;

public class RealtimeDestroyXSec : MonoBehaviour
{
    public float TimeToDestroy;
    float Timer;
    bool ObjDestroyed;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.IsServer) { return; }
        if (ObjDestroyed) { return; }
        Timer += Time.deltaTime;
        if (Timer < TimeToDestroy) { return; }
        Realtime.Destroy(gameObject);
        ObjDestroyed = true;

    }
}
