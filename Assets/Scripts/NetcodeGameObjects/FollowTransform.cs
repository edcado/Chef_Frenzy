using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTransform : MonoBehaviour
{
    private Transform targetTransform;

    public void SetTransformTarget(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    public void LateUpdate()
    {
        if (targetTransform == null)
        {
            return;
        }
        transform.position = targetTransform.position;
        transform.rotation = targetTransform.rotation;
    }
}
