using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
	private class Constants
	{
		public const float FadeTime = 0.25f;
		public const float FadeDelay = 1f;
	}

	public string ResultText_Death = "The dungeon boss caught you!";
	public string ResultText_Win = "Great job! You collected all the items on the list!";
	public string ResultText_Loss = "Too bad! You missed some of the items on your list.";

	public Canvas UICanvas;
	public ObjectiveController ObjectiveController;
	public List<Image> RequiredItemImages;
	public List<Image> CollectedItemImages;
	public Image BackgroundImage;
	public Text ItemsNeededText;
	public Text ItemsCollectedText;

	public Text ResultText;
	public Image RestartPromptImage;

	private bool _canRestart = false;
	private InputMap _inputMap;

	private void Awake()
	{
		_inputMap = new InputMap();
	}

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
				this.RequiredItemImages[i].CrossFadeAlpha(0f, 0f, false);
			}
		}

		// Update the images for what we got.
		for (int i = 0; i < this.CollectedItemImages.Count; i++)
		{
			this.CollectedItemImages[i].enabled = false;

			if (collectedItems != null && i < collectedItems.Count)
			{
				this.CollectedItemImages[i].sprite = collectedItems[i].Sprite;
				this.CollectedItemImages[i].enabled = true;
				this.CollectedItemImages[i].CrossFadeAlpha(0f, 0f, false);
			}
		}

		string resultText = this.ResultText_Win;
		switch (gameOverState)
		{
			case GameOverState.Death:
				resultText = this.ResultText_Death;
				break;
			case GameOverState.IncorrectObjectives:
				resultText = this.ResultText_Loss;
				break;
		}

		this.ResultText.text = resultText;
	}

	public void ShowUI()
	{
		this.enabled = true;
		this.StopAllCoroutines();
		this.StartCoroutine(HandleFadeIn(Constants.FadeTime));
	}

	public void HideUI(float fadeTime = Constants.FadeTime)
	{
		this.enabled = false;
		this.StopAllCoroutines();
		this.StartCoroutine(HandleFadeOut(fadeTime));
	}

	private void OnEnable()
	{
		_inputMap.Player.Interact.performed += OnInteractPressed;
	}

	private void OnInteractPressed(InputAction.CallbackContext obj)
	{
		if (_canRestart)
		{
			_canRestart = false;
			StopAllCoroutines();
			SceneManager.LoadScene(0); // TODO: Transition this back to the main screen when we have one.
		}
	}

	private IEnumerator HandleFadeIn(float fadeTime)
	{
		this.UICanvas.enabled = true;

		// Fade in BG.
		this.BackgroundImage.CrossFadeAlpha(1f, fadeTime, false);
		yield return new WaitForSeconds(Constants.FadeDelay);

		// Fade in result text.
		this.ResultText.CrossFadeAlpha(1f, fadeTime, false);
		yield return new WaitForSeconds(Constants.FadeDelay);

		// Fade in items needed text and images.
		this.ItemsNeededText.CrossFadeAlpha(1f, fadeTime, false);
		yield return new WaitForSeconds(Constants.FadeDelay);

		foreach (var requiredItemImages in this.RequiredItemImages)
		{
			requiredItemImages.CrossFadeAlpha(1f, fadeTime, false);
			yield return new WaitForSeconds(Constants.FadeDelay / this.RequiredItemImages.Count);
		}

		// Fade in items collected text and images.
		this.ItemsCollectedText.CrossFadeAlpha(1f, fadeTime, false);
		yield return new WaitForSeconds(Constants.FadeDelay);

		foreach (var collectedItemImages in this.CollectedItemImages)
		{
			collectedItemImages.CrossFadeAlpha(1f, fadeTime, false);
			yield return new WaitForSeconds(Constants.FadeDelay / this.CollectedItemImages.Count);
		}

		_inputMap.Enable();
		_canRestart = true;

		while (true)
		{
			this.RestartPromptImage.CrossFadeAlpha(1f, fadeTime, false);
			yield return new WaitForSeconds(Constants.FadeDelay);
			this.RestartPromptImage.CrossFadeAlpha(0f, fadeTime, false);
			yield return new WaitForSeconds(1.1f * fadeTime);
		}
	}

	private IEnumerator HandleFadeOut(float fadeTime)
	{
		foreach (var component in this.UICanvas.GetComponentsInChildren<Text>())
		{
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
