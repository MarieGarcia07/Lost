using UnityEngine;

[ExecuteAlways]

public class SpringArm : MonoBehaviour
{
    [SerializeField] Transform mAttachTransform;
    [SerializeField] float mArmLength = 3f;
    [SerializeField] float mCameraCollisionOffset = 0.1f;
    [SerializeField] bool mDoCollisionTest = true;
    [SerializeField] LayerMask mCollisionLayerMask;

    void LateUpdate()
    {
        Vector3 endPosition = transform.position - transform.forward * mArmLength;
        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hitInfo, mArmLength, mCollisionLayerMask))
        {
            endPosition = hitInfo.point + transform.forward * mCameraCollisionOffset;
        }

        mAttachTransform.position = endPosition;
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, mAttachTransform.position);
    }
}
