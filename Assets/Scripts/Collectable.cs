using UnityEngine;

public class Collectable : MonoBehaviour
{
	public new BoxCollider2D collider;
	public BasicCharacterController characterController;
	public CarryController parent = null;

	private void Update()
	{
		if (parent != null)
		{
			float offs = (parent.isUp) ? 0.1f : -0.1f;
			transform.position = new Vector3(transform.position.x, transform.position.y, parent.playerBounds.bounds.max.y + offs);
		}
		else
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, collider.bounds.max.y);
		}
	}
}
