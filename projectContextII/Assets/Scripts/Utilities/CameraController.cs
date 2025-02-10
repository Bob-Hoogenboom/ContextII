using Cinemachine;
using UnityEngine;

/// <summary>
/// This script is specifically designed for the FreeLook Camera!
/// Using a Virtual Camera will cause errors
/// </summary>

[RequireComponent(typeof(CinemachineFreeLook))]
public class CameraController : MonoBehaviour
{
    [Header("Reference")]
    private CinemachineFreeLook _cinemachineFL;

    [Header("FOV_Zoom")]
    [SerializeField] private float targetFieldOfView = 40f;
    [SerializeField] private float fieldOfViewStep = 5f;
    [SerializeField] private float minimalFieldOfView = 8f;
    [SerializeField] private float maximumFieldOfView = 60f;

    private void Start()
    {
        _cinemachineFL = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        HandleCameraZoom();
    }

    private void HandleCameraZoom()
    {
        if(Input.mouseScrollDelta.y > 0)
        {
            targetFieldOfView += fieldOfViewStep;
        }
        if (Input.mouseScrollDelta.y < 0)
        {
            targetFieldOfView -= fieldOfViewStep;
        }

        targetFieldOfView = Mathf.Clamp(targetFieldOfView, minimalFieldOfView, maximumFieldOfView);

        _cinemachineFL.m_Lens.FieldOfView = targetFieldOfView;
    }
}
