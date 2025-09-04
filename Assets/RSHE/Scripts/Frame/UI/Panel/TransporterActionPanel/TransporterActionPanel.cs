using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransporterActionPanel : MonoBehaviour
{
    public Button OpenButton;

    public Button CloseButton;

    public void OpenButtonClicked()
    {
        OpenButton.SetButtonActive(false);
        CloseButton.SetButtonActive(true);
    }


    public void CloseButtonClicked()
    {
        OpenButton.SetButtonActive(true);
        CloseButton.SetButtonActive(false);
    }

    void CloseAll()
    {
        OpenButton.SetButtonActive(false);
        CloseButton.SetButtonActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive<TextMeshProUGUI>(active);
        if (active)
        {
            CloseButtonClicked();
        }
        else
        {
            CloseAll();
        }
    }
}
