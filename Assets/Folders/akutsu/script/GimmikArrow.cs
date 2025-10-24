using UnityEngine;

public class GimmikArrow : MonoBehaviour
{
	[SerializeField] float _power = 20f;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.tag == "Ball")
        {
			Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();

			rb.linearVelocity = Vector2.zero;

			rb.AddForce(transform.up * _power, ForceMode2D.Impulse);
		}
	}
}
