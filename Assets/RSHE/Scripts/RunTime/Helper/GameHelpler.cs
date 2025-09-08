using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHelpler : MonoBehaviour
{
    static GameHelpler Instance;

    public static GameHelpler Get()
    {
        if (Instance == null)
        {
            Instance = FindObjectOfType<GameHelpler>();
        }

        return Instance;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Log.cinput("yellow", "@@ GameHelper Awake..");
            Instance = FindObjectOfType<GameHelpler>();
            DontDestroyOnLoad(this);
        }
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
        StartCoroutine(IEBackMenu());
    }

    IEnumerator IEBackMenu()
    {
        NetworkGlobalToolkit toolkit = FindObjectOfType<NetworkGlobalToolkit>();

        if (toolkit != null)
            toolkit.CmdServerClickedMenuBack();

        yield return new WaitUntil(() => Timer.IsGoOn == false);

        UIController.Get().ShowWindows(EWindowType.UserWindow);
        CameraManager.Get().SwitchCamera(CameraTag.Manager); // default.
        NetworkManager.singleton.ServerChangeScene("Office");
    }
}
