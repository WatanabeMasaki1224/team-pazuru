using UnityEngine;

public class JumpManager: MonoBehaviour
{
    [SerializeField] float _jumpForce = 4f;

    void Start()
    {
        
    }

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Ball")
        {
            Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
            Vector2 v = rb.linearVelocity;

            // ������W�����v��̏ꍇ�F�������ɒ��˕Ԃ�
            if (v.y < 0)
            {
                v.y = Mathf.Abs(v.y); // Y���x�𐳂ɂ��ď�ɒ��˂�
            }
            // �������W�����v��̏ꍇ�F�㏸���ɒ��˕Ԃ�
            else if (v.y > 0)
            {
                v.y = -Mathf.Abs(v.y); // Y���x�𕉂ɂ��ĉ��ɒ��˂�
            }

            rb.linearVelocity = v.normalized * _jumpForce;
        }
	}
}
