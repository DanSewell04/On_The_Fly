using UnityEngine;

public class ShopInteractor : MonoBehaviour
{
    public float interactDistance = 3f;          // How far player can reach
    public LayerMask shopLayer;                  // Only collide with this
    public ShopManager shopManager;              // Link your ShopManager here

    private bool isShopOpen = false;

    void Update()
    {
        if (!isShopOpen && Input.GetKeyDown(KeyCode.E))
        {
            TryOpenShop();
        }

        if (isShopOpen && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseShop();
        }
    }

    private void TryOpenShop()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, interactDistance, shopLayer))
        {
            Debug.Log("Hit shop: " + hit.collider.name);
            shopManager.OpenShop();
            isShopOpen = true;
        }
        else
        {
            Debug.Log("No shop detected in front.");
        }
    }

    private void CloseShop()
    {
        shopManager.CloseShop();
        isShopOpen = false;
    }
}

