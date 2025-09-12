using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RecordItem : MonoBehaviour
{
    public TMP_Text ValueText;

    public float Value = 0.0f;

    public void SetValueText(float value)
    { 
        Value = value;
        ValueText.text = value.ToString("F2");
    }
}
