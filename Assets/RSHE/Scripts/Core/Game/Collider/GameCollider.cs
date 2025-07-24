using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class GameCollider : NetworkBehaviour
{
    GameTaskItem task;

    Dictionary<EIdentity, int> whoTriggerCollider = new Dictionary<EIdentity, int>();

    #region 游戏条件 & 触发模式
    public List<TaskCondition> conditionList = new List<TaskCondition>();
    public GameColliderTriggerMode trgMode = GameColliderTriggerMode.Single;
    #endregion

    bool checkIfItMet
    {
        get
        {
            bool isAllHere = false;
            if (trgMode == GameColliderTriggerMode.Multiplayer)
            {
                isAllHere = true;
                foreach (var executor in conditionList)
                {
                    if (whoTriggerCollider.ContainsKey(executor.identity))
                    {
                        isAllHere &= (whoTriggerCollider[executor.identity] == 1);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else
            {
                foreach (var executor in conditionList)
                {
                    if (whoTriggerCollider.ContainsKey(executor.identity) && whoTriggerCollider[executor.identity] == 1)
                    {
                        isAllHere = true;
                        break;
                    }
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
        Log.cinput("yellow", $"@@ GameCollider OnTriggerEnter.");
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl)
        {
            Log.cinput("yellow", $"@@ GameCollider Enter ctrl not null.");
            EIdentity identity = ctrl.identity;

            if (!whoTriggerCollider.ContainsKey(identity))
                whoTriggerCollider.Add(identity, 1);
            else
                whoTriggerCollider[identity] = 1;

            if (checkIfItMet)
            {
                task.EndTask.Invoke();
                GameSteps.Get().NextTask();
            }
        }
        else
        {
            Log.cinput("yellow", $"@@ GameColliderEnter func ctrl is null.");
        }
    }

    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();
        if (ctrl)
        {
            EIdentity identity = ctrl.identity;

            if (whoTriggerCollider.ContainsKey(identity))
                whoTriggerCollider[identity] = 0;
        }
    }
}
