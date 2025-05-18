using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player player;
    BlockingBlock blockingBlock;

    private bool flip;
    private void Awake()
    {
        player = FindObjectOfType<Player>();
        blockingBlock = FindObjectOfType<BlockingBlock>();
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
        // 진로 방해 블럭과 만났을 때
        if (collision.gameObject.CompareTag("Blocking"))
        {
            blockingBlock.HitBlocking();
            DestroyBullet();
        }
    }

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
