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

	private List<Collectible> _collectedItems = new List<Collectible>();
	private Collectible _touchedCollectable = null;
	private List<ItemDropArea> _nearbyDropAreas = new List<ItemDropArea>();

	private int _maxItems = 3;

	public void UpdateForMove(Vector2 moveDir)
	{
		isUp = moveDir.y > 0 && moveDir.x == 0;
	}

	public void HandleFirePressed()
	{
		if (_touchedCollectable != null)
		{
			this.PickUp(_touchedCollectable);
		}
		else if (_nearbyDropAreas.Count > 0)
		{
			this.Drop();
		}
	}

	public void PickUp(Collectible item)
	{
		if (_collectedItems.Count < _maxItems)
		{
			foreach (var dropTarget in _nearbyDropAreas)
			{
				if (dropTarget.item == item)
				{
					dropTarget.item = null;
					break;
				}
			}

			_collectedItems.Add(item);

			item.collider.enabled = false;
			item.canCollide = false;
			item.transform.SetParent(anchorPoint);

			item.SetParent(this);
			item.order = _collectedItems.Count;

			Arrange();

			_touchedCollectable = null;
		}
	}

	public void Drop()
	{
		if (_collectedItems.Count > 0 )
		{
			// Find the cloest drop target to the characer. Is it inefficient? Sure! Does it work? Yes!
			float closestDist = float.MaxValue;
			float dist = 0f;
			ItemDropArea dropTarget = null;
			foreach (ItemDropArea potentialTarget in _nearbyDropAreas)
			{
				if (potentialTarget.item == null && (dist = Vector2.Distance(this.transform.position, potentialTarget.transform.position)) < closestDist)
				{
					closestDist = dist;
					dropTarget = potentialTarget;
				}
			}

			// If we have a place to drop, go ahead and do it.
			if (dropTarget != null)
			{
				var item = _collectedItems[_collectedItems.Count - 1];
				item.transform.SetParent(null);

				item.SetParent(null);
				item.canCollide = true;
				item.order = 0;
				item.targetPosition = dropTarget.transform.position;

				dropTarget.item = item;

				_collectedItems.Remove(item);

				Arrange();
			}
		}
	}

	private void Arrange()
	{
		// This is a hack... I can't figure out a better way to do it, so I went with this lol.

		Vector2 itemSize = new Vector2(1f, 1f); // This should come from item.collider but it's not working righ tnow.

		int maxPerRow = 2;
		int numRows = (int)Mathf.Ceil(_collectedItems.Count / (float)maxPerRow);

		// Add each item
		for (int i = 0; i < _collectedItems.Count; i++)
		{
			var item = _collectedItems[i];

			int row = i / maxPerRow;
			int rowIndex = i % maxPerRow;

			bool singleItemRow = (i == _collectedItems.Count - 1) && (i % maxPerRow == 0);
			float rowWidth = (singleItemRow) ? itemSize.x : itemSize.x * 2;

			Vector2 offset = new Vector2(
				(-rowWidth / 2f) + (itemSize.x / 2) + (rowIndex * itemSize.x),
				(float)row * (itemSize.y / 2f)
			);

			item.targetPosition = offset;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var dropArea = collision.gameObject.GetComponentInParent<ItemDropArea>();
		if (dropArea != null && !_nearbyDropAreas.Contains(dropArea))
			_nearbyDropAreas.Add(dropArea);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		var dropArea = collision.gameObject.GetComponentInParent<ItemDropArea>();
		if (dropArea != null && _nearbyDropAreas.Contains(dropArea))
			_nearbyDropAreas.Remove(dropArea);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		// TODO: Maybe change this to a separate collider trigger so we can pick up nearby things, not just things we've smashed our face into?
		// Maybe a circle collider?
		var collectable = collision.gameObject.GetComponentInParent<Collectible>();
		if (collectable != null)
			_touchedCollectable = collectable;
	}

	private void OnCollisionExit2D(Collision2D collision)
	{
		_touchedCollectable = null;
	}
}
