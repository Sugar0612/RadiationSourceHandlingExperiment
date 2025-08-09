using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public static class UIExtensions
{
    public static void SetAciveForTheUIControl<T>(this UIBehaviour obj, bool active) where T : UIBehaviour
    {
        T[] ts = obj.gameObject.GetComponentsInChildren<T>();
        foreach (var item in ts)
            item.gameObject.GetComponent<T>().enabled = active;
    }
}
