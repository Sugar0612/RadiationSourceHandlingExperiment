using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordItem : MonoBehaviour
{
    public Button RecordButton;

    public TMP_Text HintText;

    public TMP_Text ValueText;

    public Toggle SelectedToggle;

    public float Value = 0.0f;

    private void Start()
    {
        HintText.SetAciveForTheUIControl<TextMeshProUGUI>(false);
        RecordButton.onClick.AddListener(OnClickedRecordButton);
    }

    public void OnClickedRecordButton()
    {
        RecordButton.SetAciveForTheUIControl<Image>(false);
        RecordButton.SetAciveForTheUIControl<TextMeshProUGUI>(false);

        HintText.SetAciveForTheUIControl<TextMeshProUGUI>(true);
    }

    public void SetActive(bool active)
    {
        SetActiveForButton(active);
    }

    public void SetActiveForButton(bool active)
    {
        RecordButton.SetAciveForTheUIControl<Image>(active);
        RecordButton.SetAciveForTheUIControl<TextMeshProUGUI>(active);
    }

    public void SetValueText(float value)
    {
        Value = value;
        ValueText.text = value.ToString("F2");
    }
}
