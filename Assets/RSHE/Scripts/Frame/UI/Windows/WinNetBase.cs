using Mirror;
using UnityEngine;

public abstract class WinNetBase : NetworkBehaviour, IWin
{
    public abstract EWindowType windowType { get; }

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