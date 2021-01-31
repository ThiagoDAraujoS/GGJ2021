using UnityEngine;

public class GameplayStateManager : MonoBehaviour
{
	public PlayerController PlayerController;
	public TaskUIController TaskUIController;
	public TimerUIController TimerUIController;
	public BossController BossController;

	[HideInInspector]
	public GameOverState Result { get; private set; } = GameOverState.Death;

	public void EndGame(GameOverState gameOverState)
	{
		this.Result = gameOverState;
		Debug.Log("Game has ended: " + this.Result);

		// Disable controllers we don't need anymore.
		this.PlayerController.Disable();
		this.TimerUIController.enabled = false;
		this.BossController.enabled = false;

		// Show UI
		this.TaskUIController.ShowUI();
	}
}
