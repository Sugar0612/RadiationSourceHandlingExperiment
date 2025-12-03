
using DG.Tweening;
using Mirror;
using System;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SceneWindow : WinBase, IPointerEnterHandler, IPointerExitHandler
{
    #region UI Control
    /// <summary> 教学模式 </summary>
    public Button _teachingButton;

    /// <summary> 实训模式 </summary>
    public Button _practicalTrainingButton;

    /// <summary> 自测模式 </summary>
    public Button _selfTestButton;

    /// <summary> 考核模式 </summary>
    public Button _assessmentButton;

    /// <summary> 前景提要 </summary>
    public Button _prevFactsButton;

    [SerializeField]
    Image _BGImage;

    #endregion

    [Scene]
    public string GameScene;

    [Scene]
    public string PrevFactsScene;

    public override void Start()
    {
        base.Start();

        if (!Config.Get().PicoDevice)
        {
            _teachingButton.onClick.AddListener(() => OnClickedModeButton(EGameMode.Teaching));
            _practicalTrainingButton.onClick.AddListener(() => OnClickedModeButton(EGameMode.PracticalTraining));
            _selfTestButton.onClick.AddListener(() => OnClickedModeButton(EGameMode.SelfTest));
            _assessmentButton.onClick.AddListener(() => OnClickedModeButton(EGameMode.Assessment));
            _prevFactsButton.onClick.AddListener(() => OnClickedPrevFactsButton());
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {

    }

    public void OnPointerExit(PointerEventData eventData) { }

    public void HoverTeacherButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/Teacher");
        _teachingButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void HoverPracticalButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/Practical");
        _practicalTrainingButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void HoverSelfTestButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/SelfTest");
        _selfTestButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void HoverAssessmentButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/Assessment");
        _assessmentButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void HoverPrevFactsButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/PrevFacts");
        _prevFactsButton.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
    }

    public void ExitTeacherButton() { _teachingButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }

    public void ExitPracticalButton() { _practicalTrainingButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }

    public void ExitSelfTestButton() { _selfTestButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }

    public void ExitAssessmentButton() { _assessmentButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }

    public void ExitPrevFactsButton() { _prevFactsButton.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); }

    /// <summary>
    /// 模式按钮点击
    /// </summary>
    public void OnClickedModeButton(EGameMode mode)
    {
        if (NetworkServer.active)
        {
            Game game = FindObjectOfType<Game>();
            game.CmdChangeGameScene(mode);
            StaticGlobalVar.CurrSceneName = "Scene_1";
        }
        //StaticGlobalVar.GameMode = mode;
        //GameHelpler.Get().SwitchGameScene(GameScene);
    }

    void OnClickedPrevFactsButton()
    {
        UIController.Get().ShowWindows(EWindowType.VideoWindow | EWindowType.OverviewWindow);
        NetworkManager.singleton.ServerChangeScene(PrevFactsScene);
        CameraManager.Get().SwitchCamera(CameraTag.Video);
        StaticGlobalVar.CurrSceneName = "Scene_2";
    }
}
