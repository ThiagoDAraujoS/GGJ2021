using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
	private class Constants
	{
		public const float MusicFadeTime = 0.25f;
	}

    public AudioSource PlaybackDevice;
	public AudioSource MusicSource;

    public AudioClip DoorOpenClip;
    public AudioClip DoorCloseClip;
    public static AudioManager Instance;

    public AudioClip[] FootstepClips;

    public void PlayDoorOpenClip()
    {
        PlaybackDevice.PlayOneShot(DoorOpenClip);
    }

	private List<Coroutine> _activeFootsteps = new List<Coroutine>();

    public void PlayDoorClosedClip()
    {
        PlaybackDevice.PlayOneShot(DoorCloseClip);
    }

    public void OnEnable()
    {
        Instance = this;
    }

    private IEnumerator PlayFootstepsSound()
    {
		while (true)
		{
			PlaybackDevice.PlayOneShot(FootstepClips[Random.Range(0, 3)]);
			yield return new WaitForSeconds(0.35f);
		}
    }

	private IEnumerator TransitionMusicToEndGame()
	{
		float elapsed = Time.deltaTime;

		while (elapsed < Constants.MusicFadeTime)
		{
			this.MusicSource.volume = Mathf.Lerp(this.MusicSource.volume, 0f, elapsed / Constants.MusicFadeTime);
			yield return null;
		}

		this.MusicSource.Stop();
	}

    public void BeginPlayingFootsteps()
    {
		_activeFootsteps.Add(StartCoroutine(PlayFootstepsSound()));
    }

    public void StopPlayingFootsteps()
    {
		foreach (var coroutine in _activeFootsteps)
			StopCoroutine(coroutine);
    }

	public void StopAllSounds()
	{
		StopPlayingFootsteps();

		//StartCoroutine(TransitionMusicToEndGame());
	}

}

