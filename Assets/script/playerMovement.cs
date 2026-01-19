using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using UnityEngine.UI;
using Unity.Mathematics;




public class PlayerMovement : MonoBehaviour
{
    [Header("Input Reference")]
    public InputActionReference move;
    public InputActionReference sprint;
    public InputActionReference act;
    [SerializeField] private GameObject controller;
    [SerializeField] private GameObject sprint_button;
    private Joystick joystick;
    [Header("game reference")]
    private Rigidbody2D rb;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float sprintspeed = 8f;
    public GameObject[] playerAm;
    private float _movesp;
    private Vector2 moveDirection;
    
    private Vector2 latestMovement;

    private bool isSprinting = false;
    public bool isActing;
    public bool trigged;
    [Header("script reference")]
    [SerializeField] private sprinting_button sb;
    [SerializeField] private acting_button ab;
    private playerBar pb;

    private puzzle puz;


    [Header("game settings")]
    public bool isPC;
    private PlayerInputActions playerControls;
    private bool playerstate = true;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        _movesp = moveSpeed;
        joystick = controller.GetComponent<Joystick>();
        playerControls = new PlayerInputActions();
        pb = GetComponent<playerBar>();
        
        ab = GameObject.FindGameObjectWithTag("button").GetComponent<acting_button>();
        puz = GameObject.FindGameObjectWithTag("puzzle").GetComponent<puzzle>();
    }
    void Update()
    {  
        if (isPC) 
        {
            controller.SetActive(false);
            sprint_button.SetActive(false);
        }
        Debug.Log(playerstate);
        moveDirection = isPC ? move.action.ReadValue<Vector2>() : joystick.Direction;
        moveDirection = isPC ? moveDirection : math.round(moveDirection);
        isSprinting = isPC ? sprint.action.ReadValue<float>() > 0f : sb.pressed; 
        isActing = isPC ? act.action.WasPressedThisFrame() : ab.pressed;  
        for (int i =0;i < playerAm.Length; i++)
        {
            playerAm[i].GetComponent<Animator>().SetFloat("Xmove",moveDirection.x);
            playerAm[i].GetComponent<Animator>().SetFloat("Ymove",moveDirection.y);
            playerAm[i].GetComponent<Animator>().SetFloat("XY",moveDirection.x+moveDirection.y);
        }
    }
    void OnEnable()
    {
        move.action.Enable();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("puzzle"))
        {
            trigged = true;
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("puzzle"))
        {
            trigged = false;
        }
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
        if (playerstate)
        {
            controller.SetActive(true);
            sprint_button.SetActive(true);
            rb.linearVelocity = moveDirection * _movesp;
            puz.GUI(true);
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
            controller.SetActive(false);
            sprint_button.SetActive(false);
            puz.GUI(false);
            
        }
    }
    public void PlayerState(bool statement)
    {
        playerstate = statement; 
    }
    public void initAnimation(int index)
    {
        for (int i = 1; i <=4; i++)
        {
            if (i != index)
            {
                playerAm[i].SetActive(false);
            }
            else
            {
                playerAm[i].SetActive(true);
            }
        }
    }
    public void down_init()
    {
        initAnimation(2);
    }
    public void left_init()
    {
        initAnimation(3);
    }
    public void right_init()
    {
        initAnimation(4);
    }
    public void up_init()
    {
        initAnimation(1);
    }
}


