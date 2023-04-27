using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetText : MonoBehaviour
{
    private LeverData syncedLeverData;
    private TextMeshPro textObj;
    // Start is called before the first frame update
    void Start()
    {
        syncedLeverData = GetComponent<LeverData>();
        textObj = GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.IsServer)
        {
            textObj.SetText("Levers Pulled: " +syncedLeverData._leversPulled);
        }
    }
}
