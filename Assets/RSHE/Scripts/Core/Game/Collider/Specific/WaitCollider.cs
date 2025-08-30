using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitCollider : NetworkBehaviour
{
    GameTaskItem _task;

    public void Start()
    {
        _task = gameObject.GetComponentInParent<GameTaskItem>();
    }

    [ServerCallback]
    public void OnTriggerStay(Collider other)
    {
        VRNetworkPlayerController ctrl =
                other.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && _task && GameSteps.Get().isTopTask)
        {
            GameColliderPackage gamePkg = new GameColliderPackage()
            {
                VRPlayerCtrl = ctrl,
                TaskItem = _task,
            };
            _task.OnTask?.Invoke(gamePkg);
        }
    }

    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {
        
    }
}
