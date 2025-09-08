using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using TMPro;
using static UnityEngine.XR.Hands.XRHandSubsystemDescriptor;

public class GameInfoCollider : NetworkBehaviour
{
    public UserTaskInfoPanel InfoTextPanel;

    public UserTaskInfo UserTaskInfoList;

    [ServerCallback]
    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl =
            other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl)
        {
            string identity = ctrl.identity.ToString();
            RpcShowThisIdentityInfo(identity);
        }
    }

    [ServerCallback]
    public void OnTriggerExit(Collider other)
    {
        RpcOnTriggerExit();
    }

    #region Command Function

    [Command(requiresAuthority = false)]
    void CmdShowThisIdentityInfo(string identity)
    {
        RpcShowThisIdentityInfo(identity);
    }

    #endregion

    #region ClientRpc Function

    [ClientRpc]
    void RpcShowThisIdentityInfo(string identity)
    {
        ShowThisIdentityInfo(identity);
    }

    [ClientRpc]
    void RpcOnTriggerExit()
    {
        OnTriggerExitEvent();
    }

    #endregion

    void ShowThisIdentityInfo(string identity)
    {
        UserTaskInfoItem info = UserTaskInfoList.InfoList.Find(arg => arg.IndentityName == identity);
        if (info != null)
        {
            InfoTextPanel.SetActive(true);
            InfoTextPanel.SetPanelContent(info.IndentityName, info.Info);
        }
    }

    void OnTriggerExitEvent()
    {
        InfoTextPanel.SetActive(false);
        InfoTextPanel.SetPanelContent("", "");
    }
}
