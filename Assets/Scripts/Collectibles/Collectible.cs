using UnityEngine;

public class Collectible : MonoBehaviour
{
	public new Collider2D collider;

	[HideInInspector]
	public int order = 0;
	[HideInInspector]
	public Vector2? targetPosition = null;

	private CarryController _parent = null;

	public void SetParent(CarryController carryController)
	{
		_parent = carryController;
	}

	private void Start()
	{
	}

	private void FixedUpdate()
	{
		if (targetPosition != null)
		{
			float speed = 10f;
			float step = speed * Time.deltaTime;
			transform.localPosition = Vector2.MoveTowards(transform.localPosition, targetPosition.Value, step);
			if (transform.localPosition == targetPosition)
			{
				targetPosition = null;
				if (this.collider.enabled == false)
					this.collider.enabled = true;
			}
		}
	}

	private void Update()
	{
		if (_parent != null)
		{
			float offs = (_parent.isUp) ? 0.1f : -0.1f;
			transform.position = new Vector3(transform.position.x, transform.position.y, _parent.playerBounds.bounds.max.y + offs * order);
		}
		else
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, collider.bounds.max.y);
		}
	}
}
