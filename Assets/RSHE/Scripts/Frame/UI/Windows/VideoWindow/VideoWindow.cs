using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VideoWindow : WinBase
{
    public Button ExitButton;

    public Button VideoButton;

    bool _isPlay = false;

    public void OnClickedExitButton()
    {
        GameHelpler.Get().BackMenu();
    }

    public void OnClckedVideoButton()
    {
        _isPlay = !_isPlay;
        VideoButton.GetComponentInChildren<TextMeshProUGUI>().text = _isPlay == true ? "ÔÝÍ£" : "²¥·Å";
        VideoController.Get().CmdCtrlVideoState();
    }
}
