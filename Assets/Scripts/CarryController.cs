using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CarryController : MonoBehaviour
{
	public InputMap map;

	public Collider2D playerBounds;
	public Transform anchorPoint;

	[HideInInspector]
	public bool isUp = false;

	private List<Collectable> _collectedItems = new List<Collectable>();
	private Collectable _touchedCollectable = null;

	public void OnPickObj(ContextCallback ctx) {

	}

	public void UpdateForMove(Vector2 moveDir)
	{
		isUp = moveDir.y > 0 && moveDir.x == 0;
	}

	public void HandleFirePressed()
	{
		if (_touchedCollectable != null)
		{
			this.PickUp(_touchedCollectable);
			_touchedCollectable = null;
		}
		else
		{
			this.Drop();
		}
	}

	public void PickUp(Collectable item)
	{
		item.collider.isTrigger = true;
		item.gameObject.transform.SetParent(anchorPoint);
		item.SetParent(this);
		_collectedItems.Add(item);

		Arrange();
	}

	public void Drop()
	{
		if (_collectedItems.Count > 0)
		{
			var item = _collectedItems[_collectedItems.Count - 1];
			item.collider.isTrigger = false;
			item.transform.SetParent(null);
			item.SetParent(null);
			_collectedItems.Remove(item);
		}
	}

	private void Arrange()
	{
		if (_collectedItems.Count != 0)
		{
			// TODO: Do some actual arranging based on the number of items.
			_collectedItems[0].transform.localPosition = Vector2.zero; // + new Vector2(0, 0.5f) ;
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// TODO: Maybe change this to a separate collider trigger so we can pick up nearby things, not just things we've smashed our face into?
		// Maybe a circle collider?
		var collectable = collision.gameObject.GetComponentInParent<Collectable>();
		if (collectable != null)
			_touchedCollectable = collectable;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		_touchedCollectable = null;
	}
}
