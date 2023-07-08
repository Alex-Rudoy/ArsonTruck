using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null)
        {
            throw new System.Exception("SoundManager already exists!");
        }
        Instance = this;
    }

    public void PlaySound(AudioClip[] audioClips, Vector3 position, float volume = 1f)
    {
        AudioClip audioClip = audioClips[Random.Range(0, audioClips.Length)];
        AudioSource.PlayClipAtPoint(audioClip, position, volume);
    }
}
