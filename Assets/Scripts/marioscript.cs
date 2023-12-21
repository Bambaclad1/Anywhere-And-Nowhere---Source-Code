using UnityEngine;
using UnityEngine.UI;

public class marioscript : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float detectionRadius = 5f;
    public float chaseRadius = 5f;
    public AudioClip soundClip;
    public Image gameOverImage; // Reference to the UI Image for game over screen

    private AudioSource audioSource;
    private bool isPlayerClose = false;

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = soundClip;
        audioSource.loop = true;
        audioSource.volume = 0f;
        audioSource.Play();
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        if (distanceToPlayer < detectionRadius)
        {
            isPlayerClose = true;
            audioSource.volume = Mathf.Lerp(audioSource.volume, 1f, Time.deltaTime * 2f);
        }
        else
        {
            isPlayerClose = false;
            audioSource.volume = Mathf.Lerp(audioSource.volume, 0f, Time.deltaTime * 2f);
        }

        // Check if the player is within the chase radius before moving towards them
        if (isPlayerClose && distanceToPlayer <= chaseRadius)
        {
            transform.LookAt(player.transform.position);
            transform.Translate(0, 0, speed * Time.deltaTime);
            transform.Rotate(0, 0, 0);
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Handle player hit
            HandlePlayerHit();
        }
        else
        {

        }
    }

    void HandlePlayerHit()
    {
        // Show the game over image
        if (gameOverImage != null)
        {
            gameOverImage.gameObject.SetActive(true);
        }

        // Add any additional logic for handling the player hit

        // Close the game (you may want to replace this with appropriate code for your platform)
        CloseGame();
    }

    void CloseGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }
}
