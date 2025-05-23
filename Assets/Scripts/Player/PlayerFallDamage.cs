using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallDamage : MonoBehaviour
{
    [Header("최소낙하높이 & 낙하대미지")]
    public float minFallSpeed;
    public float perFallDamage;

    private float fallSpeed;
    private float fallDamage;
    private IDamageIbe damageIbe;
    private Rigidbody rigid;
    private bool isFalling;

    private void Start()
    {
        rigid = GetComponent<Rigidbody>();
        damageIbe = GetComponent<IDamageIbe>();
    }

    private void Update()
    {
        FallDamage();
    }


    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, 1f);
    }

    void FallDamage()
    {
        bool isGrounded = IsGrounded();
        if (isGrounded && !isFalling)
        {
            fallSpeed = -rigid.velocity.y;

            if (fallSpeed >= minFallSpeed)
            {
                fallDamage= (fallSpeed-minFallSpeed)*perFallDamage;
                damageIbe?.TakePhysicalDamage(fallDamage);
            }
        }

        isFalling = isGrounded;
    }
}
