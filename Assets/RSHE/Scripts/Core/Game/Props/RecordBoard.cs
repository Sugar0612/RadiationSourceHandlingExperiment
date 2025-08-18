using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class RecordBoard : NetworkBehaviour
{
    public void ShowBeltPanle()
    {
        gameObject.GetComponentInChildren<BeltPanel>().SetActive(true);
        gameObject.GetComponentInChildren<FlagPanel>().SetActive(false);
    }

    public void ShowFlagPanel()
    {
        gameObject.GetComponentInChildren<BeltPanel>().SetActive(false);
        gameObject.GetComponentInChildren<FlagPanel>().SetActive(true);
    }
}
