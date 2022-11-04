using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private InputActionReference _movementControl;
    [SerializeField] private InputActionReference _dig;
    [SerializeField] private float _playerSpeed = 2.0f;
    [SerializeField] private float _movementVelocity = 3.0f;
    [SerializeField] private Transform _diggingArea;
    [SerializeField] private float _digingRange = 0.5f;
    [SerializeField] private LayerMask _earthBlockLaya;
    private Animator _myAnimator;
    private Rigidbody2D _myRigidbody;
    private SpriteRenderer _myspriteRenderer;


    private void OnEnable()
    {
        _movementControl.action.Enable();
        _dig.action.Enable();
    }

    private void OnDisable()
    {
        _movementControl.action.Disable();
        _dig.action.Disable();
    }
    void Start()
    {
        _myRigidbody = GetComponent<Rigidbody2D>();
        _myAnimator = GetComponent<Animator>();
        _myspriteRenderer = GetComponent<SpriteRenderer>();
    }

    void OnDig(InputValue value)
    {
        Debug.Log("hi");
        //digging animation
        //block detection
        Collider2D[] hitGround = Physics2D.OverlapCircleAll(_diggingArea.position, _digingRange, _earthBlockLaya);


        //destroy block
        foreach(Collider2D _ground in hitGround)
        {
            _ground.GetComponent<blockemyControl>().TakeDamage();
            Debug.Log("Diggi Diggi Hole");
            _myAnimator.SetBool("isDigging", true);
        }

    }
    void Update()
    {
        Vector3 movement = _movementControl.action.ReadValue<Vector2>() * _movementVelocity;
        transform.position += movement * Time.deltaTime;
        
        FlipSprite();
    }



    void FlipSprite()
    {
        Debug.Log(_movementControl.action.ReadValue<Vector2>());
        if(_movementControl.action.ReadValue<Vector2>().x < 0f) 
        {
            _myAnimator.SetBool("isDigging", false);
            _myspriteRenderer.flipX = true;
            _myAnimator.SetBool("isRunning", true);
        }
            
        
        else if(_movementControl.action.ReadValue<Vector2>().x > 0f) 
        {
            _myAnimator.SetBool("isDigging", false);
            _myspriteRenderer.flipX = false;
            _myAnimator.SetBool("isRunning", true);
        }
        else
        {
            
            _myAnimator.SetBool("isRunning", false);
            Debug.Log("I am standing still");
        }
        
    }

}
