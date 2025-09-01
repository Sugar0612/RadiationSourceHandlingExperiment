using Mirror;
using UnityEngine;

/// <summary> 教学模式 </summary>
public partial class TeachingAction : ActionBase
{
    [ClientRpc] public override void RpcStartAction_1(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().AutoRunStartTask(gamePkg, () =>
        {
            VRNetworkPlayerController[] controllerArray = FindObjectsOfType<VRNetworkPlayerController>();
            foreach (VRNetworkPlayerController ctrl in controllerArray)
            {
                if (!ctrl.isLocalPlayer)
                {
                    ctrl.Hat.SetRendererEnable(true);
                    ctrl.RightGlove.SetRendererEnable(true);
                    ctrl.LeftGlove.SetRendererEnable(true);
                    ctrl.Clothes.SetRendererEnable(true);
                    ctrl.Spectacles.SetRendererEnable(true);
                    ctrl.Clothes.SetRendererEnable(true);
                }
            }
        }); 
    }

    [ClientRpc] public override void RpcTaskAction_1(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_1(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_2(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().AutoRunStartTask(gamePkg, () => 
        {
            foreach (var go in SceneObjectManager.Get().OutSideFencesList)
            {
                go.SetActive<Renderer>(true);
            }
        }); 
    }

    [ClientRpc] public override void RpcTaskAction_2(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_2(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_3(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().AutoRunStartTask(gamePkg, () => 
        {
            foreach (var go in SceneObjectManager.Get().InSideFencesList)
            {
                go.SetActive<Renderer>(true);
            }
        });
    }

    [ClientRpc] public override void RpcTaskAction_3(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_3(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_4(GameColliderPackage gamePkg) { 
        CoreAction.Get().AutoRunStartTask(gamePkg, () => 
        {
            foreach (var go in SceneObjectManager.Get().FlagList)
            {
                go.SetActive<Renderer>(true);
            }
        }); 
    }

    [ClientRpc] public override void RpcTaskAction_4(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_4(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_5(GameColliderPackage gamePkg) 
    { 
        CoreAction.Get().AutoRunStartTask(gamePkg);

        Transporter[] transArray = FindObjectsOfType<Transporter>();
        foreach (var transp in transArray)
        {
            NetworkPropsCollider prop = transp.GetComponent<NetworkPropsCollider>();
            if (prop && prop.isCloned == false)
            {
                transp.ActionAnimator?.SetBool("goPointOne", true);
                transp.ActionAnimator?.SetBool("goBackOne", false);
                transp.LocalActiveUI(false);
                StartCoroutine(transp.CoverOpenAndClose(20.0f, 12.0f));
            }
        }

        RadiationSource[] radArray = FindObjectsOfType<RadiationSource>();
        foreach (RadiationSource rad in radArray)
        {
            if (rad.RName == "One")
            {
                StartCoroutine(rad.PickupAllObject(30.0f));
            }
        }
    }

    [ClientRpc] public override void RpcTaskAction_5(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_5(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_6(GameColliderPackage gamePkg) { CoreAction.Get().AutoRunStartTask(gamePkg); }

    [ClientRpc] public override void RpcTaskAction_6(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_6(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_7(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().AutoRunStartTask(gamePkg);

        Transporter[] transArray = FindObjectsOfType<Transporter>();
        foreach (var transp in transArray)
        {
            NetworkPropsCollider prop = transp.GetComponent<NetworkPropsCollider>();
            if (prop && prop.isCloned == true)
            {
                transp.ActionAnimator?.SetBool("goPointOne", false);
                transp.ActionAnimator?.SetBool("goBackOne", true);
                transp.LocalActiveUI(false);
            }
        }
    }

    [ClientRpc] public override void RpcTaskAction_7(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_7(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_8(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().AutoRunStartTask(gamePkg);

        Transporter[] transArray = FindObjectsOfType<Transporter>();
        foreach (var transp in transArray)
        {
            NetworkPropsCollider prop = transp.GetComponent<NetworkPropsCollider>();
            if (prop && prop.isCloned == false)
            {
                transp.ActionAnimator?.SetBool("goPointTwo", true);
                transp.ActionAnimator?.SetBool("goBackTwo", false);
                transp.LocalActiveUI(false);
                StartCoroutine(transp.CoverOpenAndClose(19.0f, 12.0f));
            }
        }

        RadiationSource[] radArray = FindObjectsOfType<RadiationSource>();
        foreach (RadiationSource rad in radArray)
        {
            if (rad.RName == "Two")
            {
                StartCoroutine(rad.PickupAllObject(31.0f));
            }
        }
    }

    [ClientRpc] public override void RpcTaskAction_8(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_8(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_9(GameColliderPackage gamePkg) { CoreAction.Get().AutoRunStartTask(gamePkg); }

    [ClientRpc] public override void RpcTaskAction_9(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_9(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_10(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().AutoRunStartTask(gamePkg);

        Transporter[] transArray = FindObjectsOfType<Transporter>();
        foreach (var transp in transArray)
        {
            NetworkPropsCollider prop = transp.GetComponent<NetworkPropsCollider>();
            if (prop && prop.isCloned == true)
            {
                StartCoroutine(transp.CoverOpenAndClose(6.0f, 7.0f));
            }
        }

        RadiationSource[] radArray = FindObjectsOfType<RadiationSource>();
        foreach (RadiationSource rad in radArray)
        {
            if (rad.RName == "Two")
            {
                StartCoroutine(rad.ShovelAllObject(12.0f));
            }
        }
    }

    [ClientRpc] public override void RpcTaskAction_10(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_10(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_11(GameColliderPackage gamePkg) { CoreAction.Get().AutoRunStartTask(gamePkg); }

    [ClientRpc] public override void RpcTaskAction_11(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_11(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_12(GameColliderPackage gamePkg) 
    {
        CoreAction.Get().AutoRunStartTask(gamePkg);
        Transporter[] transArray = FindObjectsOfType<Transporter>();
        foreach (var transp in transArray)
        {
            NetworkPropsCollider prop = transp.GetComponent<NetworkPropsCollider>();
            if (prop && prop.isCloned == true)
            {
                transp.ActionAnimator?.SetBool("goPointTwo", false);
                transp.ActionAnimator?.SetBool("goBackTwo", true);
                transp.LocalActiveUI(false);
            }
        }
    }

    [ClientRpc] public override void RpcTaskAction_12(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_12(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartAction_13(GameColliderPackage gamePkg) { CoreAction.Get().AutoRunStartTask(gamePkg); }

    [ClientRpc] public override void RpcTaskAction_13(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndAction_13(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcStartActionWait(GameColliderPackage gamePkg) { CoreAction.Get().AutoRunStartTask(gamePkg); }

    [ClientRpc] public override void RpcTaskActionWait(GameColliderPackage gamePkg) { }

    [ClientRpc] public override void RpcEndActionWait(GameColliderPackage gamePkg) { }
}
