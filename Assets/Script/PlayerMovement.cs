using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public CapsuleCollider2D coll;
    public SpriteRenderer sprite;
    public Animator anim;

    [SerializeField] private LayerMask jumpableGround;

    public float dirX = 0f;
    [SerializeField] public float moveSpeed = 7f;
    [SerializeField] public float jumpForce = 7f;


    public enum MovementState { idle, running, jumping, falling }
    public Vector2 customGravity;
    [SerializeField] public AudioSource jumpSoundEffect;

    [SerializeField] public float rayLength = 0.2f;  // 射线长度
    [SerializeField] public float raySpreadRatio = 0.4f;  // 射线间距比例（相对于碰撞器宽度）

    private bool isGrounded = false;  // 新增变量来记录是否在地面上

    [Header("Debug")]
    public bool showDebug = true;  // 是否显示调试信息
    private Color debugGroundedColor = Color.green;  // 在地面上时的调试颜色
    private Color debugAirborneColor = Color.red;    // 在空中时的调试颜色

    // Start is called before the first frame update    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    private void Update()
    {
        customGravity.x = 0;
        customGravity.y = rb.gravityScale;
        
        // 获取输入
        dirX = Input.GetAxisRaw("Horizontal");
        
        // 使用normalized保任何方向的移动速度都一致
        Vector2 moveDirection = new Vector2(dirX, 0).normalized;
        rb.velocity = new Vector2(moveDirection.x * moveSpeed, rb.velocity.y);

        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity += customGravity.normalized * jumpForce;
        }

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;
        }
        else
        {
            state = MovementState.idle;
        }

        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);

    }

    private bool IsGrounded()
    {
        return isGrounded;
    }

    private void OnGUI()
    {
        if (!showDebug) return;

        GUILayout.BeginArea(new Rect(10, 10, 300, 300));
        GUILayout.Label($"Grounded: {isGrounded}");
        GUILayout.Label($"Velocity: {rb.velocity}");
        GUILayout.Label($"Gravity Scale: {rb.gravityScale}");
        GUILayout.EndArea();
    }

    private void OnDrawGizmos()
    {
        if (!showDebug || !Application.isPlaying) return;

        // 显示角色状态
        Gizmos.color = isGrounded ? debugGroundedColor : debugAirborneColor;
        Gizmos.DrawWireSphere(transform.position, 0.5f);

        // 显示重力方向
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)(customGravity.normalized * 1f));
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        // 使用 LayerMask 检查是否是可跳跃的地面层
        if (((1 << collision.gameObject.layer) & jumpableGround) != 0)
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & jumpableGround) != 0)
        {
            isGrounded = false;
        }
    }
}








