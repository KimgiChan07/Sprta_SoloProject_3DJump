using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
   [Header("Movement")]
   public float moveSpeed;
   public float jumpForce;
   public LayerMask groundLayer;
   private Vector2 curMovementInput;

   [Header("Look")]
   public Transform cameraContainer;
   public float minXLook;
   public float maxXLook;
   public float lookSensitivity;
   private Vector2 mouseDelta;
   private float camCurXRot;
   private Rigidbody rigid;


   private void Awake()
   {
      rigid = GetComponent<Rigidbody>();
   }

   private void FixedUpdate()
   {
      Move();
   }

   private void LateUpdate()
   {
      CameraLook();
   }
   
   //-----------------------------------------------------------------------------

   public void OnMove(InputAction.CallbackContext context)
   {
      if (context.phase == InputActionPhase.Performed)
      {
         curMovementInput=context.ReadValue<Vector2>();
      }
      else if (context.phase == InputActionPhase.Canceled)
      {
         curMovementInput = Vector2.zero;
      }
   }

   void Move()
   {
      Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
      dir*= moveSpeed;
      dir.y = rigid.velocity.y;
      
      rigid.velocity = dir;
   }
   
   //-----------------------------------------------------------------------------

   public void OnLook(InputAction.CallbackContext context)
   {
      mouseDelta=context.ReadValue<Vector2>();
   }

   void CameraLook()
   {
      camCurXRot += mouseDelta.y * lookSensitivity;
      camCurXRot =  Mathf.Clamp(camCurXRot, minXLook, maxXLook);
      cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
      
      transform.localEulerAngles+=new Vector3(0, mouseDelta.x*lookSensitivity, 0);
   }

   //-----------------------------------------------------------------------------
   
   public void OnJump(InputAction.CallbackContext context)
   {
      if (context.phase == InputActionPhase.Started && IsGrounded())
      {
         Debug.Log("Jump");
         rigid.AddForce(Vector2.up * jumpForce, ForceMode.Impulse);
      }
   }

   bool IsGrounded()
   {
      Ray[] rays = new Ray[4]
      {
         new Ray(transform.position + (transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
         new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up * 0.01f), Vector3.down),
         new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
         new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
      };

      
      
      for (int i = 0; i < rays.Length; i++)
      {
         Debug.DrawRay(rays[i].origin, rays[i].direction * 0.1f, Color.red, 1f);
         if (Physics.Raycast(rays[i],1f,groundLayer))
         {
            Debug.Log("Grounded");
            return true;
         }
      }
      return false;
   }
   
   
}
