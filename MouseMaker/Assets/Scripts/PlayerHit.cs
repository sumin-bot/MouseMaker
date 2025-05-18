using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    public GameController gameController;
    public PlayerMove playerMove;
    private void Awake()
    {
        gameController = FindObjectOfType<GameController>();
        playerMove = GetComponent<PlayerMove>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 적과의 충돌
        if (collision.gameObject.CompareTag("Enemy"))
        {
            playerMove.TakeDamage(20, collision);
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
}
