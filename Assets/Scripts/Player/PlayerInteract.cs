using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public float interactionDistance = 3f;  // How close the player needs to be to interact with an object.
    public KeyCode interactKey = KeyCode.E; // The key to press for interaction.

    void Update()
    {
        // Raycast from the player to check if there is an interactable object in front.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            // Check if the object is tagged as "Interactable".
            if (hit.collider.CompareTag("Interactable"))
            {
                // Display a message in the console (or UI) to show that the object is interactable.
                Debug.Log("Press " + interactKey.ToString() + " to interact with " + hit.collider.name);

                // If the player presses the interact key, call the interact method.
                if (Input.GetKeyDown(interactKey))
                {
                    InteractWithObject(hit.collider.gameObject);
                }
            }
        }
    }

    private void InteractWithObject(GameObject interactableObject)
    {
        Debug.Log("Interacting with " + interactableObject.name);
        Destroy(interactableObject); 
    }
}
