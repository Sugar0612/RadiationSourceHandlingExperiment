
using DG.Tweening;
using Mirror;
using System.Collections;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.UI;

public class SceneWindow : WinBase, IPointerEnterHandler
{
    #region UI Control
    /// <summary> 教学模式 </summary>
    public Button _teachingButton;

    /// <summary> 实训模式 </summary>
    public Button _practicalTrainingButton;

    /// <summary> 自测模式 </summary>
    public Button _selfTestButton;

    /// <summary> 实训模式 </summary>
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
        // Loggg();
    }

    public void HoverTeacherButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/Teacher");
    }

    public void HoverPracticalButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/Practical");
    }

    public void HoverSelfTestButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/SelfTest");
    }

    public void HoverAssessmentButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/Assessment");
    }

    public void HoverPrevFactsButton()
    {
        Utility.LoadImageFromResource(_BGImage, "Textures/UI/MainBG/PrevFacts");
    }

    /// <summary>
    /// 模式按钮点击
    /// </summary>
    public void OnClickedModeButton(EGameMode mode)
    {
        if (NetworkServer.active)
        {
            Game game = FindObjectOfType<Game>();
            game.CmdChangeGameScene(mode);    
        }
        //StaticGlobalVar.GameMode = mode;
        //GameHelpler.Get().SwitchGameScene(GameScene);
    }

    void OnClickedPrevFactsButton()
    {
        UIController.Get().ShowWindows(EWindowType.VideoWindow | EWindowType.OverviewWindow);
        NetworkManager.singleton.ServerChangeScene(PrevFactsScene);
        CameraManager.Get().SwitchCamera(CameraTag.Video);
    }
}