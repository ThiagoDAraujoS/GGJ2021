using UnityEngine;

[CreateAssetMenu(fileName = "New Collectable Definition", menuName = "Collectable Definition")]
public class CollectibleDefinition : ScriptableObject
{
	public string Name = "";
	public Sprite Sprite;
}
