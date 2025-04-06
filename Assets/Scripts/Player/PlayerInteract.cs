using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionDistance = 3f;  // Interaction range
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the object collided with the player is interactable
        if (collision.gameObject.CompareTag("Interactable"))
        {
            // Log interaction
            Debug.Log("Interacting with " + collision.gameObject.name);

            // Interact with the object immediately
            InteractWithObject(collision.gameObject);
        }
    }

    private void InteractWithObject(GameObject interactableObject)
    {
        Debug.Log("Interacting with " + interactableObject.name);

        // Try calling a pickup, if the object has the ItemPickup component
        if (interactableObject.TryGetComponent(out ItemPickup pickup))
        {
            pickup.Pickup(gameObject);  // Pass the player GameObject to the Pickup method
        }
        else
        {
            // Default fallback: destroy the object
            Destroy(interactableObject);
        }
    }
}
