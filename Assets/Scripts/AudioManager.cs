using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource PlaybackDevice;

    public AudioClip DoorOpenClip;
    public AudioClip DoorCloseClip;
    public static AudioManager Instance;

    public AudioClip[] FootstepClips;

    public void PlayDoorOpenClip()
    {
        PlaybackDevice.PlayOneShot(DoorOpenClip);
    }

	private bool _isWalking = false;

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
		if (!_isWalking)
		{
			_isWalking = true;
			while (_isWalking)
			{
				PlaybackDevice.PlayOneShot(FootstepClips[Random.Range(0, 3)]);
				yield return new WaitForSeconds(0.35f);
			}
		}
    }

    public void BeginPlayingFootsteps()
    {
		StartCoroutine(PlayFootstepsSound());
    }

    public void StopPlayingFootsteps()
    {
		//StopAllCoroutines();
		_isWalking = false;
    }

	public void StopAllSounds()
	{
		StopPlayingFootsteps();
	}

}

