using UnityEngine;

namespace EmeraldPowder.CameraScaler
{
    [AddComponentMenu("Rendering/Camera Scaler")]
    [HelpURL("https://emeraldpowder.github.io/unity-assets/camera-scaler/")]
    public class CameraScaler : MonoBehaviour
    {
        [Tooltip("Set this to the resolution you have set in Game View, or resolution you usually test you game with")]
        public Vector2 ReferenceResolution = new Vector2(720, 1280);

        public WorkingMode Mode = WorkingMode.ConstantWidth;
        public float MatchWidthOrHeight = 0.5f;

        private Camera componentCamera;
        private float targetAspect;
        private float cameraZoom = 1;

        private float initialSize;

        private float initialFov;
        private float horizontalFov;

        private float previousUpdateAspect;
        private WorkingMode previousUpdateMode;
        private float previousUpdateMatch;

        public float HorizontalSize => initialSize * targetAspect;
        public float HorizontalFov => horizontalFov;

        public float CameraZoom
        {
            get => cameraZoom;
            set
            {
                cameraZoom = value;
                UpdateCamera();
            }
        }

        public enum WorkingMode
        {
            ConstantHeight,
            ConstantWidth,
            MatchWidthOrHeight,
            Expand,
            Shrink
        }

        private void Awake()
        {
            componentCamera = GetComponent<Camera>();
            initialSize = componentCamera.orthographicSize;

            targetAspect = ReferenceResolution.x / ReferenceResolution.y;

            initialFov = componentCamera.fieldOfView;
            horizontalFov = CalcVerticalFov(initialFov, 1 / targetAspect);
        }

        private void Update()
        {
            if (!Mathf.Approximately(previousUpdateAspect, componentCamera.aspect) ||
                previousUpdateMode != Mode ||
                !Mathf.Approximately(previousUpdateMatch, MatchWidthOrHeight))
            {
                UpdateCamera();

                previousUpdateAspect = componentCamera.aspect;
                previousUpdateMode = Mode;
                previousUpdateMatch = MatchWidthOrHeight;
            }
        }

        private void UpdateCamera()
        {
            if (componentCamera.orthographic)
            {
                UpdateOrtho();
            }
            else
            {
                UpdatePerspective();
            }
        }

        private void UpdateOrtho()
        {
            switch (Mode)
            {
                case WorkingMode.ConstantHeight:
                    componentCamera.orthographicSize = initialSize / cameraZoom;
                    break;

                case WorkingMode.ConstantWidth:
                    componentCamera.orthographicSize = initialSize * (targetAspect / componentCamera.aspect) / cameraZoom;
                    break;

                case WorkingMode.MatchWidthOrHeight:
                    float vSize = initialSize;
                    float hSize = initialSize * (targetAspect / componentCamera.aspect);
                    float vLog = Mathf.Log(vSize, 2);
                    float hLog = Mathf.Log(hSize, 2);
                    float logWeightedAverage = Mathf.Lerp(hLog, vLog, MatchWidthOrHeight);
                    componentCamera.orthographicSize = Mathf.Pow(2, logWeightedAverage) / cameraZoom;
                    break;

                case WorkingMode.Expand:
                    if (targetAspect > componentCamera.aspect)
                    {
                        componentCamera.orthographicSize = initialSize * (targetAspect / componentCamera.aspect) / cameraZoom;
                    }
                    else
                    {
                        componentCamera.orthographicSize = initialSize / cameraZoom;
                    }

                    break;

                case WorkingMode.Shrink:
                    if (targetAspect < componentCamera.aspect)
                    {
                        componentCamera.orthographicSize = initialSize * (targetAspect / componentCamera.aspect) / cameraZoom;
                    }
                    else
                    {
                        componentCamera.orthographicSize = initialSize / cameraZoom;
                    }

                    break;
                default:
                    Debug.LogError("Incorrect CameraScaler.Mode: " + Mode);
                    break;
            }
        }

        private void UpdatePerspective()
        {
            switch (Mode)
            {
                case WorkingMode.ConstantHeight:
                    componentCamera.fieldOfView = initialFov / cameraZoom;
                    break;

                case WorkingMode.ConstantWidth:
                    componentCamera.fieldOfView = CalcVerticalFov(horizontalFov, componentCamera.aspect) / cameraZoom;
                    break;

                case WorkingMode.MatchWidthOrHeight:
                    float vFov = initialFov;
                    float hFov = CalcVerticalFov(horizontalFov, componentCamera.aspect);
                    float vLog = Mathf.Log(vFov, 2);
                    float hLog = Mathf.Log(hFov, 2);
                    float logWeightedAverage = Mathf.Lerp(hLog, vLog, MatchWidthOrHeight);
                    componentCamera.fieldOfView = Mathf.Pow(2, logWeightedAverage) / cameraZoom;
                    break;

                case WorkingMode.Expand:
                    if (targetAspect > componentCamera.aspect)
                    {
                        componentCamera.fieldOfView = CalcVerticalFov(horizontalFov, componentCamera.aspect) / cameraZoom;
                    }
                    else
                    {
                        componentCamera.fieldOfView = initialFov / cameraZoom;
                    }

                    break;

                case WorkingMode.Shrink:
                    if (targetAspect < componentCamera.aspect)
                    {
                        componentCamera.fieldOfView = CalcVerticalFov(horizontalFov, componentCamera.aspect) / cameraZoom;
                    }
                    else
                    {
                        componentCamera.fieldOfView = initialFov / cameraZoom;
                    }

                    break;
                default:
                    Debug.LogError("Incorrect CameraScaler.Mode: " + Mode);
                    break;
            }
        }

        private static float CalcVerticalFov(float hFovInDeg, float aspectRatio)
        {
            float hFovInRads = hFovInDeg * Mathf.Deg2Rad;

            float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

            return vFovInRads * Mathf.Rad2Deg;
        }
    }
}