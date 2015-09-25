using UnityEngine;
using System.Collections;
using AssemblyCSharp;

public class Experience : MonoBehaviour{

    private float expValue;

  
    public void setExp(int playerLevel)
    {
        expValue = ((float) playerLevel+10)*Random.Range(0.5f, 2.0f);
    }

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

    void OnTriggerStay(Collider c)
    {
        if(c.tag == "Player")
        {
            Player p = c.GetComponent<Player>();
            p.gainExp(this);
            this.gameObject.SetActive(false);
        }
    }
}
