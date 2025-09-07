using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkGlobalToolkit : NetworkBehaviour
{
    #region Public command function
    [Command (requiresAuthority = false)]
    public void CmdServerClickedMenuBack()
    {
        RpcServerClickedMenuBack();
    }
    #endregion


    #region Private rpc function
    [ClientRpc]
    void RpcServerClickedMenuBack()
    {
        Log.cinput("red", "@RpcServerClickedMenuBack");
        Timer.IsGoOn = false;
        StaticGlobalVar.CurrSceneName = "Office";
    }
    #endregion
}
