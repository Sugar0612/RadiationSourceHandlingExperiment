using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InBeltPanel : RecordPanel
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

        if (_valueList.Count >= 10)
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

        foreach (var fence in SceneObjectManager.Get().InSideFencesList)
            fence.SetActive<Renderer>(true);

        // GO on Task...
    }
}
