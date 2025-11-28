using Mirror;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Unity.XR.PXR.ShapesRecognizer;

public class NetworkPropsCollider : NetworkBehaviour
{
    #region 道具信息

    /// <summary> 道具名字 </summary>
    public string PropName = "";

    /// <summary> 目前的持有人 </summary>
    public EIdentity WhoHeld = EIdentity.None;

    /// <summary> 是否已经被克隆过了 </summary>
    [SyncVar]
    public bool isCloned = false;

    /// <summary> 拿取道具时Transform </summary>
    public Transform HeldShapeTransform;

    /// <summary> Root node Transform </summary>
    public Transform RootTransform;

    #endregion

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        BodyPartInfo bodyInfo = other.gameObject.GetComponentInParent<BodyPartInfo>();
        VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        // 必须是手拿
        if (ctrl && bodyInfo && StaticGlobalVar.IsHand(bodyInfo))
        {
            RpcSetWhoHeld(ctrl.identity);
            //WhoHeld = ctrl.identity;
        }
    }

    [ClientRpc]
    void RpcSetWhoHeld(EIdentity identity)
    {
        WhoHeld = identity;
    }

    public void OnTriggerExit(Collider other)
    {
        //BodyPartInfo bodyInfo = other.gameObject.GetComponentInParent<BodyPartInfo>();
        //VRNetworkPlayerController ctrl = other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        //if (ctrl && StaticGlobalVar.IsHand(bodyInfo))
        //{
        //    WhoHeld = (ctrl.identity == WhoHolding) ? EIdentity.None : WhoHolding;
        //}
    }
}
