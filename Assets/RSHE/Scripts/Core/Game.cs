using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    static Game instance;

    public static Game Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<Game>();
        }

        return instance;
    }

    private void Awake()
    {
        if (instance == null)
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    /// <summary>
    /// 切换游戏场景
    /// </summary>
    public void SwitchGameScene(string scene)
    {
        Log.cinput("yellow", "=========== SwitchGameScene");
        UIController.Get().ShowWindows(EWindowType.GameWinow);
        NetworkManager.singleton.ServerChangeScene(scene);
        CameraManager.Get().SwitchCamera(CameraTag.WitnessFront); // default.
    }

    /// <summary>
    /// 返回菜单界面
    /// </summary>
    public void BackMenu()
    {
        Log.cinput("yellow", "=========== BackMenu");
        UIController.Get().ShowWindows(EWindowType.UserWindow);
        CameraManager.Get().SwitchCamera(CameraTag.Manager); // default.
        NetworkManager.singleton.ServerChangeScene("Office");
    }
}
