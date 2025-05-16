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
        // �÷��̾��� ���� �̵�
        float h = Input.GetAxisRaw("Horizontal");

        rigid.linearVelocity = new Vector2(h * 5f, rigid.linearVelocityY);

        // �÷��̾��� ����
        if (Input.GetAxisRaw("Vertical") == 1 && isGrounded)
        {
            rigid.linearVelocity = new Vector2(rigid.linearVelocityX, 7f);
        }

        // �÷��̾��� ������ȯ
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            spriteRenderer.flipX = true;
        }
        
        // �÷��̾��� �¿� �ִϸ��̼�
        if (Input.GetButton("Horizontal"))
        {
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }

    // ���� ��Ҵ��� ����
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
