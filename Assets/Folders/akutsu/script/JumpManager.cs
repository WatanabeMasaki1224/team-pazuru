using UnityEngine;

public class JumpManager: MonoBehaviour
{
    [SerializeField] float _jumpForce = 4f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Ball")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 v = rb.linearVelocity;

            if (v.y == 0)
            {
                v.y = _jumpForce; // ��~���Ȃ��ɒ��˂�
                rb.linearVelocity = v;
                return;
            }
            else
            {
                v.y = -v.y;
            }
            rb.linearVelocity = v.normalized * _jumpForce;
        }
	}
}
