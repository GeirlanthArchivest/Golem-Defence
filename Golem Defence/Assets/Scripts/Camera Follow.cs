using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;

    public float smoothTime = .5f;

    private Vector3 velocity;

    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return;
        }

        // Remove destroyed targets
        targets.RemoveAll(t => t == null);

        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
            {
                bounds.Encapsulate(targets[i].position);
            }
        }

        return bounds.center;
    }
}
