using UnityEngine;

public class Player : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        // �÷��̾��� ���� �̵�
        float h = Input.GetAxisRaw("Horizontal");
        transform.position += new Vector3(h * 0.05f, 0, 0);

        // �÷��̾��� ������ȯ
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            spriteRenderer.flipX = false;
        }
        else if (Input.GetAxisRaw("Horizontal") == -1)
        {
            spriteRenderer.flipX = true;
        }
        
        // �÷��̾��� �ִϸ��̼�
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
