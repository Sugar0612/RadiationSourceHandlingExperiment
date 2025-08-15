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
        EventManager.OnEventTriggered += OverviewUILimite;
        EventManager.OnEventTriggered += UpdateUserUI;
    }

    private void OnDisable()
    {
        EventManager.OnEventTriggered -= OverviewUILimite;
        EventManager.OnEventTriggered -= UpdateUserUI;
    }

    /// <summary>
    /// 该函数是对前景提要场景的部分UI进行约束
    /// 约束条件是当所有人物不在等待区域UI enable = false;
    /// </summary>
    public void OverviewUILimite(int personCnt)
    {
        OverviewWindow overview = UIController.Get().GetWindow<OverviewWindow>(EWindowType.OverviewWindow) as OverviewWindow;

        if (overview)
        {
            if (personCnt == StaticGlobalVar.PersonCount)
            {
                overview.CameraPanel.SetActiveForTheUI<Button>(true);
                overview.ModePanel.SetActiveForTheUI<Button>(true);
            }
            else
            {
                overview.CameraPanel.SetActiveForTheUI<Button>(false);
                overview.ModePanel.SetActiveForTheUI<Button>(false);
            }
        }
    }

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

    /// <summary>
    /// 更新玩家登录状态UI
    /// </summary>
    /// <param name="identity"></param>
    /// <param name="state"></param>
    //public void UpdateUsrState(EIdentity identity, EUserState state)
    //{
    //    UserWindow userWin = UIController.Get().GetWindow<UserWindow>(EWindowType.UserWindow) as UserWindow;
    //    userWin.SetItemState(identity, state);
    //}
}
