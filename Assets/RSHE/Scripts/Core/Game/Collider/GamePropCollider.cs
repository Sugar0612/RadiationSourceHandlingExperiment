using System.Collections;
using System.Collections.Generic;
using System.Security.Principal;
using Mirror;
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
        NetworkPropsCollider propCollider =
            other.gameObject.GetComponentInParent<NetworkPropsCollider>();

        if (propCollider && _task)
        {
            EIdentity identity = propCollider.WhoHolding;

            int idx = _task.conditions.FindIndex(arg => arg.Identity == identity);
            if (idx != -1)
            {
                int porpidx = _task.conditions[idx].HoldingItems.FindIndex(arg => arg.pName == propCollider.PropName);
                if (porpidx != -1)
                {
                    _task.conditions[idx].HoldingItems[porpidx].pCount 
                        -= (_task.conditions[idx].HoldingItems[porpidx].pCount <= 0) ? 0 : 1;
                }
            }

            GameColliderPackage gamePkg = new GameColliderPackage() { TaskItem = _task };
            _task.OnTask?.Invoke(gamePkg);
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
