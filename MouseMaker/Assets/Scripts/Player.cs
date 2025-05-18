using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    public GameController gameController;

    private bool isGrounded;
    private bool canMove = true;
    [SerializeField]
    private float coolTime;
    private float bulletTime;

    public GameObject bulletPrefab;
    public bool flip = false;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
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
                flip = false;
            }
            else if (Input.GetAxisRaw("Horizontal") == -1)
            {
                spriteRenderer.flipX = true;
                flip = true;
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

            if (bulletTime <= 0)
            {
                // �÷��̾��� ����
                if (Input.GetAxisRaw("Vertical") == -1)
                {
                    Instantiate(bulletPrefab, transform.position, transform.rotation);
                }
                bulletTime = coolTime;
            }
            bulletTime -= Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ����� ��
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }

        // ������ �浹
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20, collision);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // ������ �������� ��
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ������� �浹
        if (collision.gameObject.CompareTag("Apple"))
        {
            gameController.AddAppleCount(1);
            collision.gameObject.SetActive(false);
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
