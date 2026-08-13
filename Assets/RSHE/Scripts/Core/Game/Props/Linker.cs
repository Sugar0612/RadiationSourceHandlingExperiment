using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 两个警戒庄的连接器：警戒带
/// </summary>
public class Linker : NetworkBehaviour
{
    private Transform _targetA;
    private Transform _targetB;

    private float _baseLength = 1.0f;

    public void Init(Transform a, Transform b)
    {
        _targetA = a;
        _targetB = b;
    }

    private void Update()
    {
        Vector3 a = _targetA.position;
        Vector3 b = _targetB.position;

        Vector3 direction = b - a;
        float distance = direction.magnitude;

        // 中点
        transform.position = (a + b) * 0.5f;

        // 让 X 轴指向 B
        transform.rotation = Quaternion.FromToRotation(
            Vector3.right,
            direction
        );

        // 修改 X 轴长度
        Vector3 scale = transform.localScale;
        scale.x = distance / _baseLength;
        transform.localScale = scale;
    }
}
