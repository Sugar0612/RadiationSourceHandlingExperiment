using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine.UI;

public class GameWindow : WinBase
{
    public Button exitButton;

    public Button prevButton;

    public Button nextButton;

    public TMP_Text infoTx;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        InfoTxCalibrate();
    }

    /// <summary>
    /// 返回菜单界面
    /// </summary>
    public void OnClickedExitButton()
    {
        GameHelpler.Get().BackMenu();
        //CmdInitPlayer();
    }

    /// <summary>
    /// 前一个观察视角
    /// </summary>
    public void OnClickedPrevButton()
    {
        Log.cinput("red", "OnClickedPrevButton");
        ObservationGroup.Get().Prev();
        InfoTxCalibrate();
    }

    /// <summary>
    /// 后一个观察视角
    /// </summary>
    public void OnClickedNextButton()
    {
        Log.cinput("red", "OnClickedNextButton");
        ObservationGroup.Get().Next();
        InfoTxCalibrate();
    }

    public void InfoTxCalibrate()
    {
        infoTx.text = 
            $"{ObservationGroup.Get().GetCurrIdx()}/{ObservationGroup.Get().GetObserverCount()}";
    }
}