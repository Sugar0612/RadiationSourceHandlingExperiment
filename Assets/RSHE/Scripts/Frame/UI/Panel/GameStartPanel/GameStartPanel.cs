using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStartPanel : NetworkBehaviour
{
    public Button StartButton;

    public Button ExitButton;

    private void Awake()
    {
        if (Config.Get().PicoDevice)
            SetActive(false);
        else
            SetActive(true);
    }

    void Start()
    {
        StartButton.onClick.AddListener(OnStartButtonClicked);
        ExitButton.onClick.AddListener(OnExitButtonClicked);
    }

    public void OnStartButtonClicked()
    {
        GameSteps.Get().RunStart();
        SetActive(false);
    }

    public void OnExitButtonClicked()
    {
        GameHelpler.Get().BackMenu();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<Image>(active);
        gameObject.SetActive<TextMeshProUGUI>(active);
    }
}
