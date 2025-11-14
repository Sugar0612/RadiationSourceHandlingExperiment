using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OverviewWindow : WinBase
{
    public Button CameraPanelButton;

    public Button ModePanelButton;

    public CameraPanel CameraPanel;

    public SelectModePanel ModePanel;

    bool _cameraPanelActive = false;

    bool _gameModeActive = false;

    public override void Start()
    {
        //base.Start();

        CameraPanelButton.onClick.AddListener(() => OnClickedCamerPanelButton());
        ModePanelButton.onClick.AddListener(() => OnClickedModePanelButton());
    }

    public void OnClickedCamerPanelButton()
    {
        //CameraPanel.SetActive<Image>(!_cameraPanelActive);
        //CameraPanel.SetActive<Button>(!_cameraPanelActive);
        //CameraPanel.SetActive<TextMeshProUGUI>(!_cameraPanelActive);

        //ModePanel.SetActive<Image>(false);
        //ModePanel.SetActive<Button>(false);
        //ModePanel.SetActive<TextMeshProUGUI>(false);

        CameraPanel.SetActive(!_cameraPanelActive);
        ModePanel.SetActive(false);

        _cameraPanelActive = !_cameraPanelActive;
        _gameModeActive = false;
    }

    public void OnClickedModePanelButton()
    {
        //ModePanel.SetActive<Image>(!_gameModeActive);
        //ModePanel.SetActive<Button>(!_gameModeActive);
        //ModePanel.SetActive<TextMeshProUGUI>(!_gameModeActive);

        //CameraPanel.SetActive<Image>(false);
        //CameraPanel.SetActive<Button>(false);
        //CameraPanel.SetActive<TextMeshProUGUI>(false);

        ModePanel.SetActive(!_gameModeActive);
        CameraPanel.SetActive(false);

        _gameModeActive = !_gameModeActive;
        _cameraPanelActive = false;
    }
}
