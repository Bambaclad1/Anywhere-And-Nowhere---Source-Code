using System.Collections;
using UnityEngine;

public class PlayerTeleportation : MonoBehaviour
{
    public Transform[] teleportLocations; // Add the teleport locations in the Unity Editor
    public float teleportInterval = 20f;

    private int currentTeleportIndex = 0;

    void Start()
    {
        // Start the teleportation coroutine
        StartCoroutine(TeleportRoutine());
    }

    IEnumerator TeleportRoutine()
    {
        while (true)
        {
            // Wait for the specified interval before teleporting
            yield return new WaitForSeconds(teleportInterval);

            // Teleport the player to the next location in the sequence
            TeleportToNextLocation();
        }
    }

    void TeleportToNextLocation()
    {
        // Check if there are any teleport locations
        if (teleportLocations.Length > 0)
        {
            // Teleport the player to the current location
            transform.position = teleportLocations[currentTeleportIndex].position;

            // Move to the next teleport location index
            currentTeleportIndex = (currentTeleportIndex + 1) % teleportLocations.Length;
        }
        else
        {
            Debug.LogError("No teleport locations set!");
        }
    }
}
