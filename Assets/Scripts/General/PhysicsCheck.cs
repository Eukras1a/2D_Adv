using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    [Header("当前状态")]
    public bool isGround;
    public bool TouchLeftWall;
    public bool TouchRightWall;

    [Header("检测参数")]
    public bool manual;
    public Vector2 bottomOffset;
    public Vector2 leftOffset;
    public Vector2 rightOffset;
    public float checkRadius;
    public LayerMask groundLayer;

    private CapsuleCollider2D col;
    private void Awake()
    {
        col = GetComponent<CapsuleCollider2D>();
        if (!manual)
        {
            rightOffset = new Vector2((col.bounds.size.x / 2) + col.offset.x, col.offset.y);
            leftOffset = new Vector2(-rightOffset.x, rightOffset.y);
        }
    }
    private void Update()
    {
        Check();
    }
    void Check()
    {
        isGround = Physics2D.OverlapCircle((Vector2)transform.position + (bottomOffset * transform.localScale), checkRadius, groundLayer);
        TouchLeftWall = Physics2D.OverlapCircle((Vector2)transform.position + leftOffset, checkRadius, groundLayer);
        TouchRightWall = Physics2D.OverlapCircle((Vector2)transform.position + rightOffset, checkRadius, groundLayer);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere((Vector2)transform.position + (bottomOffset * transform.localScale), checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + leftOffset, checkRadius);
        Gizmos.DrawWireSphere((Vector2)transform.position + rightOffset, checkRadius);
    }
}
