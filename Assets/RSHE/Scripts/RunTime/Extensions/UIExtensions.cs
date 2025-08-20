using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class UIExtensions
{
    public static void SetAciveForTheUIControl<T>(this UIBehaviour obj, bool active) where T : UIBehaviour
    {
        T[] ts = obj.gameObject.GetComponentsInChildren<T>();
        foreach (var item in ts)
            item.gameObject.GetComponent<T>().enabled = active;
    }

    public static void SetButtonActive(this Button button, bool active)
    {
        button.SetAciveForTheUIControl<Image>(active);
        button.SetAciveForTheUIControl<TextMeshProUGUI>(active);
    }
}
