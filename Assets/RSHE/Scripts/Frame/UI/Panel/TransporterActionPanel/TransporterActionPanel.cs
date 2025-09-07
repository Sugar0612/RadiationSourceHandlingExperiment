using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransporterActionPanel : NetworkBehaviour
{
    public Button OpenButton;

    public Button CloseButton;

    #region Command function
    [Command (requiresAuthority = false)]
    public void CmdOpenButtonClicked()
    {
        RpcOpenButtonClicked();
    }

    [Command(requiresAuthority = false)]
    public void CmdCloseButtonClicked()
    {
        RpcCloseButtonClicked();
    }

    #endregion

    #region ClientRpc function
    [ClientRpc]
    public void RpcOpenButtonClicked()
    {
        OpenButtonClicked();
    }

    [ClientRpc]
    public void RpcCloseButtonClicked()
    {
        CloseButtonClicked();
    }

    #endregion

    public void OpenButtonClicked()
    {
        OpenButton.SetButtonActive(false);
        CloseButton.SetButtonActive(true);
    }


    public void CloseButtonClicked()
    {
        OpenButton.SetButtonActive(true);
        CloseButton.SetButtonActive(false);
    }

    void CloseAll()
    {
        OpenButton.SetButtonActive(false);
        CloseButton.SetButtonActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<TextMeshProUGUI>(active);
        if (active)
        {
            CloseButtonClicked();
        }
        else
        {
            CloseAll();
        }
    }
}
