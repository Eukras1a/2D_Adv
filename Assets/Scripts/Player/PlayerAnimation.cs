using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator anim;
    Rigidbody2D rb;
    PhysicsCheck physicsCheck;
    PlayerController controller;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        physicsCheck = GetComponent<PhysicsCheck>();
        controller = GetComponent<PlayerController>();
    }
    private void Update()
    {
        SetAnimation();
    }
    void SetAnimation()
    {
        anim.SetFloat("velocityX", Mathf.Abs(rb.velocity.x));
        anim.SetFloat("velocityY", rb.velocity.y);
        anim.SetBool("isGround", physicsCheck.isGround);
        anim.SetBool("isDead", GetComponent<PlayerController>().isDead);
        anim.SetBool("isAttack", controller.isAttack);
    }
    public void PlayerHurt()
    {
        anim.SetTrigger("hurt");
    }
    public void PlayerAttack()
    {
        anim.SetTrigger("attack");
    }
}
