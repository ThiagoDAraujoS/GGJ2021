using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource PlaybackDevice;

    public AudioClip DoorOpenClip;
    public AudioClip DoorCloseClip;
    public static AudioManager instance;

    public AudioClip[] footstepClips;

    public void PlayDoorOpenClip()
    {
        PlaybackDevice.PlayOneShot(DoorOpenClip);
    }

    public void PlayDoorClosedClip()
    {
        PlaybackDevice.PlayOneShot(DoorCloseClip);
    }

    public void OnEnable()
    {
        instance = this;
    }

    private IEnumerator playFootstepsSound()
    {
        while (true)
        {
            PlaybackDevice.PlayOneShot(footstepClips[Random.Range(0, 3)]);
            yield return new WaitForSeconds(0.35f);
        }

    }

    public void beginPlayingFootsteps()
        {
        StartCoroutine(playFootstepsSound());
        }
    public void stopPlayingFootsteps()
    {
        StopAllCoroutines();
    }

}

