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
        // TODO..先这么写着 后面再说
        if (StaticGlobalVar.GameMode == EGameMode.Assessment)
        {
            GlobalPanel.Get().Spawn(@"是否保存本次成绩？",
                () =>
                {
                    Scorer.Get().Save(() =>
                    {
                        UserWindow usr = UIController.Get().GetWindow<UserWindow>(EWindowType.UserWindow) as UserWindow;
                        usr?.ExamRecordPanel?.ReLoadUI();
                        usr?.ExamRecordPanel?.RefreshUI();
                        GameHelpler.Get().BackMenu();
                    });
                    return true;
                },
                () =>
                {
                    GameHelpler.Get().BackMenu();
                    return true;
                }
            ); 
        }
        else
        {
            GameHelpler.Get().BackMenu();
            GlobalPanel.Get().Destroy();
        }
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
