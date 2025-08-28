using UnityEngine;

public class TransporterCollider : MonoBehaviour
{
    /// <summary> 运输车存在范围内 </summary>
    public bool IsExist;

    public Transporter p_Transporter;

    public void OnTriggerEnter(Collider other)
    {
        p_Transporter = other.GetComponentInParent<Transporter>();
        if (p_Transporter == null)
            p_Transporter = other.GetComponentInChildren<Transporter>();

        if (p_Transporter != null)
        {
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
            p_Transporter = null;
        }
    }
}