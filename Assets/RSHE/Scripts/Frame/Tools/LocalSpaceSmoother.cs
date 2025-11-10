using UnityEngine;

public class LocalSpaceSmoother : MonoBehaviour
{
    public Transform target;
    public float positionSmoothTime = 0.3f;
    public float rotationSmoothTime = 0.3f;
    public Vector3 positionOffset;
    public Vector3 rotationOffset;

    private Vector3 positionVelocity = Vector3.zero;

    void LateUpdate()
    {
        if (target == null) return;

        // 位置平滑跟随
        Vector3 targetPosition = target.position + target.TransformDirection(positionOffset);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref positionVelocity, positionSmoothTime);

        // 旋转平滑跟随（使用Lerp简化）
        Quaternion targetRotation = target.rotation * Quaternion.Euler(rotationOffset);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSmoothTime * Time.deltaTime * 10f);
    }
}
