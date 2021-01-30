using UnityEngine;
using UnityEngine.EventSystems;


namespace be.nocomputer.ncUnityUtils
{
    [ExecuteInEditMode]

    public class CameraOrbitController : MonoBehaviour
    {
        public Transform target;
        public Vector3 pivotPoint;

        [Header("Input")]
        public bool invertY = true;
        public bool ignoreUI = false;

        [Header("Mouse sensitivity")]
        public float mouseSensitivity = 10.0f;
        public float mouseWheelSensitivity = 10.0f;

        [Header("Touch sensitivity")]
        public float touchSensitivity = 10.0f;
        public float pinchSensitivity = 1.0f;

        [Header("Physics")]
        public float maxSpeed = 10.0f;
        public float maxZoomSpeed = 10.0f;
        public float dragCoefficient = 0.1f;


        [Header("Limits")]
        [Range(0.0f, Mathf.PI)]
        public float minPolar = 0.01f;
        [Range(0.0f, Mathf.PI)]
        public float maxPolar = Mathf.PI - 0.01f;

        public float minRadius = 1.0f;
        public float maxRadius = 10.0f;

        private SphericalCoord spherical;
        private SphericalCoord speed;
        private bool isDragging = false;
        private bool isZooming = false; // only used for mobile pinch
        private float prevDistance = 0.0f;

        private void Start()
        {
            Vector3 tgt = target ? target.position + pivotPoint : pivotPoint;
            spherical = SphericalCoord.FromVector3(transform.position - tgt);
            transform.LookAt(tgt);
            speed = new SphericalCoord(0.0f, 0.0f, 0.0f);
        }

        private void Update()
        {
            Vector3 tgt = target ? target.position + pivotPoint : pivotPoint;

            if (Application.isPlaying)
            {
                // if we're interacting with UI, do not update anything. TODO: This may not work when using keyboard input!
                if (!ignoreUI && EventSystem.current.currentSelectedGameObject)
                    return;

                if (Application.isMobilePlatform)
                {
                    ProcessTouch();
                }
                else
                {
                    ProcessMouse();
                }

                speed.azimuth -= speed.azimuth * dragCoefficient;
                speed.polar -= speed.polar * dragCoefficient;
                speed.radius -= speed.radius * dragCoefficient;

                spherical.azimuth += Time.deltaTime * speed.azimuth;
                spherical.polar += Time.deltaTime * speed.polar;
                spherical.radius += Time.deltaTime * speed.radius;

                spherical.polar = Mathf.Clamp(spherical.polar, minPolar, maxPolar);
                spherical.radius = Mathf.Clamp(spherical.radius, minRadius, maxRadius);

                transform.position = tgt + spherical.ToVector3();
            }
            else
            {
                spherical = SphericalCoord.FromVector3(tgt - transform.position);
            }

            transform.LookAt(tgt);
        }

        private void ProcessMouse()
        {
            if (Input.GetMouseButton(0))
            {
                if (isDragging)
                {
                    float dx = Input.GetAxis("Mouse X");
                    float dy = Input.GetAxis("Mouse Y");

                    ProcessMove(dx, dy, mouseSensitivity);
                }

                isDragging = true;
            }
            else
            {
                isDragging = false;
            }

            ProcessZoom(-Input.GetAxis("Mouse ScrollWheel"), mouseWheelSensitivity);
        }

        private void ProcessTouch()
        {
            float dx = 0.0f;
            float dy = 0.0f;

            if (Input.touchCount > 0)
            {
                if (isDragging)
                {
                    // average movement of all touches
                    for (int i = 0; i < Input.touchCount; ++i)
                    {
                        dx += Input.touches[i].deltaPosition.x;
                        dy += Input.touches[i].deltaPosition.y;
                    }
                    dx /= Input.touchCount * Screen.width;
                    dy /= Input.touchCount * Screen.height;

                    ProcessMove(dx, dy, touchSensitivity);
                }

                isDragging = true;
            }
            else
            {
                isDragging = false;
            }

            if (Input.touchCount >= 2)
            {
                float distance = (Input.touches[0].position - Input.touches[1].position).magnitude;

                if (isZooming)
                {
                    Debug.Log(distance / prevDistance * pinchSensitivity);
                    spherical.radius *= prevDistance / distance * pinchSensitivity;
                }

                prevDistance = distance;
                isZooming = true;
            }
            else
            {
                isZooming = false;
            }
        }

        private void ProcessMove(float dx, float dy, float accelleration)
        {
            if (invertY)
                dy = -dy;

            speed.azimuth -= accelleration * dx * Time.deltaTime;
            speed.polar += accelleration * dy * Time.deltaTime;

            speed.azimuth = Mathf.Clamp(speed.azimuth, -maxSpeed, maxSpeed);
            speed.polar = Mathf.Clamp(speed.polar, -maxSpeed, maxSpeed);
        }

        private void ProcessZoom(float dr, float zoom)
        {
            speed.radius += zoom * dr;
            speed.radius = Mathf.Clamp(speed.radius, -maxZoomSpeed, maxZoomSpeed);
        }

        private void OnValidate()
        {
            maxRadius = Mathf.Max(0.0f, maxRadius);
            minRadius = Mathf.Clamp(minRadius, 0.0f, maxRadius);
        }
    }
}
