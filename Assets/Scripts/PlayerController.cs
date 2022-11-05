using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _movementControl;
    [SerializeField] private InputActionReference _throw;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _movementVelocity = 3.0f;
    private Rigidbody2D _myRigidbody;

    public float HorizontalMoveSpeed, VerticalMoveSpeed = 400;
    public float MaxJumpTime = 0.15f;
    public float AdditionalJumpForce = 10;
    public const float GRAVITY_SCALE = 5;
    public const float FALLING_GRAVITY_SCALE = 8f;
    public const float MIN_FORCE = 5;
    public const float MAX_FORCE_NORMAL = 6;

    private bool _jumping;
    private bool _touchesGround;
    private float _currentJumpTime;

    private void OnEnable()
    {
        _movementControl.action.Enable();
        _throw.action.Enable();
    }

    private void OnDisable()
    {
        _movementControl.action.Disable();
        _throw.action.Disable();
    }

    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (_movementControl.action.ReadValue<Vector2>().y > 0)
        {
            // Jumps
            if (_jumping)
            {
                _currentJumpTime += Time.deltaTime;
            }
        }
        else
        {
            if (_jumping)
            {
                _jumping = false;
                _currentJumpTime = 0;
            }
        }
    }

    private void FixedUpdate()
    {
        Vector2 movement = _movementControl.action.ReadValue<Vector2>();

        if (movement.x < 0)
        {
            _myRigidbody.AddForce(new Vector2(-HorizontalMoveSpeed * Time.fixedDeltaTime, 0), ForceMode2D.Force);

            if (_myRigidbody.velocity.x > -MIN_FORCE)
            {
                _myRigidbody.velocity = new Vector2(-MIN_FORCE, _myRigidbody.velocity.y);
            }
            else if (_myRigidbody.velocity.x < -MAX_FORCE_NORMAL)
            {
                _myRigidbody.velocity = new Vector2(-MAX_FORCE_NORMAL, _myRigidbody.velocity.y);
            }
        }
        else if (movement.x > 0)
        {
            _myRigidbody.AddForce(new Vector2(HorizontalMoveSpeed * Time.fixedDeltaTime, 0), ForceMode2D.Force);

            if (_myRigidbody.velocity.x < MIN_FORCE)
            {
                _myRigidbody.velocity = new Vector2(MIN_FORCE, _myRigidbody.velocity.y);
            }
            else if (_myRigidbody.velocity.x > MAX_FORCE_NORMAL)
            {
                _myRigidbody.velocity = new Vector2(MAX_FORCE_NORMAL, _myRigidbody.velocity.y);
            }
        }
        else
        {
            _myRigidbody.velocity = new Vector2(0, _myRigidbody.velocity.y);
        }

        if (movement.y > 0)
        {
            Jump();
        }

        _myRigidbody.gravityScale =
            _myRigidbody.velocity.y < 0 ? FALLING_GRAVITY_SCALE : GRAVITY_SCALE;
    }

    private void Jump()
    {
        // If the player just started jumping, the jump height can be increased by holding jump button
        if (_jumping && _currentJumpTime <= MaxJumpTime)
        {
            _myRigidbody
                .AddForce(Vector2.up * Time.fixedDeltaTime * VerticalMoveSpeed / AdditionalJumpForce,
                    ForceMode2D.Impulse);
        }

        if (!_touchesGround ||
            _myRigidbody.velocity.y <
            -0.001) // player cant jump if he doesnt touch the ground or is falling down
        {
            return;
        }

        _touchesGround = false;
        _jumping = true;
        _myRigidbody.AddForce(Vector2.up * Time.fixedDeltaTime * VerticalMoveSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionStay2D(Collision2D other)
    { 
        if (other.collider.CompareTag("SolidObject"))
        {
            ContactPoint2D contactPoint = other.GetContact(0);

            if (Vector3.Dot(contactPoint.normal, Vector3.up) > 0.5)
            {
                _touchesGround = true;
                _currentJumpTime = 0;
            }
        }
    } 
}
