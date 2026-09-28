using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : PlayerState
{
    //dash vars
    public float DashForce;
    public float StartDashTimer;
    float currentDashTimer;
    float dashDirection;
    bool isDashing;
    public Sprite rollSprite;
    public bool isOnGround;
    private float MovementSpeed;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && isOnGround && MovementSpeed != 0)
        {
            isDashing = true;
            currentDashTimer = StartDashTimer;
            body.velocity = Vector2.zero;
            dashDirection = (int)MovementSpeed;
            SetState(CharacterState.roll);
        }

        if (isDashing)
        {
            body.velocity = transform.right * dashDirection * DashForce;

            currentDashTimer -= Time.deltaTime;

            if (currentDashTimer <= 0)
            {
                isDashing = false;
            }
        }
    }
}
