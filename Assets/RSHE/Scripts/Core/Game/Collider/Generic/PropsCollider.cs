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

    /// <summary> 是否已经被克隆过了 </summary>
    public bool isCloned = false;

    #endregion

    public void OnTriggerEnter(Collider other)
    {
        BodyPartInfo bodyInfo = other.gameObject.GetComponentInParent<BodyPartInfo>();
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        // 必须是手拿
        if (ctrl && bodyInfo && StaticGlobalVar.IsHand(bodyInfo))
        {
            WhoHolding = ctrl.identity;
        }
    }
    
    public void OnTriggerExit(Collider other)
    {
        BodyPartInfo bodyInfo = other.gameObject.GetComponentInParent<BodyPartInfo>();
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl && StaticGlobalVar.IsHand(bodyInfo))
        {
            WhoHolding = (ctrl.identity == WhoHolding) ? EIdentity.None : WhoHolding;
        }
    }
}
