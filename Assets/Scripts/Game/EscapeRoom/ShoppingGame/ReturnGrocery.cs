using UnityEngine;

public class ReturnGrocery : MonoBehaviour
{
    [SerializeField]
    private ShopManager shopManager;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("GroceryItem"))
        {
            shopManager.ReturnGroceryItem(collision.gameObject);
        }
    }
}
