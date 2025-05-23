using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPad : MonoBehaviour
{
    public enum MoveDirection
    {
        Vertical,
        Horizontal
    }

    [Header("Movement")] 
    public MoveDirection moveDirection = MoveDirection.Horizontal;
    public float speed = 2;
    public float moveDistance;
    public Transform player;

    private Vector3 targetPos;

    void Start()
    {
        targetPos = transform.position;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        float offset = Mathf.PingPong(Time.time * speed, moveDistance);
        Vector3 newPos = targetPos;

        switch (moveDirection)
        {
            case MoveDirection.Vertical:
                newPos.y += offset;
                break;
            case MoveDirection.Horizontal:
                newPos.x += offset;
                break;
        }

        transform.position = newPos;
    }
    
    //-----------------------------------------------------------------------------------------------------
    
    bool IsGrounded()
    {
        // Ray[] rays = new Ray[4]
        // {
        //     new Ray(player.position + (player.forward * 0.1f) + (player.up * 0.01f), Vector3.down),
        //     new Ray(player.position + (-player.forward * 0.1f) + (player.up * 0.01f), Vector3.down),
        //     new Ray(player.position + (player.right * 0.1f) + (player.up * 0.01f), Vector3.down),
        //     new Ray(player.position + (-player.right * 0.1f) + (player.up * 0.01f), Vector3.down),
        // };
        //
        // for (int i = 0; i < rays.Length; i++)
        // {
        //     Debug.DrawRay(rays[i].origin, rays[i].direction * 0.1f, Color.red, 1f);
        //     if (Physics.Raycast(rays[i],1f))
        //     {
        //         return true;
        //     }
        // }
        // return false;
        
        Vector3 origin = player.position + Vector3.up * 0.1f; // ¾à°£ À§¿¡¼­
        Ray ray = new Ray(origin, Vector3.down);

        Debug.DrawRay(origin, Vector3.down * 0.2f, Color.green, 1f);

        if (Physics.Raycast(ray, out RaycastHit hit, 1f))
        {
            // ÇöÀç ÇÃ·§Æû°ú ºÎµúÈù °æ¿ì¿¡¸¸ ÀÎÁ¤
            return hit.collider.gameObject == this.gameObject;
        }

        return false;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag("Player")) return;

        if (IsGrounded())
        {
            player.SetParent(transform);
        }
        
    }

    private void OnCollisionExit(Collision other)
    {
        if (other.collider.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}