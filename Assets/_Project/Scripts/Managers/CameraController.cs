using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float panSpeed = 10f;
    public float rotateSpeed = 100f;
    public float zoomSpeed = 2f;
    public float edgeSize = 20f;

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    public static Camera cam;
    private float initialFov;
    private float initialOrthoSize;
    public static bool CameraMovementEnabled = true;

    void Awake()
    {
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        cam = Camera.main;
        initialFov = cam.fieldOfView;
        initialOrthoSize = cam.orthographicSize;
    }

    private void OnEnable()
    {
        CameraMovementEnabled = true;
    }

    void Update()
    {
        if (CameraMovementEnabled)
        {
            HandleMoveCamera();
            HandleZoomCamera();
            HandleRotateCamera();

            if (Input.GetKeyDown(KeyCode.P))
            {
                SwitchCameraProjection();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                ResetCamera();
            }
        }
    }

    public void HandleMoveCamera()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        if (vertical != 0f || horizontal != 0f)
        {
            Vector3 forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;
            Vector3 right = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;

            float speed = panSpeed * Time.unscaledDeltaTime;

            Vector3 move = right * horizontal + forward * vertical;

            transform.position += move * speed;
        }

        // if (horizontal == 0f && vertical == 0f)
        // {
        //     Vector3 mousePos = Input.mousePosition;
        //     if (mousePos.x >= Screen.width - edgeSize) { move += right; }
        //     if (mousePos.x <= edgeSize) { move -= right; }
        //     if (mousePos.y >= Screen.height - edgeSize) { move += forward; }
        //     if (mousePos.y <= edgeSize) { move -= forward; }
        // }
    }

    public void HandleZoomCamera()
    {
        float scroll = Input.GetAxisRaw("Mouse ScrollWheel");
        if (scroll != 0f)
        {
            float speed = Time.unscaledDeltaTime * zoomSpeed * scroll;

            float size = cam.orthographicSize - 500f * speed;
            cam.orthographicSize = Mathf.Clamp(size, 1.5f, 7f);

            float fov = cam.fieldOfView - 2500f * speed;
            cam.fieldOfView = Mathf.Clamp(fov, 15f, 70f);
        }
    }

    public void HandleRotateCamera()
    {
        float speed = rotateSpeed * Time.unscaledDeltaTime;
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Rotate(0, -speed, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Rotate(0, speed, 0, Space.World);
        }
    }

    public void SwitchCameraProjection()
    {
        cam.orthographic = !cam.orthographic;
    }

    public void ResetCamera()
    {
        transform.SetPositionAndRotation(initialPosition, initialRotation);
        cam.fieldOfView = initialFov;
        cam.orthographicSize = initialOrthoSize;
    }
}
