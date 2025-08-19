using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PutItem : MonoBehaviour
{
    public Button PutButton;

    public GameObject HintText;

    private void Start()
    {
        SetActiveForButton(true);
        SetActiveForText(false);
    }

    public void OnClickedPutButton()
    {
        SetActiveForButton(false);
        SetActiveForText(true);
    }

    public void SetActive(bool active)
    {
        SetActiveForButton(active);
        SetActiveForText(active);
    }

    public void SetActiveForButton(bool active)
    {
        PutButton.SetAciveForTheUIControl<Image>(active);
        PutButton.SetAciveForTheUIControl<TextMeshProUGUI>(active);
    }

    public void SetActiveForText(bool active)
    {
        HintText.SetActiveForTheUI<TextMeshProUGUI>(active);
    }
}
