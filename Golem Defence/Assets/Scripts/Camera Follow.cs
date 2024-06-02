using System.Collections; // Import the System.Collections namespace
using System.Collections.Generic; // Import the System.Collections.Generic namespace
using UnityEngine; // Import the UnityEngine namespace

public class CameraFollow : MonoBehaviour
{
    public List<Transform> targets; // List of targets for the camera to follow

    public Vector3 offset; // Offset position of the camera relative to the targets

    public float smoothTime = .5f; // Smoothing time for the camera movement

    private Vector3 velocity; // Current velocity of the camera, used by SmoothDamp

    // LateUpdate is called once per frame, after all Update calls
    private void LateUpdate()
    {
        if (targets.Count == 0)
        {
            return; // If no targets, do nothing
        }

        // Remove destroyed targets from the list
        targets.RemoveAll(t => t == null);

        // Get the center point between all targets
        Vector3 centerPoint = GetCenterPoint();

        // Calculate the new camera position
        Vector3 newPosition = centerPoint + offset;

        // Smoothly move the camera to the new position
        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    // Get the center point between all targets
    Vector3 GetCenterPoint()
    {
        if (targets.Count == 1)
        {
            return targets[0].position; // If there's only one target, return its position
        }

        // Create a bounds that encapsulates all target positions
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != null)
            {
                bounds.Encapsulate(targets[i].position); // Expand the bounds to include the target position
            }
        }

        return bounds.center; // Return the center of the bounds
    }
}
