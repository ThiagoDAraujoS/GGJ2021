using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadeInUIController : MonoBehaviour
{
	private class Constants
	{
		public const float FadeTime = 0.5f;
	}

	public Image MaskImage;

    void Start()
    {
		StartCoroutine(HandleFadeOut());
    }

	private IEnumerator HandleFadeOut()
	{
		this.MaskImage.CrossFadeAlpha(0f, Constants.FadeTime, false);
		yield return new WaitForSeconds(Constants.FadeTime);
		DestroyImmediate(this.gameObject);
	}
}
