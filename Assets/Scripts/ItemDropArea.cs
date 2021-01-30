using UnityEngine;

public class ItemDropArea : MonoBehaviour
{
	public SpriteRenderer editorDisplayRenderer;

	public Collectible item = null;

	private void Awake()
	{
		if (editorDisplayRenderer != null && !GameManager.DebugVariables.DropAreaEditorVisualsEnabled)
			editorDisplayRenderer.enabled = false;
	}
}
