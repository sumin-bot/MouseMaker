using UnityEngine;

public class Bullet : MonoBehaviour
{
    Player player;

    private bool flip;
    private void Awake()
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

    void DestroyBullet()
    {
        Destroy(gameObject);
    }
}
