using Mirror;
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
}
