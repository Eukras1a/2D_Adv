using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    PlayerInputControl inputControl;
    [Header("基本参数")]
    public float moveSpeed;
    public float jumpForce;
    public float hurtForce;
    [NonSerialized] public bool isHurt;
    [NonSerialized] public bool isDead;
    [NonSerialized] public bool isAttack;

    [Header("物理材质")]
    public PhysicsMaterial2D normal;
    public PhysicsMaterial2D wall;

    private CapsuleCollider2D capCol;
    private Vector2 inputDirection;
    private Rigidbody2D playerRigidbody;
    private PhysicsCheck physicsCheck;
    private PlayerAnimation playerAnimation;
    private void Awake()
    {
        capCol = GetComponent<CapsuleCollider2D>();
        inputControl = new PlayerInputControl();
        playerRigidbody = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        playerAnimation = GetComponent<PlayerAnimation>();
        inputControl.GamePlay.Jump.started += PlayerJump;
        inputControl.GamePlay.Attack.started += PlayerAttack;
    }

    private void Update()
    {
        inputDirection = inputControl.GamePlay.Move.ReadValue<Vector2>();

    }
    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
        {
            PlayerMove();
        }
        CheckState();
    }
    #region -----------------------------------Player-----------------------------------
    Vector3 tempScale = Vector3.one;
    private void PlayerMove()
    {
        playerRigidbody.velocity = new Vector2(moveSpeed * inputDirection.x * Time.deltaTime, playerRigidbody.velocity.y);
        if (inputDirection.x > 0)
        {
            tempScale.x = 1;
        }
        else if (inputDirection.x < 0)
        {
            tempScale.x = -1;
        }
        transform.localScale = tempScale;
    }
    private void PlayerJump(InputAction.CallbackContext context)
    {
        if (physicsCheck.isGround)
            playerRigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
    }
    public void GetHurt(Transform t)
    {
        isHurt = true;
        playerRigidbody.velocity = Vector2.zero;
        Vector2 dir = new Vector2(transform.position.x - t.position.x, 0).normalized;

        playerRigidbody.AddForce(dir * hurtForce, ForceMode2D.Impulse);
    }
    private void PlayerAttack(InputAction.CallbackContext context)
    {
        playerAnimation.PlayerAttack();
        isAttack = true;
    }
    void CheckState()
    {
        capCol.sharedMaterial = physicsCheck.isGround ? normal : wall;
    }
    public void PlayerDead()
    {
        isDead = true;
        inputControl.GamePlay.Disable();
    }
    #endregion ----------------------------------Player-----------------------------------
    private void OnEnable()
    {
        inputControl.Enable();
    }
    private void OnDisable()
    {
        inputControl.Disable();
    }
}
