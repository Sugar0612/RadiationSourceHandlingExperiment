
using DG.Tweening;
using Mirror;
using UnityEngine;
using UnityEngine.UI;

public class SceneWindow : WinBase
{
    // 场景一切换
    Button SceneButton_1;

    // 场景二切换
    Button SceneButton_2;

    // 场景三切换
    Button SceneButton_3;

    // 场景一
    [Scene]
    public string scene_1;

    // 场景二
    [Scene]
    public string scene_2;

    // 场景三
    [Scene]
    public string scene_3;

    public override void Awake()
    {
        base.Awake();
    }

    public override void Start()
    {
        base.Start();

        if (!Config.Get().PicoDevice)
        {
            gameObject.TryFindAndSetStatus("SceneButton_1", true, out SceneButton_1);
            gameObject.TryFindAndSetStatus("SceneButton_2", true, out SceneButton_2);
            gameObject.TryFindAndSetStatus("SceneButton_3", true, out SceneButton_3);

            SceneButton_1.onClick.AddListener(OnClickSceneButton_1);
            SceneButton_2.onClick.AddListener(OnClickSceneButton_2);
            SceneButton_3.onClick.AddListener(OnClickSceneButton_3);
        }
    }

    public void OnClickSceneButton_1()
    {
        NetworkManager.singleton.ServerChangeScene(scene_1);
    }

    public void OnClickSceneButton_2()
    {
        NetworkManager.singleton.ServerChangeScene(scene_2);
    }
    
    public void OnClickSceneButton_3()
    {
        NetworkManager.singleton.ServerChangeScene(scene_3);
    }
}