using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelpler : MonoBehaviour
{
    static GameHelpler m_Instance;

    public static GameHelpler Get()
    {
        if (m_Instance == null)
        {
            m_Instance = FindObjectOfType<GameHelpler>();
        }

        return m_Instance;
    }

    /// <summary>
    /// 切换游戏场景
    /// </summary>
    public void SwitchGameScene(string scene)
    {
        UIController.Get().ShowWindows(EWindowType.GameWinow);
        NetworkManager.singleton.ServerChangeScene(scene);
        CameraManager.Get().SwitchCamera(CameraTag.WitnessFront); // default.
    }

    /// <summary>
    /// 返回菜单界面
    /// </summary>
    public void BackMenu()
    {
        UIController.Get().ShowWindows(EWindowType.UserWindow);
        CameraManager.Get().SwitchCamera(CameraTag.Manager); // default.
        NetworkManager.singleton.ServerChangeScene("Office");
    }
}
