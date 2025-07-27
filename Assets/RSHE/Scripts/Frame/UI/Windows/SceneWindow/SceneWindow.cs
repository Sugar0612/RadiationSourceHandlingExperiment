
using DG.Tweening;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class SceneWindow : WinBase
{
    #region UI Control
    /// <summary> 教学模式 </summary>
    Button _teachingButton;

    /// <summary> 实训模式 </summary>
    Button _practicalTrainingButton;

    /// <summary> 自测模式 </summary>
    Button _selfTestButton;

    /// <summary> 实训模式 </summary>
    Button _assessmentButton;
    #endregion

    [Scene]
    public string GameScene;

    public override void Start()
    {
        base.Start(); 

        if (!Config.Get().PicoDevice)
        {
            gameObject.TryFindAndSetStatus("Teaching", true, out _teachingButton);
            gameObject.TryFindAndSetStatus("PracticalTraining", true, out _practicalTrainingButton);
            gameObject.TryFindAndSetStatus("SelfTest", true, out _selfTestButton);
            gameObject.TryFindAndSetStatus("Assessment", true, out _assessmentButton);

            _teachingButton.onClick.AddListener(() => OnClickedModeButton(EGameMode.Teaching));
            _practicalTrainingButton.onClick.AddListener(() => OnClickedModeButton(EGameMode.PracticalTraining));
            _selfTestButton.onClick.AddListener(() => OnClickedModeButton(EGameMode.SelfTest));
            _assessmentButton.onClick.AddListener(() => OnClickedModeButton(EGameMode.Assessment));
        }
    }

    /// <summary>
    /// 模式按钮点击
    /// </summary>
    void OnClickedModeButton(EGameMode mode)
    {
        if (NetworkServer.active)
        {
            Game game = FindObjectOfType<Game>();
            game.CmdChangeGameScene(mode);
        }
        //StaticGlobalVar.GameMode = mode;
        // GameHelpler.Get().SwitchGameScene(GameScene);
    }
}