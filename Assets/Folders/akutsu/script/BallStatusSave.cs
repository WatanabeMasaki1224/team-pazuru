using UnityEngine;

public class BallStatusSave : MonoBehaviour
{
	Rigidbody2D _rb = default;
	float _angularVelocity;
	Vector3 _velocity;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();
	}

	private void OnEnable()
	{
		InGamePauseManager.OnPause += Pause;
		InGamePauseManager.OnResume += Resume;
	}

	private void OnDisable()
	{
		InGamePauseManager.OnPause -= Pause;
		InGamePauseManager.OnResume -= Resume;
	}

	void Pause()
	{
		_angularVelocity = _rb.angularVelocity;
		_velocity = _rb.linearVelocity;
		_rb.Sleep();
	}

	void Resume()
	{
		_rb.WakeUp();
		_rb.angularVelocity = _angularVelocity;
		_rb.linearVelocity = _velocity;
	}
}
