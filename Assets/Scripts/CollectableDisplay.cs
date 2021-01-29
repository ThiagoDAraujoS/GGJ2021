using UnityEngine;

public class CollectableDisplay : MonoBehaviour
{
	public CollectableDefinition collectableDefinition;

	public SpriteRenderer spriteRenderer;

    void Awake()
    {
		spriteRenderer.sprite = collectableDefinition.Sprite;
    }
}
