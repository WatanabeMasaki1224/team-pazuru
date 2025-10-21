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
			rb.linearVelocity = new Vector2(rb.linearVelocityX, _jumpForce);
		}
	}
}
