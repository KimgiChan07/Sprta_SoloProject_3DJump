using System;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;
    public GameObject curInteractGameObject;
    private Iinteractable curInteractable;

    public GameObject itemInfoUIPanel;
    public TextMeshProUGUI promptText;
    private Camera camera;

    private void Start()
    {
        camera = Camera.main;
    }

    private void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            
            Ray ray= camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject= hit.collider.gameObject;
                    curInteractable = hit.collider.GetComponent<Iinteractable>();
                    
                    SetPrompt();
                }
            }
            else
            {
                curInteractGameObject = null;
                curInteractable = null;
                itemInfoUIPanel.gameObject.SetActive(false);
            }
        }
        
    }

    private void SetPrompt()
    {
        itemInfoUIPanel.gameObject.SetActive(true);
        promptText.text = curInteractable.GetDescription();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractable = null;
            curInteractGameObject = null;
            itemInfoUIPanel.SetActive(false);
            promptText.text=String.Empty;
        }
    }
}