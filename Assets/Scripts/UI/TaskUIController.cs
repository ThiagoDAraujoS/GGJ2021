using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TaskUIController : MonoBehaviour
{
	private class Constants
	{
		public const float FadeTime = 0.25f;
	}

	public Canvas UICanvas;
	public ObjectiveController ObjectiveController;
	public Text TextTemplate;

	private void Start()
	{
		this.TextTemplate.enabled = false;

		// Generate the task text.
		Vector2 startPosition = this.TextTemplate.rectTransform.rect.position;
		Vector2 startSize = this.TextTemplate.rectTransform.rect.size;
		float height = this.TextTemplate.rectTransform.rect.height;
		float padding = Mathf.Round(height * 0.1f);

		for (int i = 0; i < this.ObjectiveController.Objectives.Count; i++)
		{
			var objective = this.ObjectiveController.Objectives[i];
			var newText = Instantiate<Text>(this.TextTemplate, this.transform);

			newText.rectTransform.anchoredPosition -= new Vector2(0, i * (height + padding));

			newText.text = objective.Description;
			newText.enabled = true;
		}

		// Hide the UI.
		HideUI(0);
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
