using UnityEngine;

public class BossController : MonoBehaviour
{
	public Transform Player;
	public float Speed = 20;
	public float AngularSpeed = 360;
	public SpriteRenderer SpriteRenderer;

	private Vector2 _target = Vector2.zero;
	private float _angle = 0f;
	private bool _rotating = true;
	private float _zValue = 0f;

	// Start is called before the first frame update
	private void Start()
	{
		this.enabled = false;
		this.SpriteRenderer.enabled = false;
		_zValue = this.transform.position.z;
	}

	public void FixedUpdate()
	{
		_target = this.Player.transform.position;
		Vector2 newPos = Vector2.MoveTowards(this.transform.position, _target, this.Speed * Time.fixedDeltaTime);
		this.transform.position = new Vector3(newPos.x, newPos.y, _zValue);

		if ((Vector2)this.transform.position == _target)
			_rotating = false;

		if (_rotating)
		{
			_angle += this.AngularSpeed * Time.fixedDeltaTime;
			this.transform.rotation = Quaternion.Euler(Vector3.forward * _angle);
		}
	}

	public void Summon()
	{
		float xSign = (Random.Range(0, 2) == 0) ? -1 : 1;
		float ySign = (Random.Range(0, 2) == 0) ? -1 : 1;
		Vector2 p = new Vector2(
			xSign * Random.Range(0.01f, 1f),
			ySign * Random.Range(0.01f, 1f)
		);
		p.Normalize();

		float dist = (float)Random.Range(50, 100);

		_target = this.Player.transform.position;

		Vector2 startPosition = _target + dist * p;
		this.transform.position = new Vector3(startPosition.x, startPosition.y, this.transform.position.z);

		this.enabled = true;
		this.SpriteRenderer.enabled = true;
	}
}
