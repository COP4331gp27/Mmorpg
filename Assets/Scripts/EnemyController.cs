using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
	private float accl = 2;
	private int maxSpeed = 10;
	public GameObject[] targets;
	private Rigidbody rb;
    private Vector3[] playerPositions;
    private int numPlayers = 100;


    // Use this for initialization
    void Start () {
        targets = null;
		rb = GetComponent<Rigidbody>();
        playerPositions = new Vector3[numPlayers];
	}
	
	// Update is called once per frame
	void Update () {
        if (targets == null)
        {
            Debug.Log("Targets is null!");
            if (!GameObject.FindGameObjectsWithTag("Player").Equals(null))
            {
                targets = GameObject.FindGameObjectsWithTag("Player");
                numPlayers = targets.Length;
                Debug.Log("This is the number of players: " + numPlayers);
                for (int i = 0; i < numPlayers; i++)
                {
                    playerPositions[i] = targets[i].transform.position;
                }
            }
        }
        
        if (playerPositions[0].Equals(null))
        {
            Debug.Log("No players!");
        }
        Vector3 target = this.transform.position;// playerPositions[closestTarget(this.transform.position)];
        Vector3 heading = target - this.transform.position;
		rb.AddForce (heading * accl);
		if (this.rb.velocity.magnitude > maxSpeed) {
			this.rb.velocity = this.rb.velocity.normalized * maxSpeed;
		}

		//Debug.Log ("Moving Towards: " + target.transform.position.ToString ());

	}
    private int closestTarget(Vector3 currentPosition)
    {
        //float[] playerDistances = new float[numPlayers];
        //int i = 0;
        //foreach(Vector3 v in playerPositions)
        //{
        //    playerDistances[i] = (this.transform.position - v).magnitude;
        //    i++;
        //}
        //float min = playerDistances[0];
        //int playerIndex = 0;
        //for(i=0; i<playerDistances.Length; i++)
        //{
        //    float f = playerDistances[i];
        //    if (f < min)
        //    {
        //        min = f;
        //        playerIndex = i;
        //    }
        //}
        //return playerIndex;
        return 0;
    }

    
}
