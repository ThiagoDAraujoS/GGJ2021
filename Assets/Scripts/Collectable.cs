using UnityEngine;

public class Collectable : MonoBehaviour
{
	public new Collider2D collider;

	[HideInInspector]
	public int order = 0;

	private CarryController _parent = null;

	public void SetParent(CarryController carryController)
	{
		_parent = carryController;
	}

	private void Start()
	{
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
