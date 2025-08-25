using Mirror;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.LegacyInputHelpers;

/// <summary> TODO... 后面改成 MVVM架构 </summary>
public class TransportPanel : NetworkBehaviour
{
    public Slider PersentSlider;

    public TMP_Text PersentText;

    public TMP_Text HintText;

    /// <summary> 罐子模型 </summary>
    public GameObject JarModel;

    /// <summary> 运输车模型 </summary>
    public GameObject TransportModel;

    float _persentVal = 0.0f;

    private void Start()
    {
        SetActive(false);
    }

    IEnumerator UpdateView()
    {
        while (_persentVal < 1.0f)
        {
            _persentVal += 0.05f;
            PersentSlider.value = _persentVal;
            PersentText.text = (PersentSlider.value * 100).ToString("F2") + "%";
            yield return new WaitForSeconds(0.1f);
        }

        if (PersentSlider.value >= 1.0f)
        {
            JarModel.SetActive<Renderer>(false);

            HintText.text = "铅罐搬运已完成！";
            PersentSlider.SetAciveForTheUIControl<Image>(false);
            PersentText.SetAciveForTheUIControl<TextMeshProUGUI>(false);

            yield return new WaitForSeconds(3f);
            SetActive(false);
            TransportModel.SetActive<Renderer>(false);

            CmdRequestDestroy(gameObject);
        }
    }

    void SetActive(bool active)
    {
        PersentSlider.SetAciveForTheUIControl<Image>(active);
        PersentText.SetAciveForTheUIControl<TextMeshProUGUI>(active);
        HintText.SetAciveForTheUIControl<TextMeshProUGUI>(active);
    }

    public void OnBackAnimaionEvent()
    {
        SetActive(true);
        StartCoroutine(UpdateView());
    }

    public void DestroyNetworkObject(GameObject targetObject)
    {
        if (isServer && targetObject.TryGetComponent<NetworkIdentity>(out var networkIdentity))
        {
            NetworkServer.Destroy(targetObject);
        }
    }

    [Command(requiresAuthority = false)]
    public void CmdRequestDestroy(GameObject targetObject)
    {
        DestroyNetworkObject(targetObject);
    }
}
