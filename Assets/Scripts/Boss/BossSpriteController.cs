using UnityEngine;

public class BossSpriteController : MonoBehaviour
{
	public GameplayStateManager GameplayStateManager;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		var playerController = collision.GetComponentInParent<PlayerController>();
		if (playerController != null)
		{
			this.GameplayStateManager.EndGame(GameOverState.Death);
		}
	}
}
