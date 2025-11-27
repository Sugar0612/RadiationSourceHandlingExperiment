using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using UnityEngine.Playables;

public class PlayerWearPanel : NetworkBehaviour
{
    public TMP_Text PercentText;

    public TMP_Text HintText;

    public Slider WearSlider;

    /// <summary> 已穿戴提示框 </summary>
    public GameObject ShwoWorePanel;

    VRNetworkPlayerController _vrPlayerController;

    [SyncVar] public WearPanelState workState = WearPanelState.Wait;

    [SyncVar(hook = nameof(OnSliderParentChanged))] float SliderPercent = 0.0f;

    private void Start()
    {
        ResetUI();
        SetActive(false);
    }

    /// <summary> 玩家正在穿戴防护服 UI 显示 </summary>
    public void Wearing(VRNetworkPlayerController vrController, Action callback)
    {
        RpcResetUI();
        //SetWearPanelState(WearPanelState.Working);
        workState = WearPanelState.Working;
        _vrPlayerController = vrController;
        if (_vrPlayerController != null)
        {
            StartCoroutine(WearingClothing(callback));
        }
    }

    [ClientRpc] public void RpcSetWorePanelActive(bool active)
    {
        SetWorePanelActive(active);
    }

    void SetWorePanelActive(bool active)
    {
        ShwoWorePanel.SetActiveForTheUI<TextMeshProUGUI>(active);
        ShwoWorePanel.SetActiveForTheUI<Image>(active);
    }

    IEnumerator WearingClothing(Action successCallback)
    {
        SliderPercent = 0.0f;
        _vrPlayerController.WStatus = VRNetworkPlayerController.WearStatus.Wearing;
        while(_vrPlayerController.WStatus == VRNetworkPlayerController.WearStatus.Wearing && WearSlider.value != 1.0f)
        {
            SliderPercent += 0.01f;

            yield return new WaitForSeconds(0.05f);
        }

        if (SliderPercent >= 1.0f)
        {
            workState = WearPanelState.Wait;
            successCallback();
        }
        else
        {
            RpcResetUI();
        }

        yield break;
    }

    public void SetActive(bool active)
    {
        WearSlider.SetAciveForTheUIControl<Image>(active);
        WearSlider.SetAciveForTheUIControl<Text>(active);
        PercentText.SetAciveForTheUIControl<TextMeshProUGUI>(active);
        HintText.SetAciveForTheUIControl<TextMeshProUGUI>(active);
    }

    [ClientRpc]
    public void RpcSetActive(bool active)
    {
        SetActive(active);
    }

    void ResetUI()
    {
        SetWorePanelActive(false);
        HintText.text = $"穿戴区";
        PercentText.text = "0%";
        WearSlider.value = 0.0f;
    }

    [ClientRpc]
    public void RpcResetUI()
    {
        ResetUI();
    }

    [Command(requiresAuthority = false)]
    public void SetSliderParent(float val)
    {
        SliderPercent = val;
    }

    void OnSliderParentChanged(float oldVal, float newVal)
    {
        WearSlider.value = newVal;

        if (WearSlider.value <= 0.2f) HintText.text = $"防护手套穿戴中...";
        else if (WearSlider.value > 0.2f && WearSlider.value <= 0.4f) HintText.text = $"防护帽穿戴中...";
        else if (WearSlider.value > 0.4f && WearSlider.value < 0.6f) HintText.text = $"剂量片与报警仪穿戴中...";
        else HintText.text = $"防护衣穿戴中...";

        PercentText.text = $"{(WearSlider.value * 100f).ToString("F2")}%";

        if (newVal >= 1.0f)
        {
            PercentText.text = "√";
            HintText.text = $" 穿戴完成！";
        }
    }

    [Command(requiresAuthority = false)]
    public void SetWearPanelState(WearPanelState state)
    {
        workState = state;
    }

    public enum WearPanelState
    {
        Wait,
        Working
    }
}
