using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;

public class PlayerWearPanel : NetworkBehaviour
{
    public TMP_Text PercentText;

    public TMP_Text HintText;

    public Slider WearSlider;

    /// <summary> 已穿戴提示框 </summary>
    public GameObject ShwoWorePanel;

    VRNetworkPlayerController _vrPlayerController;

    public WearPanelState workState = WearPanelState.Wait;

    private void Start()
    {
        ResetUI();
        SetActive(false);
    }

    /// <summary> 玩家正在穿戴防护服 UI 显示 </summary>
    public void Wearing(VRNetworkPlayerController vrController, Action callback)
    {
        ResetUI();
        workState = WearPanelState.Working;
        _vrPlayerController = vrController;
        if (_vrPlayerController != null)
        {
            StartCoroutine(WearingClothing(callback));
        }
    }

    public void SetWorePanelActive(bool active)
    {
        ShwoWorePanel.SetActiveForTheUI<TextMeshProUGUI>(active);
        ShwoWorePanel.SetActiveForTheUI<Image>(active);
    }

    IEnumerator WearingClothing(Action callback)
    {
        _vrPlayerController.WStatus = VRNetworkPlayerController.WearStatus.Wearing;
        while(_vrPlayerController.WStatus == VRNetworkPlayerController.WearStatus.Wearing && WearSlider.value != 1.0f)
        {
            float persent = WearSlider.value;
            if (persent <= 0.2f) HintText.text = $"防护手套穿戴中...";
            else if (persent > 0.2f && persent <= 0.4f) HintText.text = $"防护帽穿戴中...";
            else if (persent > 0.4f && persent < 0.6f) HintText.text = $"剂量片与报警仪穿戴中...";
            else HintText.text = $"防护衣穿戴中...";

            WearSlider.value += 0.01f;
            PercentText.text = $"{(WearSlider.value * 100f).ToString("F2")}%";
            yield return new WaitForSeconds(0.1f);
        }

        if (WearSlider.value >= 1.0f)
        {
            _vrPlayerController.WStatus = VRNetworkPlayerController.WearStatus.Wore;
            if (!_vrPlayerController.isLocalPlayer)
            {
                _vrPlayerController.LeftGlove.SetRendererEnable(true);
                _vrPlayerController.RightGlove.SetRendererEnable(true);
                _vrPlayerController.Clothes.SetRendererEnable(true);
                _vrPlayerController.Spectacles.SetRendererEnable(true);
                _vrPlayerController.Collar.SetRendererEnable(true);
                _vrPlayerController.Hat.SetRendererEnable(true);
            }

            PercentText.text = "√";
            HintText.text = $"{_vrPlayerController.identity.ToString()} 穿戴完成！";
            workState = WearPanelState.Wait;
            callback();
        }
        else
        {
            ResetUI();
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

    void ResetUI()
    {
        SetWorePanelActive(false);
        HintText.text = $"穿戴区";
        PercentText.text = "0%";
        WearSlider.value = 0.0f;
    }

    public enum WearPanelState
    {
        Wait,
        Working
    }
}