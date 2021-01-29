using UnityEngine;

public class CollectibleDisplay : MonoBehaviour
{
	public CollectibleDefinition collectableDefinition;

	public SpriteRenderer spriteRenderer;

    void Awake()
    {
		spriteRenderer.sprite = collectableDefinition.Sprite;
    }
}
