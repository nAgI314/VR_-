using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectSystem : MonoBehaviour
{
    public static SoundEffectSystem instance1;

    public AudioClip seTouch;
    public AudioClip seNoTouch;

    AudioSource audioSource;
    private void Awake()
    {
        if (instance1 == null)
        {
            instance1 = this;
        }
    }
    private void Start()
    {

        audioSource = GetComponent<AudioSource>();
    }
    public void MakeSoundTouch()
    {
        audioSource.PlayOneShot(seTouch);
    }
    public void MakeSoundNoTouch()
    {
        audioSource.PlayOneShot(seNoTouch);
    }
}
