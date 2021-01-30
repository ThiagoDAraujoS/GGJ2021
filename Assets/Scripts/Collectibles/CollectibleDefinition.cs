using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible Definition", menuName = "Collectible Definition")]
public class CollectibleDefinition : ScriptableObject
{
	public string Name = "";
	public List<string> Descriptions;
	public Sprite Sprite;
}
