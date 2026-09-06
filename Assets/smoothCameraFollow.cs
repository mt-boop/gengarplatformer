using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    public Transform cameraTarget;

    [Header("Camera Movement Settings")]
    public float cameraMoveSpeed = 0.15f;
    public Vector3 offset = new Vector3(0f, 1f, -10f);

    private Vector3 currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        // Safety check: if target isn't assigned in Inspector, stop
        if (cameraTarget == null) return;

        // 1. Calculate destination
        Vector3 desiredPosition = cameraTarget.position + offset;

        // 2. Move smoothly toward destination
        transform.position = Vector3.SmoothDamp(
            transform.position,
            desiredPosition,
            ref currentVelocity,
            cameraMoveSpeed
        );
    }
}