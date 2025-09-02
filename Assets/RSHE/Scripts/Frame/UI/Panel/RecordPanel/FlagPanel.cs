using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagPanel : RecordPanel
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

        if (_valueList.Count >= 4)
        {
            CmdOnClickedPutButton();
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdOnClickedPutButton()
    {
        Log.cinput("yellow", "Flag RpcClickedPutButton");
        RpcClickedPutButton();
        GameSteps.Get().CheckTaskGoRun(TaskName.T4);
    }

    [ClientRpc]
    public void RpcClickedPutButton()
    {
        PutItem_1.OnClickedPutButton();

        foreach (var go in SceneObjectManager.Get().FlagList)
            go.SetActive<Renderer>(true);
    }
}
