using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player player;

    private bool flip;
    private void Start()
    {
        player = FindObjectOfType<Player>();
        flip = player.flip;
        Invoke("DestroyBullet", 2);
    }
    private void Update()
    {
        if (flip)
        {
            transform.Translate(transform.right * -10 * Time.deltaTime);
        }
        else
        {
            transform.Translate(transform.right * 10 * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ���� ���� ������ ��
        if (collision.gameObject.CompareTag("Blocking"))
        {
            BlockingBlock block = collision.GetComponent<BlockingBlock>();
            block.HitBlocking();
            Destroy(gameObject);
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
