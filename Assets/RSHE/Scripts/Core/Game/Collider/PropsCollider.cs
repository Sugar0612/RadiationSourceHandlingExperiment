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

            // TODO..
            if (!ctrl.isLocalPlayer)
            {
                ctrl.hat.SetRendererEnable(true);
                ctrl.clothes.SetRendererEnable(true);
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
