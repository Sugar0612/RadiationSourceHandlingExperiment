using Mirror;
using UnityEngine;

public class WinBase : MonoBehaviour
{
    public EWindowType windowType  = EWindowType.None;

    public virtual void Awake()
    {
        UIController.Get().Register(this);
    }

    public virtual void Start()
    {

    }

    public virtual void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }
}