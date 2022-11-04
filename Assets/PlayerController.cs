using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.TerrainTools;
using UnityEngine.UI;
using Object = System.Object;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;

    [Header("Input")]
    public Vector2 RawMove;
    public bool sprint;
    public bool jumpUp;
    public bool jumpDown;

    public float HorizontalMoveSpeed, VerticalMoveSpeed;
    public float MAXJumpTime = 0.15f; // how long the player can hold the jump button to get higher
    public float ADDITIONALJumpForce = 10; // How much additional force is added, higher value -> less force
    public const float GRAVITY_SCALE = 5f; // normal gravity scale when not falling down
    public const float FALLING_GRAVITY_SCALE = 8f; // how much gravity affects the player when falling down
    public float MIN_FORCE = 5;
    public float MAX_FORCE_NORMAL = 6;
    public float MAX_FORCE_FAST = 10;
    private float _maxForce; // current speed
    [SerializeField] private bool _isGrounded;
    private Vector2 _movement;
    private Vector2 _jumpForce;
    private float _currentJumpTime;
    private bool _jumping;
    private bool _looksLeft; // if the character is currently looking left

    private void Start()
    {
        if(_rigidbody2D = GetComponent<Rigidbody2D>()){
            Debug.Log($"Rigidbody (ID:{_rigidbody2D.GetInstanceID()}) pulled!");
        }
    }

    private void CalculateMovement(){

        if(_isGrounded){
            _rigidbody2D.AddForce(RawMove * (sprint ? MAX_FORCE_FAST : MIN_FORCE), ForceMode2D.Force);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetKey(KeyCode.D)) // Move right
        // {
        //     _movement.x = 1;
        //     transform.rotation = Quaternion.Euler(0, 0, 0);
        //     _looksLeft = false;

        //     if (Input.GetKey(KeyCode.LeftShift))
        //     {
        //         _maxForce = MAX_FORCE_FAST;
        //     }
        //     else
        //     {
        //         _maxForce = MAX_FORCE_NORMAL;
        //     }
        // }
        // else if (Input.GetKey(KeyCode.A)) // Move left
        // {
        //     _movement.x = -1;
        //     transform.rotation = Quaternion.Euler(180, 0, -180);
        //     _looksLeft = true;

        //     if (Input.GetKey(KeyCode.LeftShift))
        //     {
        //         _maxForce = MAX_FORCE_FAST;
        //     }
        //     else
        //     {
        //         _maxForce = MAX_FORCE_NORMAL;
        //     }
        // }
        // else // No horizontal movement
        // {
        //     _movement.x = 0;
        // }

        // if (Input.GetKey(KeyCode.W)) // Jump
        // {
        //     _movement.y = 1;
        //     if (_jumping)
        //     {
        //         _currentJumpTime += Time.deltaTime;
        //     }
        // }
        // else
        // {
        //     _movement.y = 0;
        //     if (_jumping)
        //     {
        //         _jumping = false;
        //         _currentJumpTime = 0;
        //     }
        // }
    }

    private void FixedUpdate()
    {
        // CalculateMovement();
        MoveCharacter();
        
    //     if (_movement.x < 0) // Left movement
    //     {
    //         GetComponent<Rigidbody2D>().AddForce(new Vector2(-HorizontalMoveSpeed * Time.fixedDeltaTime,
    //             0), ForceMode2D.Force);

    //         if (GetComponent<Rigidbody2D>().velocity.x > -MIN_FORCE)
    //         {
    //             GetComponent<Rigidbody2D>().velocity = new Vector2(-MIN_FORCE, GetComponent<Rigidbody2D>().velocity.y);
    //         }
    //         else if (GetComponent<Rigidbody2D>().velocity.x < -_maxForce)
    //         {
    //             GetComponent<Rigidbody2D>().velocity = new Vector2(-_maxForce, GetComponent<Rigidbody2D>().velocity.y);
    //         }
    //     }
    //     else if (_movement.x > 0) // Right movement
    //     {
    //         GetComponent<Rigidbody2D>().AddForce(new Vector2(+HorizontalMoveSpeed * Time.fixedDeltaTime,
    //             0), ForceMode2D.Force);

    //         if (GetComponent<Rigidbody2D>().velocity.x < MIN_FORCE)
    //         {
    //             GetComponent<Rigidbody2D>().velocity = new Vector2(MIN_FORCE, GetComponent<Rigidbody2D>().velocity.y);
    //         }
    //         else if (GetComponent<Rigidbody2D>().velocity.x > _maxForce)
    //         {
    //             GetComponent<Rigidbody2D>().velocity = new Vector2(_maxForce, GetComponent<Rigidbody2D>().velocity.y);
    //         }
    //     }
    //     else // None
    //     {
    //         GetComponent<Rigidbody2D>().velocity = new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
    //     }

    //     if (_movement.y > 0) // Player presses jump button
    //     {
    //         _Jump();
    //     }

    //     // When the player falls down, the gravity scale is increased
    //     GetComponent<Rigidbody2D>().gravityScale =
    //         GetComponent<Rigidbody2D>().velocity.y < 0 ? FALLING_GRAVITY_SCALE : GRAVITY_SCALE;
    // }

    // private void _Jump()
    // {
    //     // If the player just started jumping, the jump height can be increased by holding jump button
    //     if (_jumping && _currentJumpTime <= MAXJumpTime)
    //     {
    //         GetComponent<Rigidbody2D>()
    //             .AddForce(Vector2.up * Time.fixedDeltaTime * VerticalMoveSpeed / ADDITIONALJumpForce,
    //                 ForceMode2D.Impulse);
    //     }

    //     if (!_touchesGround ||
    //         GetComponent<Rigidbody2D>().velocity.y <
    //         -0.001) // player cant jump if he doesnt touch the ground or is falling down
    //     {
    //         return;
    //     }

    //     _touchesGround = false;
    //     _jumping = true;
    //     GetComponent<Rigidbody2D>().AddForce(Vector2.up * Time.fixedDeltaTime * VerticalMoveSpeed, ForceMode2D.Impulse);
    }

    private void MoveCharacter() {
        var pos = transform.position;
        Vector3 move = RawMove * Time.deltaTime;
        var furthestPoint = pos + move;

        transform.position += move * (sprint ? MAX_FORCE_FAST : MIN_FORCE);
    }


    private void OnCollisionStay2D(Collision2D other)
    {
        // if (other.collider.CompareTag("SolidObject"))
        // {
        //     ContactPoint2D contactPoint = other.GetContact(0);

        //     if (Vector3.Dot(contactPoint.normal, Vector3.up) > 0.5)
        //     {
        //         _touchesGround = true;
        //         _currentJumpTime = 0;
        //     }
        // }
    }
}