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
        PlayClipAt(audioClip, position, volume);
    }

    void PlayClipAt(AudioClip clip, Vector3 pos, float volume)
    {
        GameObject tempGameObject = new GameObject("TempAudio");
        tempGameObject.transform.position = pos;
        AudioSource audioSource = tempGameObject.AddComponent<AudioSource>();
        audioSource.volume = volume;
        audioSource.spatialBlend = 0.5f;
        audioSource.clip = clip;
        audioSource.Play();
        Destroy(tempGameObject, clip.length);
    }
}
