using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHintPanel : MonoBehaviour
{
    public TMP_Text HintText;

    public Button CloseButton;

    private void Start()
    {
        SetActive(false);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActiveForTheUI<Image>(active);
        gameObject.SetActiveForTheUI<TextMeshProUGUI>(active);
    }

    public void ShowHintPanel(string content, float duration)
    {
        HintText.text = content;
        StartCoroutine(DisplayPanelSpecifiesTime(duration));
    }

    IEnumerator DisplayPanelSpecifiesTime(float duration)
    {
        SetActive(true);

        yield return new WaitForSeconds(duration);

        SetActive(false);
    }
}
