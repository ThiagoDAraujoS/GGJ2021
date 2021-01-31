using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Collectible Definition", menuName = "Collectible Definition")]
public class CollectibleDefinition : ScriptableObject
{
	public string Name = "";
	public List<string> Descriptions;
	public Sprite Sprite;

	public override bool Equals(object other)
	{
		bool equals = false;

		if (other != null && other is CollectibleDefinition)
			equals = this.Name == ((CollectibleDefinition)other).Name;

		return equals;
	}

	public override int GetHashCode()
	{
		return this.Name.GetHashCode();
	}

	public static bool operator ==(CollectibleDefinition lhs, CollectibleDefinition rhs)
	{
		return lhs.Equals(rhs);
	}

	public static bool operator !=(CollectibleDefinition lhs, CollectibleDefinition rhs)
	{
		return !(lhs == rhs);
	}
}
