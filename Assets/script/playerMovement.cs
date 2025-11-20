using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;




public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintspeed = 8f;
    private float _movesp;
    private Vector2 moveDirection;
    public InputActionReference move;
    public InputActionReference sprint;
    public InputActionReference act;
    private Vector2 latestMovement;
    private Animator playerAm;
    private bool isSprinting = false;
    public bool isActing;
    private PlayerControl playerControls;
    private playerBar pb;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAm = GetComponent<Animator>();
        _movesp = moveSpeed;
        playerControls = new PlayerControl();
        pb = GetComponent<playerBar>();
    }
    void Update()
    {
        moveDirection = move.action.ReadValue<Vector2>();
        isSprinting = sprint.action.ReadValue<float>() > 0f;
        isActing = act.action.WasPressedThisFrame();
        playerAm.SetFloat("Ymove", moveDirection.y);
        playerAm.SetFloat("Xmove", moveDirection.x);
        playerAm.SetFloat("XY", moveDirection.x + moveDirection.y);
    }
    void OnEnable()
    {
        move.action.Enable();
    }


    void OnDisable()
    {
        move.action.Disable();
    }
    void LateUpdate()
    {
        if (moveDirection.sqrMagnitude > 0)
            latestMovement = moveDirection;
    }




    void FixedUpdate()
    {
        if (isSprinting && moveDirection!= Vector2.zero)
        {
            _movesp = sprintspeed;
            pb.SliderTovaule(0);
        }
        else
        {
            _movesp = moveSpeed;
            pb.SliderTovaule(100);
        }
        if (pb.slider.value < 6f)
        {
            _movesp = moveSpeed;
        }
        rb.linearVelocity = moveDirection * _movesp;  
    }
}


