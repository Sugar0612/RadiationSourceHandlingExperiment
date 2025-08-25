using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpPanel : NetworkBehaviour
{
    /// <summary> 进度条 </summary>
    public Slider Progress;

    /// <summary> 提示TEXT <summary>
    public TMP_Text HintText;

    [SyncVar(hook = nameof(UpdateSlider))]
    float _value = 0.0f;

    public bool IsPickingUp = false;

    private void Start()
    {
        ResetUI();
    }

    public void PickingUp(Action Success, Action Cancel)
    {
        RpcUpdateViewUI();
        // 
        StartCoroutine(StartProgessIncreasing(Success, Cancel));
    }

    [ClientRpc]
    public void RpcUpdateViewUI()
    {
        HintText.text = "废料处理中...";
        Progress.SetAciveForTheUIControl<Image>(true);
    }

    void UpdateSlider(float old, float New)
    {
        Progress.value = New;
    }

    IEnumerator StartProgessIncreasing(Action Success, Action Cancel)
    {
        while (_value <= 1.0f && IsPickingUp)
        {
            _value += 0.05f;
            yield return new WaitForSeconds(0.1f);
        }

        if (_value >= 1.0f)
        {
            Success();
        }
        else
        {
            Cancel();
        }
        _value = 0.0f;

        yield return null;
    }

    public void ResetUI()
    {
        _value = 0.0f;
        Progress.value = 0.0f;
        HintText.text = "待处理废料";

        Progress.SetAciveForTheUIControl<Image>(false);
    }

    [ClientRpc]
    public void RpcSetActive(bool active)
    {
        gameObject.SetActive<Collider>(false);
        Progress.SetAciveForTheUIControl<Image>(active);
        HintText.SetAciveForTheUIControl<TextMeshProUGUI>(active);
    }
}
