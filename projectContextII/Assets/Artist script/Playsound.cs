using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class TriggerSoundPlayer : MonoBehaviour
{
    [Header("Audio Settings")]
    public AudioClip soundClip; // Assign in Inspector
    public bool playOnce = true; // Play only once?

    private AudioSource audioSource;
    private bool hasPlayed = false;

    void Start()
    {
        // Add an AudioSource component dynamically
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 1f; // 3D sound
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Ensure the player has the correct tag
        {
            if (!playOnce || (playOnce && !hasPlayed))
            {
                PlaySound();
                hasPlayed = true;
            }
        }
    }

    void PlaySound()
    {
        if (soundClip != null)
        {
            audioSource.clip = soundClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("No sound assigned to TriggerSoundPlayer on " + gameObject.name);
        }
    }
}
