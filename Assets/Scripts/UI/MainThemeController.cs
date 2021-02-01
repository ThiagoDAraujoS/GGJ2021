using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainThemeController : MonoBehaviour
{
	private class Constants
	{
		public const float FadeTime = 0.25f;
		public const float FadeDelay = 1f;
	}

	public Image ClickImage;

	private InputMap _inputMap;

	private void Awake()
	{
		_inputMap = new InputMap();
		_inputMap.Enable();

		StartCoroutine(HandleClickFade());
	}

	private void OnEnable()
	{
		_inputMap.Player.Interact.performed += OnInteract;
	}

	private void OnDisable()
	{
		_inputMap.Player.Interact.performed -= OnInteract;
		_inputMap.Disable();
	}

	private void OnInteract(InputAction.CallbackContext obj)
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	private IEnumerator HandleClickFade()
	{
		while (true)
		{
			this.ClickImage.CrossFadeAlpha(1f, Constants.FadeTime, false);
			yield return new WaitForSeconds(Constants.FadeDelay);
			this.ClickImage.CrossFadeAlpha(0f, Constants.FadeTime, false);
			yield return new WaitForSeconds(1.1f * Constants.FadeTime);
		}
	}
}
