using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PutItem : MonoBehaviour
{
    public Button PutButton;

    public Button RecordButton;

    public GameObject HintText;

    private void Start()
    {
        PutButton.SetButtonActive(false);
        RecordButton.SetButtonActive(true);
        SetActiveForText(false);
    }

    public void OnClickedPutButton()
    {
        PutButton.SetButtonActive(false);
        SetActiveForText(true);
    }

    public void OnClickedRecordButtonFinal()
    {
        RecordButton.SetButtonActive(false);
        PutButton.SetButtonActive(true);
    }

    public void SetActive(bool active)
    {
        PutButton.SetButtonActive(active);
        SetActiveForText(active);
    }

    public void SetActiveForText(bool active)
    {
        HintText.SetActiveForTheUI<TextMeshProUGUI>(active);
    }
}
