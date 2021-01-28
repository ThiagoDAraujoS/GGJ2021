using UnityEngine;

public class Collectable : MonoBehaviour
{
	public BoxCollider2D col2d;

    void Awake()
    {
    }

	private void Update()
	{
		BasicCharacterController character = this.transform.parent?.gameObject.GetComponent<BasicCharacterController>();
		if (character != null)
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, character.col2d.bounds.max.y - 0.1f);
		}
		else
		{
			transform.position = new Vector3(transform.position.x, transform.position.y, col2d.bounds.max.y);
		}
	}
}
