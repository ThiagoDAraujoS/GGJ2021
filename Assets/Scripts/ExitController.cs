using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ExitController : MonoBehaviour
{
	public List<ItemDropArea> LinkedDropAreas;
	public SpriteRenderer EditorDisplayRenderer;
	public ObjectiveController ObjectiveController;

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

		//foreach (ItemDropArea dropArea in this.LinkedDropAreas)
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

		if (collectionCorrect)
			Debug.Log("All items found, you win!");
		else
			Debug.Log("You didn't collect all the items, sorry!");
	}
}
