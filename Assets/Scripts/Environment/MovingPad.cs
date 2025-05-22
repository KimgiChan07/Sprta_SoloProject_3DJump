using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPad : MonoBehaviour
{
    public Transform pointA;
    public Transform pointB;
    public float speed = 2;
    public Transform player;

    private Vector3 target;

    void Start()
    {
        target = pointB.position;
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.1f)
        {
            target = target == pointA.position ? pointB.position : pointA.position;
        }
    }
    
    bool IsGrounded()
    {
        Ray[] rays = new Ray[4]
        {
            new Ray(player.position + (player.forward * 0.2f) + (player.up * 0.01f), Vector3.down),
            new Ray(player.position + (-player.forward * 0.2f) + (player.up * 0.01f), Vector3.down),
            new Ray(player.position + (player.right * 0.2f) + (player.up * 0.01f), Vector3.down),
            new Ray(player.position + (-player.right * 0.2f) + (player.up * 0.01f), Vector3.down),
        };
      
        for (int i = 0; i < rays.Length; i++)
        {
            Debug.DrawRay(rays[i].origin, rays[i].direction * 0.1f, Color.red, 1f);
            if (Physics.Raycast(rays[i],1f))
            {
                return true;
            }
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