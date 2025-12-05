using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : NetworkBehaviour
{
    #region 场景切换功能

    /// <summary> 游戏场景 </summary>
    [Scene]
    public string GameScene;

    /// <summary> 是否游戏模式已经改变 </summary>
    bool _isChangedMode = false;

    [Command(requiresAuthority = false)]
    public void CmdChangeGameScene(EGameMode mode)
    {
        RpcSetGameModeSync(mode);
        StartCoroutine(ReadyChangedScene());
    }

    [ClientRpc]
    void RpcSetGameModeSync(EGameMode mode)
    {
        StaticGlobalVar.GameMode = mode;
        _isChangedMode = true;
    }

    IEnumerator ReadyChangedScene()
    {
        yield return new WaitUntil(() => _isChangedMode == true);
        GameHelpler.Get().SwitchGameScene(GameScene);
    }

    #endregion

    #region 退出游戏
    [ServerCallback]
    public void ReadyQuitGame()
    {
        GlobalPanel.Get().Spawn("确定退出程序吗？", QuitGame, CancelQuitGame);
    }

    private bool QuitGame()
    {
        RpcQuitGame();
        StartCoroutine(QuitGameCoroutine());
        return true;
    }

    private bool CancelQuitGame()
    {
        return true;
    }

    [ClientRpc]
    private void RpcQuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    IEnumerator QuitGameCoroutine()
    {
        yield return new WaitUntil(() => { return StaticGlobalVar.PersonCount <= 0; });

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }

    #endregion
}
