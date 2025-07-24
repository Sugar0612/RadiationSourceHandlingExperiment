using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkPropsCollider : NetworkBehaviour
{
    #region 道具信息
    /// <summary> 道具名字 </summary>
    public string propName = "";

    /// <summary> 目前的持有人 </summary>
    public EIdentity whoHolding = EIdentity.None;
    #endregion

    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();
        if (ctrl)
        {
            whoHolding = ctrl.identity;
            if (!ctrl.isLocalPlayer)
            {
                ctrl.hat.SetGameObjectMeshActive(true);
                ctrl.clothes.SetGameObjectMeshActive(true);
            }
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();
        if (ctrl)
        {
            whoHolding = (ctrl.identity == whoHolding) ? EIdentity.None : whoHolding;
        }
    }
}
