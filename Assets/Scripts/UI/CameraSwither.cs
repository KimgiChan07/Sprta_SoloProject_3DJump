using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSwither : MonoBehaviour
{
    public Camera mainCamera;
    public Camera sideCamera;
    
    private bool isSideCamera = false;

    private void Start()
    {
        mainCamera.enabled = true;
        sideCamera.enabled = false;
    }

    public void OnSwitchCamera(InputAction.CallbackContext context)
    {
        isSideCamera= !isSideCamera;
        mainCamera.enabled = !isSideCamera;
        sideCamera.enabled = isSideCamera;
    }
}
