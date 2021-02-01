using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameplayStateManager : MonoBehaviour
{
	public PlayerController PlayerController;
	public TimerUIController TimerUIController;
	public BossController BossController;

	public TaskUIController TaskUIController;
	public GameOverUIController GameOverUIController;

    //events
    public UnityEvent GameOverWin;

	[HideInInspector]
	public GameOverState Result { get; private set; } = GameOverState.Death;

	public void EndGame(GameOverState gameOverState, List<CollectibleDefinition> collectedItems)
	{
		this.Result = gameOverState;
		Debug.Log("Game has ended: " + this.Result);

		// Disable controllers we don't need anymore.
		this.PlayerController.Disable();
		this.TimerUIController.enabled = false;
		this.BossController.Disable();

		// Update Game Over UI
		this.GameOverUIController.UpdateForGameOver(gameOverState, collectedItems);

		// Show UI
		this.TaskUIController.ShowUI();
		this.GameOverUIController.ShowUI();

        if (gameOverState == GameOverState.Success)
        {
            GameOverWin?.Invoke();
        }
	}
}
