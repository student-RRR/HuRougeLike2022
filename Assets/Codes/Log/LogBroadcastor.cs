using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogBroadcastor : MonoBehaviour
{
    public string LastestLog;
    private string tempLog = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        tempLog = LogServise.PlayLog();

        if (tempLog != null)
        {
            Debug.Log(tempLog);
            LastestLog = tempLog;
        }
        tempLog = null;

        
    }
}
