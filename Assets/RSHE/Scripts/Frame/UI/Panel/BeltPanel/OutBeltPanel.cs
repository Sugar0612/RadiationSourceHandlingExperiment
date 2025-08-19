using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OutBeltPanel : BeltPanel
{
    public override void Start()
    {
        base.Start();

        PutItem_1.PutButton.onClick.AddListener(() => CmdOnClickedPutButton());
    }

    [Command(requiresAuthority = false)]
    public void CmdOnClickedPutButton()
    {
        RpcClickedPutButton();
    }

    [ClientRpc]
    public void RpcClickedPutButton()
    {
        foreach (RecordItem item in _recordList)
        {
            if (item.SelectedToggle.isOn)
            {
                _valueList.Add(item.Value);
            }
        }
        PutItem_1.OnClickedPutButton();

        foreach (var fence in SceneObjectManager.Get().OutSideFencesList)
            fence.SetActive<Renderer>(true);

        // GO on Task...
    }
}
