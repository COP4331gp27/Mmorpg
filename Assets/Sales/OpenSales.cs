using UnityEngine;
using System.Collections;

public class OpenSales : MonoBehaviour
{
    public Canvas InventoryScreen;

    void Start ()
    {
        InventoryScreen = InventoryScreen.GetComponent<Canvas>();
        InventoryScreen.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Entered Shop");

            InventoryScreen.enabled = true;
        }
    }
	
    void OnTriggerExit (Collider other)
    {
        Debug.Log("Left Shop");

        InventoryScreen.enabled = false;
    }
}
