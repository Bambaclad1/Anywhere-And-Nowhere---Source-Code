using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pewpew : MonoBehaviour
{
    public Animator Anim1;
    public AudioClip soundClip;
    private AudioSource audioSource;
    public GameObject raycastVisualPrefab; // The prefab for the visual representation of the ray
    public float raycastVisualScale = 1.0f; // Adjustable scale for the visual representation
    public Vector3 raycastVisualRotation = Vector3.zero; // Adjustable rotation for the visual representation

    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        Anim1 = GetComponent<Animator>();
        audioSource.volume = 1f;
        audioSource.clip = soundClip;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Anim1.SetTrigger("Attack");
            audioSource.Play();

            FireRaycast();
        }
    }

    void FireRaycast()
    {
        // Raycasting logic
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Debug.Log("Hit object: " + hit.collider.gameObject.name);

            // Check if the ray hits an enemy
            NextBot enemy = hit.collider.GetComponent<NextBot>();
            if (enemy != null)
            {
                // If it hits an enemy, destroy the enemy
                Destroy(enemy.gameObject);
            }

            // Instantiate the visual representation at the hit point with adjustable scale and rotation
            GameObject raycastVisual = Instantiate(raycastVisualPrefab, hit.point, Quaternion.identity);
            raycastVisual.transform.localScale = new Vector3(raycastVisualScale, raycastVisualScale, raycastVisualScale);

            // Rotate the visual representation to face the player's forward direction
            raycastVisual.transform.forward = transform.forward;

            // Move the visual representation towards the hit point
            StartCoroutine(MoveRaycastVisual(raycastVisual, hit.point));
        }
    }

    IEnumerator MoveRaycastVisual(GameObject raycastVisual, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float moveDuration = 0.5f; // Adjust the duration as needed

        while (elapsedTime < moveDuration)
        {
            raycastVisual.transform.position = Vector3.Lerp(transform.position, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the visual representation is exactly at the hit point
        raycastVisual.transform.position = targetPosition;

        // Destroy the visual representation after hitting the object
        Destroy(raycastVisual, 1.0f); // Adjust the delay before destruction as needed
    }
}
