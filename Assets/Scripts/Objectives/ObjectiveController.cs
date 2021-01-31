using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectiveController : MonoBehaviour
{
	private class Constants
	{
		public const float MaxTime = 3 * 60; // Seconds
	}

	public bool GenerateRandom = true;
	public int ObjectiveCount = 5;
	public List<CollectibleDefinition> AvailableCollectibles;
	public List<CollectibleDefinition> DebugObjectives;

	[HideInInspector]
	public List<Objective> Objectives { get { return _objectives; } }
	private List<Objective> _objectives = new List<Objective>();

	public float TimeRemaining { get; private set; }
	public float TimePercent { get { return this.TimeRemaining / Constants.MaxTime; } }

	void Awake()
	{
		this.TimeRemaining = Constants.MaxTime;

		if (this.GenerateRandom || this.DebugObjectives.Count == 0)
			GenerateObjectives();
		else
			GenerateDebug();

		Debug.Log("Target Items:");
		foreach (var objective in _objectives)
			Debug.Log($"{objective.Target.Name} - {objective.Description}");
	}

	private void Update()
	{
		this.TimeRemaining = Mathf.Max(0f, this.TimeRemaining - Time.deltaTime);
	}

	public bool CheckCollection(List<CollectibleDefinition> collectedItems)
	{
		bool collectionCorrect = true;

		// Go through all our objectives.
		foreach (var objectiveItem in _objectives)
		{
			// Reset to not found.
			objectiveItem.Found = false;

			// Search for the objective in the collected items.
			foreach (var collectedItem in collectedItems)
			{
				if (collectedItem == objectiveItem.Target)
				{
					objectiveItem.Found = true;
					break;
				}
			}

			// One object wasn't found, indicate failure. Keep searching the list to update the status though.
			if (objectiveItem.Found == false)
				collectionCorrect = false;
		}

		return collectionCorrect;
	}
	
	private void GenerateObjectives()
	{
		Debug.Log("[ObjectiveController] Generating random objectives.");

		List<int> availableIndexes = new List<int>();
		for (int i = 0; i < this.AvailableCollectibles.Count; i++)
			availableIndexes.Add(i);

		for (int i = 0; i < this.ObjectiveCount; i++)
		{
			int index = Random.Range(0, availableIndexes.Count);
			int selectionIndex = availableIndexes[index];
			availableIndexes.RemoveAt(index);

			var def = this.AvailableCollectibles[selectionIndex];
			int descriptionIndex = Random.Range(0, def.Descriptions.Count);

			_objectives.Add(new Objective(def, def.Descriptions[descriptionIndex]));
		}
	}

	private void GenerateDebug()
	{
		Debug.Log("[ObjectiveController] Using debug objectives.");
		foreach (var def in this.DebugObjectives)
		{
			// TODO: Maybe figure out how to hard code the description it picks? No idea how to do this though.
			int descriptionIndex = Random.Range(0, def.Descriptions.Count);
			_objectives.Add(new Objective(def, def.Descriptions[descriptionIndex]));
		}
	}
}
