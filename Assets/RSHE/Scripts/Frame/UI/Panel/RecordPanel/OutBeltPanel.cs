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
    }

    public void OnClickedPutButton()
    {
        foreach (RecordItem item in _recordList)
        {
            if (item.SelectedToggle.isOn)
            {
                _valueList.Add(item.Value);
            }
        }

        if (_valueList.Count == 4)
        {
            CmdOnClickedPutButton();
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdOnClickedPutButton()
    {
        foreach (TaskName task in Enum.GetValues(typeof(TaskName)))
        {
            if (task == TaskName.T2)
                break;

            if (!GameSteps.Get().IsCheckTaskFinished(task))
                return;
        }

        RpcClickedPutButton();
        GameSteps.Get().CheckTaskGoRun(TaskName.T2);
    }

    [ClientRpc]
    public void RpcClickedPutButton()
    {
        PutItem_1.OnClickedPutButton();

        foreach (var go in SceneObjectManager.Get().OutSideFencesList)
            go.SetActive<Renderer>(true);
    }
}
