using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WearCollider : NetworkBehaviour
{
    GameTaskItem _task;

    public void Start()
    {
        _task = gameObject.GetComponentInParent<GameTaskItem>();
    }

    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl =
                other.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && _task)
        {
            GameColliderPackage gamePkg = new GameColliderPackage()
            {
                VRPlayerCtrl = ctrl,
                TaskItem = _task,
            };
            _task.OnTask?.Invoke(gamePkg);
        }      
    }

    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl =
                other.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && ctrl.WStatus == VRNetworkPlayerController.WearStatus.Wearing)
            ctrl.WStatus = VRNetworkPlayerController.WearStatus.NoWear;
    }
}
