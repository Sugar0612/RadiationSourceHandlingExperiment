using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 该类是对不同UI在不同环境场景下 的约束功能。
/// </summary>
public class UILimiteExpansion : MonoBehaviour
{
    private void OnEnable()
    {
        //EventManager.OnEventTriggered += OverviewUILimite;
        EventManager.OnEventTriggered += UpdateUserUI;
        EventManager.OnButtonEnable += SetButtonEnableOnScene;
    }

    private void OnDisable()
    {
        //EventManager.OnEventTriggered -= OverviewUILimite;
        EventManager.OnEventTriggered -= UpdateUserUI;
        EventManager.OnButtonEnable -= SetButtonEnableOnScene;
    }

    /// <summary>
    /// 该函数是对前景提要场景的部分UI进行约束
    /// 约束条件是当所有人物不在等待区域UI enable = false;
    /// </summary>
    //public void OverviewUILimite(int personCnt)
    //{
    //    OverviewWindow overview = UIController.Get().GetWindow<OverviewWindow>(EWindowType.OverviewWindow) as OverviewWindow;

    //    if (overview)
    //    {
    //        if (personCnt == StaticGlobalVar.PersonCount)
    //        {
    //            overview.CameraPanel.SetActiveForTheUI<Button>(true);
    //            overview.ModePanel.SetActiveForTheUI<Button>(true);
    //        }
    //        else
    //        {
    //            overview.CameraPanel.SetActiveForTheUI<Button>(false);
    //            overview.ModePanel.SetActiveForTheUI<Button>(false);
    //        }
    //    }
    //}

    /// <summary>
    /// 更新当前的UserWindows的UI
    /// </summary>
    public void UpdateUserUI(int personCnt)
    {
        UserWindow usrWin = UIController.Get().GetWindow<UserWindow>(EWindowType.UserWindow) as UserWindow;
        if (usrWin)
        {
            usrWin.ChangedWaitPersonCountText(personCnt);   
        }
    }

    public void SetButtonEnableOnScene(bool enable)
    {
        if (StaticGlobalVar.CurrSceneName == "Office")
        {
            SetOfficeSceneButtonEnable(enable);
        }
        else if (StaticGlobalVar.CurrSceneName == "Scene_1")
        {
            SetScene1ButtonEnable(enable);
        }
        else if (StaticGlobalVar.CurrSceneName == "Scene_2")
        {
            SetScene2ButtonEnable(enable);
        }
    }

    void SetOfficeSceneButtonEnable(bool enable)
    {
        UserWindow usrWin = UIController.Get().GetWindow<UserWindow>(EWindowType.UserWindow) as UserWindow;
        SetPanelButtonEnable(usrWin, enable, new List<string>() { "UsrButton" });
    }

    void SetScene1ButtonEnable(bool enable)
    {
        GameWindow gameWin = UIController.Get().GetWindow<GameWindow>(EWindowType.GameWinow) as GameWindow;
        SetPanelButtonEnable(gameWin, enable, new List<string>() { "Prev", "Next" });
    }

    void SetScene2ButtonEnable(bool enable)
    {
        VideoWindow videoWin = UIController.Get().GetWindow<VideoWindow>(EWindowType.VideoWindow) as VideoWindow;
        SetPanelButtonEnable(videoWin, enable, new List<string>() { "Video" });

        OverviewWindow overviewWin = UIController.Get().GetWindow<OverviewWindow>(EWindowType.OverviewWindow) as OverviewWindow;
        SetPanelButtonEnable(overviewWin, enable, new List<string>() { "CameraPanelButton", "GameModeButton", "CameraViewButton", "PlayerViewButton" });
    }

    public void SetPanelButtonEnable(WinBase win, bool enable, List<string> excludeName)
    {
        Button[] buttons = win?.GetComponentsInChildren<Button>();

        foreach (Button button in buttons)
        {
            if (excludeName.Contains(button.name)) continue;
            button.enabled = enable;
            button.interactable = enable;
        }
    }

    //[Command(requiresAuthority = false)]
    //void CmdInitPlayer()
    //{
    //    RpcInitPlayer();
    //}

    //[ClientRpc]
    //void RpcInitPlayer()
    //{
    //    VRNetworkPlayerController[] controllerArray = FindObjectsOfType<VRNetworkPlayerController>();
    //    foreach (VRNetworkPlayerController ctrl in controllerArray)
    //    {
    //        if (!ctrl.isLocalPlayer)
    //        {
    //            ctrl.Hat.SetRendererEnable(true);
    //            ctrl.RightGlove.SetRendererEnable(true);
    //            ctrl.LeftGlove.SetRendererEnable(true);
    //            ctrl.Clothes.SetRendererEnable(true);
    //            ctrl.Spectacles.SetRendererEnable(true);
    //            ctrl.Clothes.SetRendererEnable(true);
    //        }
    //    }
    //}
}
