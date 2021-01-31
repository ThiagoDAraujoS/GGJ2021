﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
	private class Constants
	{
		public const float FadeTime = 0.25f;
	}

	public Canvas UICanvas;
	public ObjectiveController ObjectiveController;
	public List<Image> RequiredItemImages;
	public List<Image> CollectedItemImages;

	private void Start()
	{
		// Hide the UI.
		HideUI(0);
	}

	public void UpdateForGameOver(GameOverState gameOverState, List<CollectibleDefinition> collectedItems)
	{
		// Sort the list of items we collected.
		if (collectedItems != null)
			collectedItems.Sort((lhs, rhs) => lhs.Name.CompareTo(rhs.Name));

		// Sort the list of items we need.
		List<CollectibleDefinition> objectives = new List<CollectibleDefinition>();
		foreach (var objective in this.ObjectiveController.Objectives)
			objectives.Add(objective.Target);
		objectives.Sort((lhs, rhs) => lhs.Name.CompareTo(rhs.Name));

		// Update the images for what we need.
		for (int i = 0; i < this.RequiredItemImages.Count; i++)
		{
			this.RequiredItemImages[i].enabled = false;

			if (i < objectives.Count)
			{
				this.RequiredItemImages[i].sprite = objectives[i].Sprite;
				this.RequiredItemImages[i].enabled = true;
			}
		}

		// Updat ethe images for what we got.
		for (int i = 0; i < this.CollectedItemImages.Count; i++)
		{
			this.CollectedItemImages[i].enabled = false;

			if (collectedItems != null && i < collectedItems.Count)
			{
				this.CollectedItemImages[i].sprite = collectedItems[i].Sprite;
				this.CollectedItemImages[i].enabled = true;
			}
		}
	}

	public void ShowUI()
	{
		this.StopAllCoroutines();
		this.StartCoroutine(HandleFadeIn(Constants.FadeTime));
	}

	public void HideUI(float fadeTime = Constants.FadeTime)
	{
		this.StopAllCoroutines();
		this.StartCoroutine(HandleFadeOut(fadeTime));
	}

	private IEnumerator HandleFadeIn(float fadeTime)
	{
		this.UICanvas.enabled = true;

		foreach (var component in this.UICanvas.GetComponentsInChildren<Image>())
		{
			component.CrossFadeAlpha(1f, Constants.FadeTime, false);
		}

		foreach (var component in this.UICanvas.GetComponentsInChildren<Text>())
		{
			component.CrossFadeAlpha(1f, fadeTime, false);
		}

		yield return new WaitForSeconds(fadeTime);
	}

	private IEnumerator HandleFadeOut(float fadeTime)
	{
		foreach (var component in this.UICanvas.GetComponentsInChildren<Text>())
		{
			//component.enabled = false;
			component.CrossFadeAlpha(0f, fadeTime, false);
		}

		foreach (var component in this.UICanvas.GetComponentsInChildren<Image>())
		{
			component.CrossFadeAlpha(0f, fadeTime, false);
		}

		yield return new WaitForSeconds(fadeTime);
		this.UICanvas.enabled = false;
	}
}
