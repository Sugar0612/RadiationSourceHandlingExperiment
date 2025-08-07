using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UserTaskInfoPanel : MonoBehaviour
{
    public TMP_Text UserIndentityText;

    public TMP_Text InfoText;

    public Image Image;

    public void SetPanelContent(string name, string info)
    {
        UserIndentityText.text = name;
        InfoText.text = info;
    }

    public void SetActive(bool active)
    {
        UserIndentityText.GetComponent<TextMeshProUGUI>().enabled = active;
        InfoText.GetComponent<TextMeshProUGUI>().enabled = active;
        Image.GetComponent<Image>().enabled = active;
    }
}
