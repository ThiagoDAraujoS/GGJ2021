using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ExitController : MonoBehaviour
{
	public List<ItemDropArea> LinkedDropAreas;
	public SpriteRenderer EditorDisplayRenderer;
	public ObjectiveController ObjectiveController;
	public GameplayStateManager GameplayStateManager;

	private void Awake()
	{
		if (EditorDisplayRenderer != null && !GameManager.DebugVariables.ExitAreaRuntimeVisualsEnabled)
			EditorDisplayRenderer.enabled = false;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		List<CollectibleDefinition> collectedItems = new List<CollectibleDefinition>();

		StringBuilder msg = new StringBuilder();
		msg.Append("Collected Items: {");
		bool wroteFirst = false;

		for (int i = 0; i < this.LinkedDropAreas.Count; i++)
		{
			var dropArea = this.LinkedDropAreas[i];
			if (dropArea != null && dropArea.item != null)
			{
				if (wroteFirst == true)
					msg.Append(", ");
				wroteFirst = true;

				CollectibleDisplay display = dropArea.item.GetComponent<CollectibleDisplay>();
				collectedItems.Add(display.collectableDefinition);

				msg.Append(display.collectableDefinition.Name);
			}
		}

		msg.Append("}");

		Debug.Log(msg.ToString());

		bool collectionCorrect = this.ObjectiveController.CheckCollection(collectedItems);
		this.GameplayStateManager.EndGame((collectionCorrect) ? GameOverState.Success : GameOverState.IncorrectObjectives);
	}
}
