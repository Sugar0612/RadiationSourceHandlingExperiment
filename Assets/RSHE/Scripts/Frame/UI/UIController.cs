using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static UIController instance;

    MyNetworkDiscovery networkDiscovery;

    [Header("Control")]

    // 人数文本
    private TMP_Text personCountText;

    // 主机按钮
    private Button hostButton;

    // Scene Window UI
    SceneUI sceneUI;

    public static UIController Get()
    {
        if (instance == null)
        {
            instance = FindObjectOfType<UIController>();
            if (instance == null)
            {
                GameObject obj = new GameObject("UIController");
                instance = obj.AddComponent<UIController>();
            }
        }
        return instance;
    }

    void Awake()
    {
        networkDiscovery = FindObjectOfType<MyNetworkDiscovery>();

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        if (SwitchCameraController.Get().isTeacher)
        {
            gameObject.TryFindAndSetStatus("HostButton", true, out hostButton);
            gameObject.TryFindAndSetStatus("PersonCountText", true, out personCountText);
            gameObject.TryFindAndSetStatus("SceneWindow", false, out sceneUI);
        }
        
        if (!SwitchCameraController.Get().isTeacher)
            OnClickJoinButton();
    }

    public void OnClickHostButton()
    {
        NetworkManager.singleton.StartHost();
        networkDiscovery.AdvertiseServer();

        hostButton.gameObject.SetActive(false);
        sceneUI.gameObject.SetActive(true);
    }

    public void OnClickJoinButton()
    {
        Log.cinput("green", "Join Button Clicked");
        StartCoroutine(networkDiscovery.IEStartDiscovery()); //开始查找主机
    }

    public void ChangedpersonCountText(int personCount)
    {
        MyVRStaticVariables.personCount = personCount;
        personCountText.text = personCount.ToString();
    }

    public void HidePanel()
    {
        gameObject.SetActive(false);
    }
}
