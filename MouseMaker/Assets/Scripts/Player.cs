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
            // 플레이어의 가로 이동
            float h = Input.GetAxisRaw("Horizontal");

            rigid.linearVelocity = new Vector2(h * 5.0f, rigid.linearVelocityY);

            // 플레이어의 점프
            if (Input.GetAxisRaw("Vertical") == 1 && isGrounded)
            {
                rigid.linearVelocity = new Vector2(rigid.linearVelocityX, 7f);
            }

            // 플레이어의 방향전환
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

            // 플레이어의 좌우 애니메이션
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
                // 플레이어의 공격
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
        // 땅에 닿았을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            anim.SetBool("isJumping", false);
        }

        // 적과의 충돌
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage(20, collision);
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        // 땅에서 떨어졌을 때
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            anim.SetBool("isJumping", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 사과와의 충돌
        if (collision.gameObject.CompareTag("Apple"))
        {
            gameController.AddAppleCount(1);
            collision.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage, Collision2D collision)
    {
        // 플레이어 피격
        gameController.health -= damage;

        // 플레이어 HP UI 변경
        gameController.ChangeHealthUI();

        // 플레이어 넉백
        Vector2 knockback = (transform.position - collision.transform.position).normalized;
        rigid.linearVelocity = Vector2.zero;
        rigid.AddForce(knockback * 5.0f, ForceMode2D.Impulse);

        // 플레이어 넉백시 움직임 제한
        StartCoroutine(disableMove());
    }

    IEnumerator disableMove()
    {
        canMove = false;
        yield return new WaitForSeconds(1.0f);
        canMove = true;
    }
}
