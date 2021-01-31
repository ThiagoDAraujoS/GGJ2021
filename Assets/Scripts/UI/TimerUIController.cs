using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUIController : MonoBehaviour
{
	public Image Image;
	public List<Sprite> Sprites;
	public ObjectiveController ObjectiveController;

	void Update()
    {
		int frameNum = Mathf.RoundToInt(this.ObjectiveController.TimePercent * (this.Sprites.Count - 1));
		this.Image.sprite = this.Sprites[frameNum];
	}
}
