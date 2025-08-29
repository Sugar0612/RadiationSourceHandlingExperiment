using Mirror;
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
        RpcClickedPutButton();
    }

    [ClientRpc]
    public void RpcClickedPutButton()
    {
        PutItem_1.OnClickedPutButton();

        foreach (var go in SceneObjectManager.Get().OutSideFencesList)
            go.SetActive<Renderer>(true);

        // 2-1
        if (!GameSteps.Get().IsCheckTaskFinished(TaskName.T2))
        {
            GameSteps.Get().Run();
        }
        // GO on Task...

    }
}
