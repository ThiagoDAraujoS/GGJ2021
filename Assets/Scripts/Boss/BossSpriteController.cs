using UnityEngine;

public class BossSpriteController : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D collision)
	{
		var playerController = collision.GetComponentInParent<PlayerController>();
		if (playerController != null)
		{
			playerController.enabled = false;
		}
	}
}
