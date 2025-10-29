using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutBeltPanel : RecordPanel
{
    public override void Start()
    {
        base.Start();

        PutItem_1.PutButton.onClick.AddListener(() => OnClickedPutButton());
        PutItem_1.RecordButton.onClick.AddListener(() => OnClickedRecordButton());
    }

    /// <summary> 放置按钮 </summary>
    public void OnClickedPutButton()
    {
        EIdentity identity = GetLocalPlayer();
        CmdSetClickedButtonIdentity(identity);
        CmdOnClickedPutButton();
    }

    /// <summary> 记录按钮 </summary>
    public void OnClickedRecordButton()
    {
        CmdOnClickedRecordButton();
    }

    #region Command Function

    [Command(requiresAuthority = false)]
    public void CmdOnClickedPutButton()
    {
        if (_inspector.CheckRecordTask(0.1f, 0.3f, _clickedButtonIdentity, TaskName.T2, ref _valueList))
        {
            RpcClickedPutButton();
            GameSteps.Get().CheckTaskGoRun(TaskName.T2);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdOnClickedRecordButton()
    {
        float val = ScanArea();
        _valueList.Add(val);
        RpcOnClickedRecordButton(val);
    }

    #endregion

    #region Client Rpc Function

    [ClientRpc]
    public void RpcClickedPutButton()
    {
        PutItem_1.OnClickedPutButton();

        foreach (var go in SceneObjectManager.Get().OutSideFencesList)
            go.SetActive<Renderer>(true);
    }

    [ClientRpc] 
    public void RpcOnClickedRecordButton(float val)
    {
        if (_itemIndex < _recordList.Count)
        {
            _recordList[_itemIndex].SetValueText(val);
            _itemIndex++;

            if (_itemIndex >= 4)
                PutItem_1.OnClickedRecordButtonFinal();
        }
    }

    #endregion
}
