using UnityEngine;

public class BlockingBlock : MonoBehaviour
{
    [SerializeField]
    private int blockingHP = 5;

    public void HitBlocking()
    {
        blockingHP--;
    }

    private void Update()
    {
        if (blockingHP <= 0)
        {
            gameObject.SetActive(false);
        }
    }
}
