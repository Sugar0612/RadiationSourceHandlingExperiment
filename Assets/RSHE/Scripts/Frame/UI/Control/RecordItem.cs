using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordItem : MonoBehaviour
{
    public TMP_Text HintText;

    public Button RecordButton;

    private void Start()
    {
        HintText.SetAciveForTheUIControl<TextMeshProUGUI>(false);
    }

    public void OnClickedRecordButton()
    {
        RecordButton.SetAciveForTheUIControl<Image>(false);
        RecordButton.SetAciveForTheUIControl<TextMeshProUGUI>(false);

        HintText.SetAciveForTheUIControl<TextMeshProUGUI>(true);
    }

    public void SetActive(bool active)
    {
        RecordButton.SetAciveForTheUIControl<Image>(active);
        RecordButton.SetAciveForTheUIControl<TextMeshProUGUI>(active);

        HintText.SetAciveForTheUIControl<TextMeshProUGUI>(active);
    }
}
