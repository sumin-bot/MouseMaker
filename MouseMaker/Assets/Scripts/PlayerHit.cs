using UnityEngine;
using System.Collections;

public class PlayerHit : MonoBehaviour
{
    public int health = 100;

    private Rigidbody2D rigid;
    private PlayerMove playerMove;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        playerMove = GetComponent<PlayerMove>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            takeDamage(20, collision);
        }
    }

    void takeDamage(int damage, Collision2D collision)
    {
        // �÷��̾� �ǰ�
        health -= damage;

        // �÷��̾� �˹�
        Vector2 knockback = (transform.position - collision.transform.position).normalized;
        rigid.linearVelocity = Vector2.zero;
        rigid.AddForce(knockback * 5.0f, ForceMode2D.Impulse);

        // �÷��̾� �˹�� ������ ����
        StartCoroutine(disableMove());
    }

    IEnumerator disableMove()
    {
        playerMove.canMove = false;
        yield return new WaitForSeconds(1.5f);
        playerMove.canMove = true;
    }
}
