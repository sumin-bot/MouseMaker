using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    GameController gameController;

    private bool isGrounded;
    private bool canMove = true;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        gameController = FindObjectOfType<GameController>();
    }

    private void FixedUpdate()
    {
        if (canMove)
        {
            // �÷��̾��� ���� �̵�
            float h = Input.GetAxisRaw("Horizontal");

            rigid.linearVelocity = new Vector2(h * 5.0f, rigid.linearVelocityY);

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

    public void TakeDamage(int damage, Collision2D collision)
    {
        // �÷��̾� �ǰ�
        gameController.health -= damage;

        // �÷��̾� HP UI ����
        gameController.ChangeHealthUI();

        // �÷��̾� �˹�
        Vector2 knockback = (transform.position - collision.transform.position).normalized;
        rigid.linearVelocity = Vector2.zero;
        rigid.AddForce(knockback * 5.0f, ForceMode2D.Impulse);

        // �÷��̾� �˹�� ������ ����
        StartCoroutine(disableMove());
    }

    IEnumerator disableMove()
    {
        canMove = false;
        yield return new WaitForSeconds(1.0f);
        canMove = true;
    }

}
