using RootMotion;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 该类用于前景提要模式 控制摄像机的类
/// </summary>
public class CameraPanel : MonoBehaviour
{
    public Button VideoButton;

    public Button PlayerButton;

    private void Start()
    {
        VideoButton.onClick.AddListener(() => OnClickedVideoButton());
        PlayerButton.onClick.AddListener(() => OnClickedPlayerButton());

        SetActive(false);
    }

    public void OnClickedVideoButton()
    {
        CameraManager.Get().SwitchCamera(CameraTag.Video);
    }

    public void OnClickedPlayerButton()
    {
        CameraManager.Get().SwitchCamera(CameraTag.OverviewPlayer);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<Image>(active);
        gameObject.SetActive<Button>(active);
        gameObject.SetActive<TextMeshProUGUI>(active);
    }
}
