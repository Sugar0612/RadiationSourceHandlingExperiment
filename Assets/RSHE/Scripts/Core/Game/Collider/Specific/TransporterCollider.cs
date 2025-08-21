using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransporterCollider : MonoBehaviour
{
    /// <summary> 运输车存在范围内 </summary>
    public bool IsExist;

    public void OnTriggerEnter(Collider other)
    {
        Log.cinput("yellow", "@@ TransporterCollider OnTriggerEnter");
        Transporter transporter = other.GetComponentInParent<Transporter>();
        if (transporter == null)
            transporter = other.GetComponentInChildren<Transporter>();

        if (transporter != null)
        {
            Log.cinput("yellow", "@@ transporter is not null");
            IsExist = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        Transporter transporter = other.GetComponentInParent<Transporter>();
        if (transporter == null)
            transporter = other.GetComponentInChildren<Transporter>();

        if (transporter != null)
        {
            IsExist = false;
        }
    }
}
