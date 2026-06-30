using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothSpeed = 5f;
    public Vector3 offset;

    public Vector2 minBounds;
    public Vector2 maxBounds;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position + offset;

        float halfHeight = cam.orthographicSize;
        float halfWidth = halfHeight * cam.aspect;

        float clampX = Mathf.Clamp(
            desiredPosition.x,
            minBounds.x + halfWidth,
            maxBounds.x - halfWidth
        );

        float clampY = Mathf.Clamp(
            desiredPosition.y,
            minBounds.y + halfHeight,
            maxBounds.y - halfHeight
        );

        Vector3 targetPos = new Vector3(
            clampX,
            clampY,
            -10f
        );

        transform.position = Vector3.Lerp(
            transform.position,
            targetPos,
            smoothSpeed * Time.deltaTime
        );
    }
}