using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverviewWindow : WinBase
{
    public Button CameraPanelButton;

    public Button ModePanelButton;

    public GameObject CameraPanel;

    public GameObject ModePanel;

    public override void Start()
    {
        base.Start();

        CameraPanelButton.onClick.AddListener(() => OnClickedCamerPanelButton());
        ModePanelButton.onClick.AddListener(() => OnClickedModePanelButton());
    }

    public void OnClickedCamerPanelButton()
    {
        bool active = CameraPanel.activeSelf;
        CameraPanel.SetActive(!active);
    }

    public void OnClickedModePanelButton()
    {
        bool active = ModePanel.activeSelf;
        ModePanel.SetActive(!active);
    }
}
