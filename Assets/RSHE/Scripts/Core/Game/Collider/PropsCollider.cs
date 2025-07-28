using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NetworkPropsCollider : NetworkBehaviour
{
    #region 道具信息

    /// <summary> 道具名字 </summary>
    public string PropName = "";

    /// <summary> 目前的持有人 </summary>
    public EIdentity WhoHolding = EIdentity.None;

    #endregion

    public void OnTriggerEnter(Collider other)
    {
        BodyPartInfo bodyInfo = other.gameObject.GetComponentInParent<BodyPartInfo>();
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl & bodyInfo.EPart == EBodyParts.Hand)
        {
            WhoHolding = ctrl.identity;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        BodyPartInfo bodyInfo = other.gameObject.GetComponentInParent<BodyPartInfo>();
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl & bodyInfo.EPart == EBodyParts.Hand)
        {
            WhoHolding = (ctrl.identity == WhoHolding) ? EIdentity.None : WhoHolding;
        }
    }
}
