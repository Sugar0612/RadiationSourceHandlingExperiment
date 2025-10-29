using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Transporter : NetworkBehaviour
{
    /// <summary> 背景图片 </summary>
    public Image BGImage;

    /// <summary> 到达Text </summary>
    public TMP_Text ArriveText;

    /// <summary> 去放射源一 </summary>
    public Button GoPointOneButton;

    /// <summary> 去放射源二 </summary>
    public Button GoPointTwoButton;

    /// <summary> 一号线回收 </summary>
    public Button GoBackButtonOne;

    /// <summary> 二号线回收 </summary>
    public Button GoBackButtonTwo;

    /// <summary> 打开盖子 </summary>
    public Button OpenButton;

    /// <summary> 关闭盖子 </summary>
    public Button CloseButton;

    /// <summary> 行为面板 </summary>
    public TransporterActionPanel ActionPanel;

    public Animator ActionAnimator;

    public Animator CoverAnimator;

    public JarStatus p_JarStatus = JarStatus.Close;

    TaskName[] _closeTaskArray = new TaskName[3] { TaskName.T5, TaskName.T8, TaskName.T10 };

    TaskInspector _inspector;

    [SyncVar] public EIdentity _clickedButtonIdentity;

    [SyncVar] public bool _isCanGoBackOne = false;
    [SyncVar] public bool _isCanGoBackTwo = false;

    public void Start()
    {
        GoPointOneButton.onClick.AddListener(() =>
        {
            CmdSetActionBool("goPointOne", true);
            CmdSetActionBool("goBackOne", false);
            CmdSetActive(false);
        });

        GoPointTwoButton.onClick.AddListener(() =>
        {
            CmdSetActionBool("goPointTwo", true);
            CmdSetActionBool("goBackTwo", false);
            CmdSetActive(false);
        });

        GoBackButtonTwo.onClick.AddListener(() =>
        {
            _clickedButtonIdentity = GetLocalPlayer();
            CmdSetClickedButtonIdentity(_clickedButtonIdentity);
            CmdGoBackOneCheck();

            if (_isCanGoBackTwo) //p_JarStatus == JarStatus.Close && !GameSteps.Get().IsCheckTaskFinished(TaskName.T7))
            {
                CmdSetActionBool("goPointOne", false);
                CmdSetActionBool("goBackOne", true);
                CmdSetActive(false);
                CmdGoTask(TaskName.T12);
            }
        });

        GoBackButtonOne.onClick.AddListener(() =>
        {
            _clickedButtonIdentity = GetLocalPlayer();
            CmdSetClickedButtonIdentity(_clickedButtonIdentity);
            CmdGoBackOneCheck();

            if (_isCanGoBackOne) //p_JarStatus == JarStatus.Close && !GameSteps.Get().IsCheckTaskFinished(TaskName.T7))
            {
                CmdSetActionBool("goPointOne", false);
                CmdSetActionBool("goBackOne", true);
                CmdSetActive(false);
                CmdGoTask(TaskName.T7);
            }
        });

        OpenButton.onClick.AddListener(() =>
        {
            CmdSetCoverBool("isOpening", true);
            CmdSetCoverBool("isClosing", false);
            CmdSetJarStatus(JarStatus.Open);
            ActionPanel.CmdOpenButtonClicked();
        });


        CloseButton.onClick.AddListener(() =>
        {
            _clickedButtonIdentity = GetLocalPlayer();
            CmdSetClickedButtonIdentity(_clickedButtonIdentity);
            CmdSetCoverBool("isOpening", false);
            CmdSetCoverBool("isClosing", true);
            CmdSetJarStatus(JarStatus.Close);
            ActionPanel.CmdCloseButtonClicked();
            // CloseButton.SetButtonActive(false);
            // OpenButton.SetButtonActive(true);
            CmdGoCloseTask();
        });

        GoBackButtonOne.SetButtonActive(false);
        GoBackButtonTwo.SetButtonActive(false);
        CloseButton.SetButtonActive(false);
        CmdSetActionPanelActive(false);

        _inspector = new TaskInspector();
    }

    #region 步骤检查

    [Command(requiresAuthority = false)]
    void CmdGoCloseTask()
    {
        _inspector.TCloseActionCheck(_closeTaskArray, _clickedButtonIdentity);
    }

    [Command(requiresAuthority = false)]
    void CmdGoBackOneCheck()
    {
        bool b = _inspector.T7Check(p_JarStatus, _clickedButtonIdentity);
        RpcSetisCanGoBackOne(b);
    }

    [ClientRpc]
    void RpcSetisCanGoBackOne(bool b) { _isCanGoBackOne = b; }

    [Command(requiresAuthority = false)]
    void CmdGoBackTwoCheck()
    {
        bool b = _inspector.T7Check(p_JarStatus, _clickedButtonIdentity);
        RpcSetisCanGoBackTwo(b);
    }

    [ClientRpc]
    void RpcSetisCanGoBackTwo(bool b) { _isCanGoBackTwo = b; }

    #endregion

    [Command (requiresAuthority = false)]
    void CmdGoTask(TaskName taskName)
    {
        GameSteps.Get().CheckTaskGoRun(taskName);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetActionBool(string param, bool val)
    {
        RpcSetActionBool(param, val);
    }

    [ClientRpc]
    public void RpcSetActionBool(string param, bool val)
    {
        ActionAnimator?.SetBool(param, val);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetCoverBool(string param, bool val)
    {
        RpcSetCoverBool(param, val);
    }

    [ClientRpc]
    public void RpcSetCoverBool(string param, bool val)
    {
        CoverAnimator?.SetBool(param, val);
    }

    [Command(requiresAuthority = false)]
    void CmdSetActive(bool active)
    {
        RpcSetActive(active);
    }

    [ClientRpc]
    void RpcSetActive(bool active)
    {
        GoBackButtonOne.SetButtonActive(active);
        GoBackButtonTwo.SetButtonActive(active);
        GoPointOneButton.SetButtonActive(active);
        GoPointTwoButton.SetButtonActive(active);
        ActionPanel.SetActive(active);

        if (ArriveText) ArriveText.text = active ? "目的地:" : "";
        if (BGImage) BGImage.enabled = active;
    }

    [Command(requiresAuthority = false)]
    void CmdSetActionPanelActive(bool active)
    {
        RpcSetActionPanelActive(active);
    }

    [ClientRpc]
    void RpcSetActionPanelActive(bool active)
    {
        ActionPanel.SetActive(active);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetJarStatus(JarStatus status)
    {
        RpcSetJarStatus(status);
    }


    [ClientRpc]
    public void RpcSetJarStatus(JarStatus status)
    {
        p_JarStatus = status;
    }

    public void ArriveOnePointEvent()
    {
        GoBackButtonOne.SetButtonActive(true);
        ActionPanel.SetActive(true);
        if (ArriveText) ArriveText.text = "目的地:";
        if (BGImage) BGImage.enabled = true;
    }

    public void ArriveTwoPointEvent()
    {
        GoBackButtonTwo.SetButtonActive(true);
        ActionPanel.SetActive(true);
        if (ArriveText) ArriveText.text = "目的地:";
        if (BGImage) BGImage.enabled = true;
    }

    public void LocalActiveUI(bool active)
    {
        GoBackButtonOne.SetButtonActive(active);
        GoBackButtonTwo.SetButtonActive(active);
        GoPointOneButton.SetButtonActive(active);
        GoPointTwoButton.SetButtonActive(active);
        ActionPanel.SetActive(active);

        if (ArriveText) ArriveText.text = active ? "目的地:" : "";
        if (BGImage) BGImage.enabled = active;
    }

    public EIdentity GetLocalPlayer()
    {
        VRNetworkPlayerController[] players = FindObjectsOfType<VRNetworkPlayerController>();
        foreach (VRNetworkPlayerController player in players)
        {
            if (player.isLocalPlayer)
            {
                return player.identity;
            }
        }
        return EIdentity.None;
    }

    [Command(requiresAuthority = false)]
    public void CmdSetClickedButtonIdentity(EIdentity identity)
    {
        _clickedButtonIdentity = identity;
    }

    public IEnumerator CoverOpenAndClose(float delay, float switchTime)
    {
        yield return new WaitForSeconds(delay);

        CmdSetCoverBool("isOpening", true);
        CmdSetCoverBool("isClosing", false);
        CmdSetJarStatus(JarStatus.Open);
        OpenButton.SetButtonActive(false);
        CloseButton.SetButtonActive(true);

        yield return new WaitForSeconds(switchTime);

        CmdSetCoverBool("isOpening", false);
        CmdSetCoverBool("isClosing", true);
        CmdSetJarStatus(JarStatus.Close);
        OpenButton.SetButtonActive(true);
        CloseButton.SetButtonActive(false);
    }

    public enum JarStatus
    {
        Open,
        Close
    }
}
