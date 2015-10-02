using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class OpenSales : MonoBehaviour
{
    public Canvas InventoryScreen;
    public Canvas Main;
    public Canvas Help;
    public Canvas Settings;

    void Start ()
    {
        InventoryScreen = InventoryScreen.GetComponent<Canvas>();
        Main = Main.GetComponent<Canvas>();
        Help = Help.GetComponent<Canvas>();
        Settings = Settings.GetComponent<Canvas>();
        Main.enabled = false;
        Help.enabled = false;
        Settings.enabled = false;
        InventoryScreen.enabled = false;
    }

   /* void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {   
            InventoryScreen.enabled = true;
        }
    }*/

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player" && (!Main.enabled && !Help.enabled && !Settings.enabled))
        {
            InventoryScreen.enabled = true;
        }
        else
        {
            InventoryScreen.enabled = false;
        }
    }
	
    void OnTriggerExit (Collider other)
    {
        InventoryScreen.enabled = false;
    }
}
