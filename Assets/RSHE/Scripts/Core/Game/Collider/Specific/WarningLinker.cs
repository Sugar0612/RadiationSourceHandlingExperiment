using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WarningLinker : NetworkBehaviour
{
    public class linkerPkg
    {
        public WarningLinker linker;
        public Linker linkerObj;
    }
    private List<linkerPkg> linkers = new List<linkerPkg>();

    public Transform linkerPointer;

    public bool canlink = true;

    [SerializeField] private GameObject _linkPrefab;

    /// <summary>
    /// 检测
    /// </summary>
    /// <param name="other"></param>
    public void OnTriggerStay(Collider other)
    {
        var linker = other.GetComponent<WarningLinker>();
        if (linker != null && linker.canlink)
        {
            linker.canlink = false;

            var linkerObj = GameObject.Instantiate(_linkPrefab).GetComponent<Linker>();
            linkerObj.Init(linkerPointer, linker.linkerPointer);

            linkerPkg linkPkg = new linkerPkg();
            linkPkg.linker = linker;
            linkPkg.linkerObj = linkerObj;

            linkers.Add(linkPkg);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        var linker = other.GetComponent<WarningLinker>();
        var pkg = linkers.Find(_ => _.linker = linker);
        if (linker != null && pkg != null)
        {
            linker.canlink = true;
            pkg.linkerObj.gameObject.SetActive(false);
            linkers.Remove(pkg);
        }
    }

    private void Link(Transform link_1, Transform link_2)
    {

    }
}
