using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerSoundPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioSource audioSource;


    void OnTriggerEnter(Collider other)
    {
        PlaySound();
    }

    void PlaySound()
    {
        audioSource.Play();
    }
}