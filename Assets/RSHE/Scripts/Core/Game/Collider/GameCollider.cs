using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class GameCollider : MonoBehaviour
{
    GameTaskItem task;

    Dictionary<EIdentity, int> whoTriggerCollider = new Dictionary<EIdentity, int>();

    bool checkIfItMet
    {
        get 
        {
            bool isAllHere = true;
            foreach (var executor in task.executorsList)
            {
                if (whoTriggerCollider.ContainsKey(executor))
                {
                    isAllHere &= (whoTriggerCollider[executor] == 1);
                }
                else
                {
                    return false;
                }
            }
            return isAllHere;
        }
    }

    void Start()
    {
        task = GetComponentInParent<GameTaskItem>();    
    }

    public void OnTriggerEnter(Collider other)
    {
        EIdentity identity = other.gameObject.GetComponent<VRPlayerController>().identity;

        Log.cinput("yellow", $"@@ OnTriggerEnter. {identity}");
        if (!whoTriggerCollider.ContainsKey(identity))
        {
            Log.cinput("yellow", $"@@ whoTriggerCollider.Add. {identity}");
            whoTriggerCollider.Add(identity, 1);
        }

        if (checkIfItMet)
        {
            Log.cinput("yellow", $"@@@ checkIfItMet");
            task.EndTask.Invoke();
            GameSteps.Get().NextTask();
        }
    }

    public void OnTriggerExit(Collider other)
    {
        EIdentity identity = other.gameObject.GetComponent<VRPlayerController>().identity;

        if (whoTriggerCollider.ContainsKey(identity)) 
            whoTriggerCollider[identity] = 0;
    }
}
