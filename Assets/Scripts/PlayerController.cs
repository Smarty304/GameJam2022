using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private InputActionReference _movementControl;
    [SerializeField] private InputActionReference _throw;

    [Header("BottleStuff")] [SerializeField]
    GameObject currentBottle; // WorkAround for Bottle PickUp

    [Header("SoundStuff")]
    
    [SerializeField] float throwForce = 10f;
    bool isAliveBottle = false;

    [Header("Movement Settings")] public float HorizontalMoveSpeed, VerticalMoveSpeed = 400;
    public float MaxJumpTime = 0.15f;
    public float AdditionalJumpForce = 10;
    public const float GRAVITY_SCALE = 5;
    public const float FALLING_GRAVITY_SCALE = 8f;
    public const float MIN_FORCE = 5;
    public const float MAX_FORCE_NORMAL = 6;

    private bool _jumping;
    private bool _touchesGround;
    private float _currentJumpTime;
    private int _currentSlot; // which slot is currently selected
    private bool _looksLeft;
    private InventorySlot[] _slots;
    private Rigidbody2D _myRigidbody;

    AudioPlayer audioPlayer;

    private void Awake()
    {
        audioPlayer = FindObjectOfType<AudioPlayer>();
    }
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
        _slots = new InventorySlot[] {new InventorySlot(), new InventorySlot(), new InventorySlot()};
        _currentSlot = 0;
    }

    void Update()
    {
        startAnim();
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

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _currentSlot = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _currentSlot = 1;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _currentSlot = 2;
        }

        currentBottle = _slots[_currentSlot].Item;

        if (_throw.action.WasPerformedThisFrame())
        {
            ThrowBottle();
        }


        GetComponent<SpriteRenderer>().flipX = _looksLeft;
    }
    private void startAnim()
    {
        if (_touchesGround != true) { animator.SetBool("inair", true); }
        else { animator.SetBool("inair", false); }
        if (MathF.Abs(_myRigidbody.velocity.x) <= 0.1) { animator.SetBool("running", false); }
        else { animator.SetBool("running", true); }
    }
    private void FixedUpdate()
    {
        Vector2 movement = _movementControl.action.ReadValue<Vector2>();

        if (movement.x < 0) // Move left
        {
            _looksLeft = true;
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
        else if (movement.x > 0) // Move right
        {
            _looksLeft = false;
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
            ContactPoint2D[] contactPoints = new ContactPoint2D[4];
            for (int i = 0; i < other.GetContacts(contactPoints); i++)
            {
                if (Vector3.Dot(contactPoints[i].normal, Vector3.up) > 0.5)
                {
                    _touchesGround = true;
                    _currentJumpTime = 0;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        audioPlayer.PlayBottlePickUpClip();
        if (other.CompareTag("Bottle"))
        {
            // Player collided with bottle trigger
            other.GetComponent<Bottle>().OnPickup();
            other.transform.parent = this.transform;
            _slots[_currentSlot].Item = other.gameObject;
        }
    }

    class InventorySlot
    {
        public GameObject Item { get; set; }
    }

    private void ThrowBottle()
    {
        if (_slots[_currentSlot].Item == null)
        {
            return;
        }

        if (!isAliveBottle)
        {
            audioPlayer.PlayThrowingClip();
            Vector3 spawnDirection = _looksLeft ? Vector3.left : Vector3.right;
            GameObject aliveBottle =
                Instantiate(currentBottle, transform.position + spawnDirection, Quaternion.identity);
            
            // Change physics of bottle
            aliveBottle.GetComponent<Collider2D>().isTrigger = false;
            aliveBottle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            aliveBottle.GetComponent<SpriteRenderer>().enabled = true;
            
            Physics2D.IgnoreCollision(GetComponent<Collider2D>(), aliveBottle.GetComponent<Collider2D>());
            aliveBottle.GetComponent<Collider2D>().enabled = true;
            
            Vector2 direction = _looksLeft ? new Vector2(-1f, 0.5f) : new Vector2(1f, 0.5f);
            aliveBottle.GetComponent<Rigidbody2D>()
                .AddForce(((direction) * throwForce));

            isAliveBottle = true;
            Invoke(nameof(killBottle), 5f);
        }
    }

    void killBottle()
    {
        
        isAliveBottle = false;
    }
}