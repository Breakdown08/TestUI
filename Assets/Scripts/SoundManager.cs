using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance {get; private set;}
    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip cursorHoverSound;
    [SerializeField] private AudioClip cursorClickSound;

    private void Awake()
    {
        Instance = this;
    }

    private static void PlaySound(AudioClip audioClip)
    {
        Instance.audioSource.PlayOneShot(audioClip);
    }

    public static void PlayCursorHoverSound()
    {
        PlaySound(Instance.cursorHoverSound);
    } 

    public static void PlayCursorClickSound()
    {
        PlaySound(Instance.cursorClickSound);
    }
}
