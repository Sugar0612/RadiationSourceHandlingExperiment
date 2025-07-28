using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using UnityEngine;

/// <summary>
/// 推进步骤的Collider:
/// * GamePropCollider √
/// * GamePlayerCollider
public class GamePropCollider : NetworkBehaviour
{
    GameTaskItem _task;

    void Start()
    {
        _task = GetComponentInParent<GameTaskItem>();    
    }

    public void OnTriggerEnter(Collider other)
    {
        NetworkPropsCollider propCollider = other.gameObject.GetComponentInParent<NetworkPropsCollider>();

        if (propCollider && _task)
        {
            EIdentity identity = propCollider.WhoHolding;
            TaskCondition condition = _task.conditions.Find(x => x.identity == identity);

            GameColliderPackage gamePkg = new GameColliderPackage()
            {
                Condition = condition
            };

            if (condition != null)
            {
                _task.OnTask?.Invoke(gamePkg);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        //VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();
        //if (ctrl)
        //{

        //}
    }
}
