using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VideoWindow : WinBase
{
    public Button ExitButton;

    public Button VideoButton;

    public void OnClickedExitButton()
    {
        GameHelpler.Get().BackMenu();
    }

    public void OnClckedVideoButton()
    {
        VideoController.Get().CmdCtrlVideoState();
    }
}
