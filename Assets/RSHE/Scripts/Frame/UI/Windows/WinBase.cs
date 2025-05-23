using UnityEngine;

public class WinBase : MonoBehaviour
{
    public EWindowType windowType = EWindowType.None;

    public virtual void Start()
    {
        UIController.Get().Register(this);
    }
}

public enum EWindowType
{
    None = 0,
    MainWindow = 1,
    SceneWindow = 2,
    UserWindow = 3,
    VRJoinWindow = 4,
}