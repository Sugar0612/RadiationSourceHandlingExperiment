using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Mirror;
using System;
using Unity.VisualScripting;

public class PlayerWearPanel : NetworkBehaviour
{
    public TMP_Text PercentText;

    public TMP_Text HintText;

    public Slider WearSlider;

    /// <summary> 已穿戴提示框 </summary>
    public GameObject ShwoWorePanel;

    VRNetworkPlayerController _vrPlayerController;

    private void Start()
    {
        ResetUI();
        SetActive(false);
    }

    /// <summary> 玩家正在穿戴防护服 UI 显示 </summary>
    public void Wearing(VRNetworkPlayerController vrController, Action callback)
    {
        ResetUI();
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
            WearSlider.value += 0.01f;
            PercentText.text = $"{(WearSlider.value * 100f).ToString("F2")}%";
            yield return new WaitForSeconds(0.1f);
        }

        if (WearSlider.value == 1.0f)
        {
            _vrPlayerController.WStatus = VRNetworkPlayerController.WearStatus.Wore;
            if (!_vrPlayerController.isLocalPlayer)
            {
                _vrPlayerController.hat.SetRendererEnable(true);
                _vrPlayerController.clothes.SetRendererEnable(true);
            }

            PercentText.text = "√";
            HintText.text = $"{_vrPlayerController.identity.ToString()} 穿戴完成！";

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
        HintText.text = $"正在穿戴中...";
        PercentText.text = "0%";
        WearSlider.value = 0.0f;
    }
}