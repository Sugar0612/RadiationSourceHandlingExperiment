using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCollider : MonoBehaviour
{
    GameTaskItem task;
    
    void Start()
    {
        task = GetComponentInParent<GameTaskItem>();    
    }

    public void OnTriggerEnter(Collider other)
    {
        EIdentity identity = other.gameObject.GetComponent<VRPlayerController>().identity;

        Log.cinput("yellow", $"@@ GameCollider Trigger. player identity: {identity.ToString()}");

        foreach (var tar in task.executorsList)
            Log.cinput("yellow", $"@@@ {tar.ToString()}");
        
    }
}
