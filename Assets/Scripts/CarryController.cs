using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryController : MonoBehaviour
{
	public BoxCollider2D playerBounds;
	public bool isUp = false;

	private List<Collectable> _collectedItems = new List<Collectable>();

	public void UpdateForMove(Vector2 moveDir)
	{
		isUp = moveDir.y > 0 && moveDir.x == 0;
	}

	public void PickUp(Collectable item)
	{
		item.collider.isTrigger = true;
		item.gameObject.transform.SetParent(this.transform);
		item.parent = this;
		_collectedItems.Add(item);

		Arrange();
	}

	public void Drop()
	{
		if (_collectedItems.Count > 0)
		{
			var item = _collectedItems[_collectedItems.Count - 1];
			item.collider.isTrigger = false;
			_collectedItems.Remove(item);
			item.transform.SetParent(null);
			item.parent = null;
		}
	}

	private void Arrange()
	{
		if (_collectedItems.Count != 0)
		{
			_collectedItems[0].transform.localPosition = Vector2.zero + new Vector2(0, 0.5f) ;
		}
	}
}
