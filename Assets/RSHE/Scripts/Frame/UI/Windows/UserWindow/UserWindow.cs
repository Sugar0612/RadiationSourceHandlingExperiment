
using Mirror;
using TMPro;
using UnityEngine;

public class UserWindow : WinBase
{
    // 人数文本
    private TMP_Text personCountText;

    public override void Start()
    {
        base.Start();
        
        if (StaticGlobalVar.isPicoDevice == 0)
        {
            gameObject.TryFindAndSetStatus("PersonCountText", true, out personCountText);
        }
    }

    public void ChangedpersonCountText(int personCount)
    {
        MyVRStaticVariables.personCount = personCount;
        personCountText.text = personCount.ToString();
    }

}