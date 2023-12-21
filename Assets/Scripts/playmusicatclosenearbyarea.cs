using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip songClip;
    private AudioSource audioSource;
    public float detectionRange = 5f;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = songClip;
        audioSource.loop = true; // Set to true if you want the song to loop
        audioSource.volume = 1f;
    }

    void Update()
    {
        // Check if the player is within the detection range
        if (IsPlayerClose())
        {
            // If the song is not already playing, start playing it
            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // If the player is not in range, stop the song
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    bool IsPlayerClose()
    {
        // You can replace this with a more accurate method of checking player proximity, like using a collider
        // For simplicity, this example assumes the player is tagged as "Player"
        GameObject player = GameObject.FindWithTag("Player");

        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);
            return distance <= detectionRange;
        }

        return false;
    }
}
