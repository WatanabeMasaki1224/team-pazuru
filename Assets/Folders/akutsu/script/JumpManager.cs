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

            // 上向きジャンプ台の場合：落下中に跳ね返す
            if (v.y < 0)
            {
                v.y = Mathf.Abs(v.y); // Y速度を正にして上に跳ねる
            }
            // 下向きジャンプ台の場合：上昇中に跳ね返す
            else if (v.y > 0)
            {
                v.y = -Mathf.Abs(v.y); // Y速度を負にして下に跳ねる
            }

            rb.linearVelocity = v.normalized * _jumpForce;
        }
	}
}
