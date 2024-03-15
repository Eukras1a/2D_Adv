using System.Collections;
using UnityEngine;
public class Enemy : MonoBehaviour
{
    [HideInInspector] public Rigidbody2D rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public PhysicsCheck physicsCheck;

    [Header("基本参数")]
    public float chaseSpeed;
    public float normalSpeed;
    public float hurtForce;

    [Header("计时器")]
    public float waitTime;
    public float lostTime;
    public bool wait;

    Transform attacker;
    [HideInInspector] public Vector3 currentDir;

    protected BaseState currentState;
    protected BaseState patrolState;
    protected BaseState chaseState;

    [Header("检测参数")]
    public Vector2 centenOffset;
    public Vector2 checkSize;
    public float checkDistance;
    public LayerMask attackLayer;

    [HideInInspector] public float currentSpeed;
    float waitTimer;
    [HideInInspector] public float lostTimer;
    bool isHurt;
    bool isDead;
    #region 周期函数
    private void OnEnable()
    {
        currentState = patrolState;
        currentState.OnEnter(this);
    }
    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        physicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
    }

    private void Update()
    {
        DestroyAfterAnimation();
        currentDir = new Vector3(-transform.localScale.x, 0, 0);
        currentState.OnUpdate();
        Timer();
    }
    private void FixedUpdate()
    {
        Move();
        currentState.OnFixUpdate();
    }
    private void OnDisable()
    {
        currentState.OnExit();
    }
    #endregion
    public void Move()
    {
        //TODO:奇怪的偶发性BUG，代码执行但是velocity = 0
        if (!isHurt && !isDead && !wait)
        {
            rb.velocity = new Vector2(currentSpeed * currentDir.x * Time.deltaTime, 0);
        }
    }
    public void Timer()
    {
        if (wait)
        {
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0)
            {
                wait = false;
                waitTimer = waitTime;
                transform.localScale = new Vector3(currentDir.x, 1, 1);
            }
        }
        if (!FindPlayer() && lostTimer > 0)
        {
            lostTimer -= Time.deltaTime;
        }
    }
    #region 事件
    public void OnTakeDamage(Transform t)
    {
        attacker = t;
        if (t.position.x - transform.position.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        if (t.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        isHurt = true;
        anim.SetTrigger("hurt");
        Vector2 dir = new Vector2(transform.position.x - t.position.x, 0).normalized;
        rb.velocity = new Vector2(0, rb.velocity.y);
        StartCoroutine(OnHurt(dir));
    }
    public void OnDie()
    {
        isDead = true;
        GetComponent<Attack>().enabled = false;
        anim.SetBool("dead", true);
    }
    void DestroyAfterAnimation()
    {
        if (isDead)
        {
            AnimatorStateInfo info = anim.GetCurrentAnimatorStateInfo(0);
            if (info.normalizedTime >= 0.99f)
            {
                Destroy(this.gameObject);
            }
        }

    }
    private IEnumerator OnHurt(Vector2 dir)
    {
        rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }
    public bool FindPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)centenOffset, checkSize, 0, currentDir, checkDistance, attackLayer);
    }
    public void SetState(EnemyState state)
    {
        var newState = state switch
        {
            EnemyState.Patrol => patrolState,
            EnemyState.Chase => chaseState,
            _ => null
        };
        currentState.OnExit();
        currentState = newState;
        currentState.OnEnter(this);
    }
    #endregion
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position + (Vector3)centenOffset + new Vector3(checkDistance * currentDir.x, 0), 0.2f);
    }
}
