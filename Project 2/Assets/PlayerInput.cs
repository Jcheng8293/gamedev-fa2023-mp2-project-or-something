using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerInput : MonoBehaviour
{
    /*****
     * Variables (Changed in runtime)
     *****/

    // Player States
    private bool grounded = false;
    private bool jumping = false;
    private bool clingRight = false;
    private bool clingLeft = false;
    private bool climb = false;
    private float lastGrounded = 0.0f;
    private float lastPressed = 0.0f;
    private Vector2 moveDirection = Vector2.zero;
    private bool combatMode = false;

    /*****
     * Constants (Changed in editor)
     *****/

    // Timers
    private float LASTGROUNDED = 0.167f;
    private int LASTVECTOR = 0;
    private float LASTPRESSED = 0.167f;

    // Roam Speeds
    private float SLOWSPEED = 5; // Slow
    private float MOVESPEED = 8; // Nomral (Player Default)
    private float FASTSPEED = 12; // Fast
    private float BLITZSPEED = 20; // Blitz (Projectile)

    // Accelration and Decceleration
    private float ACCEL = 2f;
    private float DECEL = 3f;

    /*****
     * Pure Constant (Not changed ever)
     *****/

    // Player
    public Rigidbody2D player2D;
    public PlayerInput inputActions;

    // Starts when script is compiled or something
    void Awake()
    {
        inputActions = new PlayerInput();
        player2D = GetComponent<Rigidbody2D>();
    }

    // Update
    void FixedUpdate()
    {

        // Roam
        float targetSpeedx = moveDirection.x * MOVESPEED;
        float speedDiffx = targetSpeedx - player2D.velocity.x;
        float acceleratex = (Mathf.Abs(targetSpeedx) > 0.01f) ? ACCEL : DECEL;
        float Roamx = Mathf.Pow(Mathf.Abs(speedDiffx) * acceleratex, 2) * Mathf.Sign(speedDiffx);

        float targetSpeedy = moveDirection.y * MOVESPEED;
        float speedDiffy = targetSpeedy - player2D.velocity.y;
        float acceleratey = (Mathf.Abs(targetSpeedy) > 0.01f) ? ACCEL : DECEL;
        float Roamy = Mathf.Pow(Mathf.Abs(speedDiffy) * acceleratey, 2) * Mathf.Sign(speedDiffy);

        player2D.AddForce(Roamx * Vector2.right);
        player2D.AddForce(Roamy * Vector2.up);

        // Friction
        float frictionx = Mathf.Min(Mathf.Abs(player2D.velocity.x), Mathf.Abs(0.2f));
        frictionx *= Mathf.Sign(player2D.velocity.x);
        player2D.AddForce(Vector2.right * -frictionx, ForceMode2D.Impulse);

        float frictiony = Mathf.Min(Mathf.Abs(player2D.velocity.y), Mathf.Abs(0.2f));
        frictiony *= Mathf.Sign(player2D.velocity.y);
        player2D.AddForce(Vector2.right * -frictiony, ForceMode2D.Impulse);
    }

    

    /**************************************************************************************************************************************************************************************
    * Roam Mode -=- Roam Mode -=- Roam Mode -=- Roam -=- Roam Mode -=- Roam Mode -=- Roam -=- Roam Mode -=- Roam Mode -=- Roam Mode -=- Roam Mode -=- Roam Mode -=- Roam Mode
    **************************************************************************************************************************************************************************************/

    // Get Vector Value upon Roam keypress
    public void Move(InputAction.CallbackContext context)
    {
        moveDirection = context.ReadValue<Vector2>();
    }

    /**************************************************************************************************************************************************************************************
     * Combat Mode -=- Combat Mode -=- Combat Mode -=- Combat -=- Combat Mode -=- Combat Mode -=- Combat Mode -=- Combat Mode -=- Combat Mode -=- Combat -=- Combat Mode -=- Combat Mode
     **************************************************************************************************************************************************************************************/

    public void Attack(InputAction.CallbackContext context)
    {

    }

    public void Interact(InputAction.CallbackContext context)
    {

    }
}
