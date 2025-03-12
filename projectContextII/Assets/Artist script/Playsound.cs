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

/*____        _     _                                 _    _               
 |  _ \      | |   | |                               | |  | |              
 | |_) | ___ | |__ | |__  _   _  __      ____ _ ___  | |__| | ___ _ __ ___ 
 |  _ < / _ \| '_ \| '_ \| | | | \ \ /\ / / _` / __| |  __  |/ _ \ '__/ _ \
 | |_) | (_) | |_) | |_) | |_| |  \ V  V / (_| \__ \ | |  | |  __/ | |  __/
 |____/ \___/|_.__/|_.__/ \__, |   \_/\_/ \__,_|___/ |_|  |_|\___|_|  \___|
                           __/ |                                           
                          |___/                                            */