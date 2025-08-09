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

    public void OnTriggerEnter(Collider other)
    {
        VRNetworkPlayerController ctrl =
            other.gameObject.GetComponentInParent<VRNetworkPlayerController>();

        if (ctrl)
        {
            string identity = ctrl.identity.ToString();

            UserTaskInfoItem info = UserTaskInfoList.InfoList.Find(arg => arg.IndentityName == identity);
            if (info != null)
            {
                InfoTextPanel.SetActive(true);
                InfoTextPanel.SetPanelContent(info.IndentityName, info.Info);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        InfoTextPanel.SetActive(false);
        InfoTextPanel.SetPanelContent("", "");
    }
}
