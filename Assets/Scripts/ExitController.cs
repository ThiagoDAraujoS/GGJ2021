using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class ExitController : MonoBehaviour
{
	public List<ItemDropArea> LinkedDropAreas;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// TODO: Make this check the items we got against the list we're looking for.

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
				msg.Append(display.collectableDefinition.Name);
			}
		}

		msg.Append("}");

		Debug.Log(msg.ToString());
	}
}
