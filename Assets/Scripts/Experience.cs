using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Experience : MonoBehaviour{

    private float expValue;

  	//sets amount of exp for orb drop
    public void setExp(int playerLevel)
    {
        expValue = ((float) playerLevel+10)*Random.Range(0.5f, 2.0f);
    }
	//gives you exp value
    public float getExp()
    {
        return expValue;
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	//allows for pick up of exp by player
    void OnTriggerStay(Collider c)
    {
        if(c.tag == "Player")
        {
            Player p = c.GetComponent<Player>();
            p.gainExp(this);
            Destroy(this.gameObject);
        }
    }
}
