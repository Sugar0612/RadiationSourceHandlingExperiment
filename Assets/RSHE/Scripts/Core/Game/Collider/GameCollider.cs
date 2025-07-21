using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

public class GameCollider : MonoBehaviour
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
        EIdentity identity = other.gameObject.GetComponentInParent<VRPlayerController>().identity;

        BodyPartInfo bodyPart;
        bool getBodyPart = other.gameObject.TryGetComponent<BodyPartInfo>(out bodyPart);

        Log.cinput("yellow", $"Trigger Enter: identity: {identity.ToString()} and body part: {bodyPart.ePart.ToString()}");

        if (!whoTriggerCollider.ContainsKey(identity))
        {
            whoTriggerCollider.Add(identity, 0);
        }
      
        foreach (var condition in conditionList)
        {
            if (getBodyPart && identity == condition.identity && bodyPart.ePart == condition.bodyParts)
                whoTriggerCollider[identity] = 1;
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
        EIdentity identity = other.gameObject.GetComponentInParent<VRPlayerController>().identity;

        if (whoTriggerCollider.ContainsKey(identity)) 
            whoTriggerCollider[identity] = 0;
    }
}
