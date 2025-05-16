using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;

    public bool isGrounded;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // 플레이어의 가로 이동
        float h = Input.GetAxisRaw("Horizontal");

        rigid.linearVelocity = new Vector2(h * 5f, rigid.linearVelocityY);

        // 플레이어의 점프
        if (Input.GetAxisRaw("Vertical") == 1 && isGrounded)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocityX, 7f);
        }

        // 플레이어의 방향전환
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            spriteRenderer.flipX = true;
        }
        
        // 플레이어의 좌우 애니메이션
        if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    // 땅에 닿았는지 여부
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }
    }

}
