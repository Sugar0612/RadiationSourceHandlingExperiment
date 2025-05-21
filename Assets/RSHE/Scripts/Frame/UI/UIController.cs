using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    static UIController instance;

    MyNetworkDiscovery networkDiscovery;

    [Header("Panel")]

    // VR面板
    public GameObject vrPanel;

    // PC面板
    public GameObject normalPanel;

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
        int isPicoDevice = Config.Get().projectConfig.PicoDevice;

        vrPanel.SetActive(isPicoDevice == 1);
        normalPanel.SetActive(isPicoDevice == 0);

        if (isPicoDevice == 0)
        {
            gameObject.TryFindAndSetStatus("HostButton", true, out hostButton);
            gameObject.TryFindAndSetStatus("PersonCountText", true, out personCountText);
            gameObject.TryFindAndSetStatus("SceneWindow", false, out sceneUI);
        }
        
        if (isPicoDevice == 1)
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
